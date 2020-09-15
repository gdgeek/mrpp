using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



#if !NO_UNITY_VUFORIA
using Vuforia;
using static Vuforia.TrackableBehaviour;
#endif
namespace MrPP.Tracking
{
    public class VuforiaMark : TrackingMark
/*
#if !NO_UNITY_VUFORIA
        , ITrackableEventHandler
#endif*/
    {
      
        [SerializeField]
        private Mark _mark;

        public override Mark mark
        {
            set
            {
                _mark = value;
            }
            get
            {
                return _mark;
            }
        }

      

#if !NO_UNITY_VUFORIA
        protected TrackableBehaviour trackableBehaviour_;
        public TrackableBehaviour.Status currentStatus {
            get {
                return trackableBehaviour_.CurrentStatus;
            }
        }

        protected virtual void Start()
        {

            trackableBehaviour_ = GetComponent<TrackableBehaviour>();

            if (trackableBehaviour_)
            {
                trackableBehaviour_.RegisterOnTrackableStatusChanged(OnTrackableStatusChanged);
            }


            //if (_transform == null) {
            //    _transform = this.transform;
            //}
            // mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            //if (mTrackableBehaviour)
            //    mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
        protected virtual void OnDestroy()
        {
            if (trackableBehaviour_)
            {
                trackableBehaviour_.UnregisterOnTrackableStatusChanged(OnTrackableStatusChanged);
            }
        }

        /*
        protected virtual void Start()
        {
            
            trackableBehaviour_ = GetComponent<TrackableBehaviour>();
            if (trackableBehaviour_)
                trackableBehaviour_.RegisterTrackableEventHandler(this);
        }*/
        public Action<VuforiaMark, TrackableBehaviour.Status, TrackableBehaviour.Status> onStateChanged
        {
            get;
            set;
        }

        public void OnTrackableStatusChanged(StatusChangeResult result)
        {
          //  Debug.LogError("!!!");
            onStateChanged?.Invoke(this, result.PreviousStatus, result.NewStatus);
        }
#endif
    }
}
