using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.InputHandler { 
    public class Toucher : MonoBehaviour, IMixedRealityTouchHandler
    {
        public void OnTouchCompleted(HandTrackingInputEventData eventData)
        {
            Debug.LogError("touch completed");
        }

        public void OnTouchStarted(HandTrackingInputEventData eventData)
        {
            
        }

        public void OnTouchUpdated(HandTrackingInputEventData eventData)
        {
            
        }

        
    }
}