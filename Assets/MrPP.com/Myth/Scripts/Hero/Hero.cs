using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MrPP.Helper;
using System;
using System.Net;
using Mirror;
namespace MrPP.Myth {
  //  [RequireComponent(typeof(SynchroLocalTransform))]
    public class Hero : NetworkBehaviour, IHero
    {
        
        private static Hero Instance_;
        public static Hero Instance
        {
            get
            {
                return Instance_;
            }
        }

        public uint id
        {
            get
            {

                return this.netId;
                /*
                if (string.IsNullOrEmpty(data.ip))
                {
                    return 0;
                }
                return BitConverter.ToUInt32(IPAddress.Parse(data.ip).GetAddressBytes(), 0);
                */
            }
        }



        [SerializeField]
        [SyncVar(hook = "stateChanged")]
        private HeroState _state = HeroState.None;

        public void ready() {
            Debug.Log("ready");
            this.CmdReady();
        }

        public HeroState state
        {
            get
            {
                return _state;
            }
        }

        private void stateChanged(MrPP.Myth.HeroState oldValue, MrPP.Myth.HeroState newValue)
        {
            _state = newValue;
            refresh();
        }
        


        [SerializeField]
        [SyncVar(hook = "dataChanged")]
        private HeroData _data;




        public HeroData data
        {
            get
            {
                return _data;
            }
        }

     

        private void dataChanged(MrPP.Myth.HeroData oldValue, MrPP.Myth.HeroData newValue)
        {
            Debug.Log("change" + newValue.name);
            _data = newValue;  
            refresh();
        }
        private void refresh() {
            
            this.gameObject.name = "Hero@" + _data.name;
            if (HeroAudoList.IsInitialized) {
                HeroAudoList.Instance.refresh(this); 
            }
          
         
        }


      

        public override void OnStartClient() {
            base.OnStartClient();
            refresh();
        }
        public void OnDestroy()
        {

            if (HeroAudoList.IsInitialized)
            {
                HeroAudoList.Instance.remove(this);
            }
          
        }

        [Command(channel = 1)]
        public void CmdPlayerMessage(HeroData data)
        {
            this._data = data;
            this._state = HeroState.Joined;
        }
        [Command(channel = 1)]
        public void CmdReady()
        {
            this._state = HeroState.Ready;
        }
  

//        private NetworkSystem networkDiscovery_ = null;
        private void Start()
        {

//            networkDiscovery_ = NetworkSystem.Instance;
            if (isLocalPlayer)
            {
                 Instance_ = this;

                HeroData d;
                d.ip = LocalInfo.IP;  
               /* if (PlatformInfo.Instance != null)
                {

                    d.platform = PlatformInfo.Instance.type;
                }
                else {
                    d.platform = PlatformInfo.Type.Unknow;
                }*/
                d.name = LocalInfo.ComputerName;
                d.isServer = this.isServer;
                CmdPlayerMessage(d);


            }
            if (HeroAudoList.IsInitialized) {
                HeroAudoList.Instance.add(this);
            }
          
        }
       
        private void Update()
        {
            if (isLocalPlayer)
            {
                transform.position = Camera.main.transform.position;
                transform.rotation = Camera.main.transform.rotation;
            }
        }
    }
}