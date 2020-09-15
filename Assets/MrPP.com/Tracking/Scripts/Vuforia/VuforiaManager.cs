using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if NO_UNITY_VUFORIA
namespace MrPP.Tracking
{
    public class VuforiaManager : GDGeek.Singleton<VuforiaManager>
    {
    }
}
#else
using Vuforia;
namespace MrPP.Tracking { 
    public class VuforiaManager : TrackingManager
    {
        private Dictionary<VuforiaMark.Mark, VuforiaMark> marks_ = new Dictionary<VuforiaMark.Mark, VuforiaMark>();
        public void Start() {
            var list = gameObject.GetComponentsInChildren<VuforiaMark>();
            foreach (var mark in list) {
                mark.onStateChanged += doStateChanged;
                marks_[mark.mark] = mark;
            }
          
        }
        void OnDestroy() {
            foreach (var mark in marks_) {
                mark.Value.onStateChanged -= doStateChanged;
            }
        
        }
        public override void close() {
            VuforiaBehaviour.Instance.enabled = false;
        }

        public override void addHandler(ITrackingHandler handler)
        {
            base.addHandler(handler);
            foreach (var mark in handler.marks) {
                if (marks_.ContainsKey(mark)) {

                    TrackableBehaviour.Status[] status = getStatus(handler.gameObject);
                    if (Array.Exists(status, element => element == marks_[mark].currentStatus))
                    {
                        handler.find(marks_[mark]);
                    }
                    
                }
            }
        }

        private TrackableBehaviour.Status[] getStatus(GameObject obj) {
            ExtendedTracked et = obj.GetComponent<ExtendedTracked>();

            if (et == null)
            {
                TrackableBehaviour.Status[] status = { TrackableBehaviour.Status.TRACKED, TrackableBehaviour.Status.DETECTED };
                return status;
            }
            else {

                TrackableBehaviour.Status[] status = { TrackableBehaviour.Status.TRACKED, TrackableBehaviour.Status.DETECTED, TrackableBehaviour.Status.EXTENDED_TRACKED };
                return status;
            }
        }
        private void doStateChanged(VuforiaMark mark, TrackableBehaviour.Status last, TrackableBehaviour.Status state)
        {

         //  Debug.LogError(mark.mark);
            if (_map.ContainsKey(mark.mark))
            {
                HashSet<ITrackingHandler> set = _map[mark.mark];

                foreach (ITrackingHandler handler in set)
                {
                   
                   // bool find = false;

                    TrackableBehaviour.Status[] status = getStatus(handler.gameObject);
                 

                    if (!Array.Exists(status, element => element == last) && Array.Exists(status, element => element == state))
                    {
                        handler.find(mark);
                    }
                    else if (Array.Exists(status, element => element == last) && !Array.Exists(status, element => element == state))
                    {
                        handler.lost(mark);

                    }

                 
                }

            }
        }

    }
}
#endif