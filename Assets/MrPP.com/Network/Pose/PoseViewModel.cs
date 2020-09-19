using GDGeek;
using MrPP.Myth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace MrPP.Network
{
    public class PoseViewModel : NetworkBehaviour, IPoseModel, IBridgeReceiver
    {

        [SerializeField]
        [SyncVar(hook = "poseChange")]
        private Yggdrasil.AsgardPose pose_; 


        private void poseChange(MrPP.Myth.Yggdrasil.AsgardPose oldValue, MrPP.Myth.Yggdrasil.AsgardPose newValue)
        {
            _binding.onPose?.Invoke(newValue);

        }



        [SerializeField]
        [SyncVar(hook = "lockedChange")]
        private uint locked_;

        private void lockedChange(uint oldValue, uint newValue) {
            //  Debug.LogError("value:" + value);
           // locked_ = value;
            _binding.onLocked?.Invoke(newValue);
        }

        [SerializeField]
        private PoseBinding _binding;
      
        public void locked()
        {
            Debug.LogError("locked");
            if (Hero.Instance != null) {

                Debug.LogError("locked" + Hero.Instance.id);
                Bridge.Instance.post(this.handle, "locked_", Hero.Instance.id);
            }
        }
        public void unlocked() {

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
            if (controler && Bridge.Instance != null) {
                Bridge.Instance.post(this.handle, "pose_", aPose);
            }
        }


        public string handle => this.longName();

        public void broadcast(string evt, object data)
        {
            if ("pose_" == evt)
            {
                pose_ = (MrPP.Myth.Yggdrasil.AsgardPose)(data);
            }
            if ("locked_" ==  evt) {
                locked_ = (uint)(data);
            }
        }
        public void Awake() {
            _binding.getPose = delegate {
                return this.pose_;
            };
            _binding.getLocked = delegate
            {
                return this.locked_;
            };
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
    }
}
