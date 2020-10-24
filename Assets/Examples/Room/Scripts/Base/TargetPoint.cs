using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.PartyBuilding{
    public class TargetPoint : GDGeek.Singleton<TargetPoint>
    {
        public Target[] targets {

            get {
                return this.gameObject.GetComponentsInChildren<Target>(true);
            }
        
        }
        public TargetBody[] bodyTargets {
            get
            {
                return this.gameObject.GetComponentsInChildren<TargetBody>(true);
            }
        }
    }
}