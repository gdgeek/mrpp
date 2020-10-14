using GDGeek;
using Mirror;
using MrPP.Myth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MrPP.Network
{

    public class SyncTransform : NetworkBehaviour, IBridgeReceiver
    {

        [SerializeField]
        private bool _clientAuthority = true;
        [SerializeField]
        private Transform _target;
        public Transform target
        {
            get
            {
                if (_target)
                {
                    return _target;
                }
                return this.transform;
            }
        }

        [SerializeField]
        [SyncVar(hook = "doPose")]
        private Yggdrasil.AsgardPose pose_;

        [SerializeField]
        UnityEvent hasControler;
        [SerializeField]
        UnityEvent lostControler;
        [SerializeField]
        UnityEvent releaseControler;



        private void doPose(MrPP.Myth.Yggdrasil.AsgardPose oldValue, MrPP.Myth.Yggdrasil.AsgardPose newValue)
        {

            if (!this.controler)
            {
                Yggdrasil.WorldPose world = Yggdrasil.Instance.getWorldPose(newValue);
                if (Vector3.Distance(world.position, target.position) < 0.3f)
                {
                    TweenTransformData.Begin(target.gameObject, 0.03f, new TransformData(world.position, Quaternion.LookRotation(world.forward, world.up), world.scale));
                }
                else
                {
                    target.position = world.position;
                    target.rotation = Quaternion.LookRotation(world.forward, world.up);
                    target.setGlobalScale(world.scale);
                }
            }

        }



        [SerializeField]
        [SyncVar(hook = "onLocked")]
        private uint locked_;

        private void onLocked(uint oldValue, uint newValue)
        {
            if (Hero.Instance.id != oldValue &&  Hero.Instance.id == newValue)
            {
                hasControler?.Invoke();

            }
            
            if(Hero.Instance.id == oldValue && Hero.Instance.id != newValue)
            {
                lostControler?.Invoke();
            }
            if (0 == newValue)
            {
                releaseControler?.Invoke();
            }

            
        }

        public string handle => this.longName();

        public void broadcast(string evt, object data)
        {
            if ("pose_" == evt)
            {
                pose_ = (MrPP.Myth.Yggdrasil.AsgardPose)(data);
            }
            if ("locked_" == evt)
            {
                locked_ = (uint)(data);
            }
        }

        public void askControl()
        {
            if (Hero.Instance != null)
            {
                Bridge.Instance.post(this.handle, "locked_", Hero.Instance.id);
            }
        }
        public void unlocked()
        {
            Bridge.Instance.post(this.handle, "locked_", (uint)0);
        }
        public bool controler
        {
            get
            {
                if (!_clientAuthority) {
                    if (this.isServer)
                    {
                        return true;
                    }
                    else {
                        return false;
                    }
                }else if (Hero.Instance == null)
                {
                    return true;
                }
                return (Hero.Instance.id == locked_);
            }
        }
   
        void Update()
        {
            if (this.controler)
            {
                if (this.target.hasChanged)
                {
                    MrPP.Myth.Yggdrasil.AsgardPose pose = MrPP.Myth.Yggdrasil.Instance.GetAsgardPose(this.target);
                    if (Bridge.Instance != null)
                    {
                        Bridge.Instance.post(this.handle, "pose_", pose);
                    }
                    this.target.hasChanged = false;
                }

            }
        }

        public void Awake()
        {
           
            this.pose_ = MrPP.Myth.Yggdrasil.Instance.GetAsgardPose(target);
        }



        public void Start()
        {

            BridgeBroadcast.Instance.addReceiver(this);
        }
        public void OnDestroy()
        {
            if (BridgeBroadcast.IsInitialized)
            {

                BridgeBroadcast.Instance.removeReceiver(this);
            }
        }

        public void setPose(Yggdrasil.AsgardPose aPose)
        {
            if (controler && Bridge.Instance != null)
            {
                Bridge.Instance.post(this.handle, "pose_", aPose);
            }
        }


    }
}