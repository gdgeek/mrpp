
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MrPP.Tracking
{ 
    public class MarkTarget : GDGeek.Singleton<MarkTarget>
    {

        [SerializeField]
        private Transform _offset = null;
           
         
        public Action<Transform> onMarkFound { get; set; }
       // public Action onMarkLost { get; set; }




        [SerializeField]
        private TrackableHandler _target = null;

        [SerializeField]
        private TrackableHandler _validate = null;
        private bool _tracking = false;
	    // Update is called once per frame
	    void Update () {

            if (!_tracking)
            {
//                Debug.Log("_target" + _target.tranking.ToString() + ":_validate" + _validate.tranking.ToString());
                if (_target.tranking && _validate.tranking)
                {
                    float angle = Quaternion.Angle(_target.transform.rotation, _validate.transform.rotation);
//                    Debug.Log("angle");
                   if (angle < 5.0f)
                    {
                      Debug.Log("found a");
                       // Configure.Instance.markTransform = _offset;
                        onMarkFound?.Invoke(_offset);
                        _tracking = true;
                    }
                }
            }
            else {
                if (!_target.tranking || !_validate.tranking)
                {
                    _tracking = false;
                   // Configure.Instance.markTransform = null;
                 //   onMarkLost?.Invoke();
                }
                else {
                    float angle = Quaternion.Angle(_target.transform.rotation, _validate.transform.rotation);
                    if (angle >= 5.0f) {
                        _tracking = false;
                     //   onMarkLost?.Invoke();
                    }

                }

            }

        }

     
    }
}