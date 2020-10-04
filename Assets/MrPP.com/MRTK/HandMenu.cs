using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP { 
    public class HandMenu : GDGeek.Singleton<HandMenu>
    {
        public FollowTrackedFingers _follow;
        public GameObject[] _buttons;
        private Action<int> touch_;
     

        MixedRealityPose pose;
          
       
        public void tigger(int idx) {
            Debug.LogError("go"+ idx);
            touch_?.Invoke(idx);
            hidden();
        }
        public void show(int num, Action<int> touch)
        {
            touch_ = touch;

            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Wrist, Handedness.Right, out pose))
            {
                this.transform.position = pose.Position;
                _follow.wristObjectR.SetActive(true);
               // wristObjectR.GetComponent<Renderer>().enabled = true;
            }
            else if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Wrist, Handedness.Left, out pose))
            {
                this.transform.position = pose.Position;
                _follow.wristObjectL.SetActive(true);
                // wristObjectR.GetComponent<Renderer>().enabled = true;
            }

            for (int i = 0; i < Mathf.Min(_buttons.Length, num); ++i) {
                _buttons[i].SetActive(true);
            }
            //throw new NotImplementedException();
        }

        public void hidden()
        {
            for (int i = 0; i < _buttons.Length; ++i)
            {
                _follow.wristObjectL.SetActive(false);
                _follow.wristObjectR.SetActive(false);
                _buttons[i].SetActive(false);
            }
        }
    }
}