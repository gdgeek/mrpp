using GDGeek;
using Mirror;
using MrPP.Myth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Network {
    public interface ITransformModel
    {
        bool controler { get; }

        void setPose(Yggdrasil.AsgardPose asgardPose);
        void update(Yggdrasil.AsgardPose pose);
    }
    public class TransformModel : NetworkBehaviour, IBridgeReceiver, ITransformModel
    {


        [SerializeField]
        [SyncVar(hook = "doPose")]
        private Yggdrasil.AsgardPose pose_;



        [SerializeField]
        private ATransformView _target = null;


        private void doPose(MrPP.Myth.Yggdrasil.AsgardPose oldValue, MrPP.Myth.Yggdrasil.AsgardPose newValue)
        {

            _target.onPose(oldValue, newValue);
            //_binding.onPose?.Invoke(newValue);

        }



        [SerializeField]
        [SyncVar(hook = "lockedChange")]
        private uint locked_;

        private void lockedChange(uint oldValue, uint newValue)
        {
            //  Debug.LogError("value:" + value);
            // locked_ = value;

            _target.onLocked(oldValue, newValue);

           // _binding.onLocked?.Invoke(newValue);
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
            //  Debug.LogError("locked");
            if (Hero.Instance != null)
            {

                //                Debug.LogError("locked" + Hero.Instance.id);
                Bridge.Instance.post(this.handle, "locked_", Hero.Instance.id);
            }
        }
        public void unlocked()
        {

            Bridge.Instance.post(this.handle, "locked_", 0);
        }
        public bool controler
        {
            get
            {
                if (Hero.Instance == null)
                {
                    return true;
                }
                return (Hero.Instance.id == locked_);
            }
        }
        public void update(Yggdrasil.AsgardPose aPose)
        {
            if (controler && Bridge.Instance != null)
            {
                Bridge.Instance.post(this.handle, "pose_", aPose);
            }
        }


        public void Awake()
        {
            _target.model = this;
            this.pose_ = MrPP.Myth.Yggdrasil.Instance.GetAsgardPose(_target.transform);
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
           // pose_ = aPose;
            if (controler && Bridge.Instance != null)
            {
                Bridge.Instance.post(this.handle, "pose_", aPose);
            }/**/
            // pose_ = aPose;
        }


    }
}