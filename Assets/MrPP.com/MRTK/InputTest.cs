using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP { 
    public class InputTest : MonoBehaviour, IMixedRealityPointerHandler, IMixedRealityFocusChangedHandler
    {    
        public void OnBeforeFocusChange(FocusEventData eventData)
        {
            Debug.Log("on before focus change");
        }

        public void OnFocusChanged(FocusEventData eventData)
        {    

            Debug.Log("on focus changed");
        }
           
        public void OnPointerClicked(MixedRealityPointerEventData eventData)
        {


            Debug.Log("on pointer clicked");
        }
            
        public void OnPointerDown(MixedRealityPointerEventData eventData)
        {

            Debug.Log("on pointer down");
        }

        public void OnPointerDragged(MixedRealityPointerEventData eventData)
        {

            Debug.Log("on pointer dragged");
        }

        public void OnPointerUp(MixedRealityPointerEventData eventData)
        {
            Debug.Log("on pointer up");
        }

      
    }
}