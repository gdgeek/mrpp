/*==============================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.

Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using System;
using UnityEngine;
using UnityEngine.Events;

#if NO_UNITY_VUFORIA
namespace MrPP
{
    public class TrackableHandler : MonoBehaviour
    {
        internal bool tranking;
    }
}
#else

using Vuforia;
using static Vuforia.TrackableBehaviour;

namespace MrPP {
    /// <summary>
    ///     A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    public class TrackableHandler : MonoBehaviour//, DefaultTrackableEventHandler
    {
        [SerializeField]
        private UnityEvent _onTrackingFound;
        [SerializeField]
        private UnityEvent _onTrackingLost;

        public Action<TrackableBehaviour.Status, TrackableBehaviour.Status> onStateChanged{
            get;
            set;
        }
        [SerializeField]
        private bool tranking_ = false;
        public bool tranking {
            get {
                return tranking_;
            }
            set{
                tranking_ = value;
            }
        }

        private void  onTrakingLost(){

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            tranking = false;
            if(_onTrackingLost != null){
                _onTrackingLost.Invoke();
            }

        }
        private void onTrankingFound(){


            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            tranking = true;
            if (_onTrackingFound != null)
            {
                _onTrackingFound.Invoke();
            }

        }
#region PRIVATE_MEMBER_VARIABLES

        protected TrackableBehaviour mTrackableBehaviour;
        //protected VuMarkBehaviour _vuMarkBehaviour;

        #endregion // PRIVATE_MEMBER_VARIABLES

        #region UNTIY_MONOBEHAVIOUR_METHODS



        protected virtual void Start()
        {

            mTrackableBehaviour = GetComponent<TrackableBehaviour>();

            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterOnTrackableStatusChanged(OnTrackableStatusChanged);
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
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.UnregisterOnTrackableStatusChanged(OnTrackableStatusChanged);
            }
        }

#endregion // UNTIY_MONOBEHAVIOUR_METHODS

#region PUBLIC_METHODS

        /// <summary>
        ///     Implementation of the ITrackableEventHandler function called when the
        ///     tracking state changes.
        /// </summary>
        public void OnTrackableStatusChanged(StatusChangeResult result)
        {
           
            if (result.NewStatus == TrackableBehaviour.Status.DETECTED ||
                result.NewStatus == TrackableBehaviour.Status.TRACKED ||
                result.NewStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                onTrankingFound();
            }
            else if (result.PreviousStatus == TrackableBehaviour.Status.TRACKED &&
                     result.NewStatus == TrackableBehaviour.Status.NO_POSE)
            {
                onTrakingLost();
            }
            else
            {
                // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
                // Vuforia is starting, but tracking has not been lost or found yet
                // Call OnTrackingLost() to hide the augmentations
            

                onTrakingLost();
            }
          
        }

#endregion // PUBLIC_METHODS

       
      
    }
}
#endif