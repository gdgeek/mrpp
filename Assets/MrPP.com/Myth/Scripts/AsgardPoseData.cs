using System;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Myth
{
    [Serializable]
    public class AsgardPoseData
    {
        public AsgardPoseData()
        {
        }

        public AsgardPoseData(GameObject gameObj)
        {


            aPose =  MrPP.Myth.Yggdrasil.Instance.getAsgardPose(new MrPP.Myth.Yggdrasil.WorldPose(gameObj.transform));//.WorldToAsgard()
            this.name = gameObj.name;
          
        }
       
        public MrPP.Myth.Yggdrasil.WorldPose worldPose {

            get {
                return MrPP.Myth.Yggdrasil.Instance.getWorldPose(this.aPose);
            }
        }
        [SerializeField]
        public string name;
        [SerializeField]
        public MrPP.Myth.Yggdrasil.AsgardPose aPose;
       

    }


}