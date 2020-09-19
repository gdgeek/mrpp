using MrPP.Myth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Network { 
    public class PoseBinding : MonoBehaviour
    {
        public Func<Yggdrasil.AsgardPose> getPose;

        public Action<Yggdrasil.AsgardPose> onPose;

        public Func<uint> getLocked;
        public Action<uint> onLocked;

        public bool controler {
            get {
                if (Hero.Instance == null) {

                    Debug.LogError("what?");
                    return true;
                }
//                Debug.LogError("id" + Hero.Instance.id);
  //              Debug.LogError("getLocked" + getLocked());  
                return (Hero.Instance.id == getLocked());
            }
        }

    }
}