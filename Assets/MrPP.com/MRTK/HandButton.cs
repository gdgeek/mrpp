using Microsoft.MixedReality.Toolkit.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MrPP { 
    public class HandButton : MonoBehaviour
    {
        public FollowTrackedFingers _follow;
        public float _display = 0.05f;
        public UnityEvent _onTigger;
      
        private void tigger()
        {
            _onTigger?.Invoke();
        }

        private void OnTriggerEnter(Collider collision) {
            if (collision.gameObject.name == "HandPointL" || collision.gameObject.name == "HandPointR")
            {
                tigger();
            }
        }
   

      
    }
}