

using GDGeek;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using System;
using UnityEngine;

namespace MrPP.Input
{

    [RequireComponent(typeof(NearInteractionTouchable))]
    public class Inputable : MonoBehaviour,
        IMixedRealityFocusHandler,
        IMixedRealityPointerHandler,
        IMixedRealityTouchHandler
    {


      

        void IMixedRealityTouchHandler.OnTouchCompleted(HandTrackingInputEventData eventData)
        {
            IInputHandler[] inputs = this.gameObject.GetComponents<IInputHandler>();
            foreach (var input in inputs)
            {
                input.focusExit();
            }
            IClicker[] clickers = this.gameObject.GetComponents<IClicker>();
            foreach (var click in clickers)
            {
                click.execute();
            }
        }

        void IMixedRealityTouchHandler.OnTouchStarted(HandTrackingInputEventData eventData)
        {
            IInputHandler[] inputs = this.gameObject.GetComponents<IInputHandler>();
            foreach (var input in inputs)
            {
                input.focusEnter();
            }
        }

        void IMixedRealityTouchHandler.OnTouchUpdated(HandTrackingInputEventData eventData)
        {
       
        }


        public void Awake() {
            // Interactable interactable =  this.gameObject.AskComponent<Interactable>();
           // interactable.OnClick.AddListener(doClick);
            // Interactable
        }

        public void OnFocusEnter(FocusEventData eventData)
        {
            IInputHandler[] inputs = this.gameObject.GetComponents<IInputHandler>();
            foreach (var input in inputs)
            {
                input.focusEnter();
            }
        }

        public void OnFocusExit(FocusEventData eventData)
        {
            IInputHandler[] inputs = this.gameObject.GetComponents<IInputHandler>();
            foreach (var input in inputs)
            {
                input.focusExit();
            }
        }

       
        public void OnPointerClicked(MixedRealityPointerEventData eventData)
        {
            doClick();
        }

        public void OnPointerDown(MixedRealityPointerEventData eventData)
        {
            IInputHandler[] inputs = this.gameObject.GetComponents<IInputHandler>();
            foreach (var input in inputs)
            {
                input.inputDown();

            }
        }

        public void OnPointerDragged(MixedRealityPointerEventData eventData)
        {
          //  throw new NotImplementedException();
        }

        public void OnPointerUp(MixedRealityPointerEventData eventData)
        {
            IInputHandler[] inputs = this.gameObject.GetComponents<IInputHandler>();
            foreach (var input in inputs)
            {
                input.inputUp();
            }
        }

       
        private  void doClick() {
            IClicker[] clickers = this.gameObject.GetComponents<IClicker>();
            foreach (var click in clickers) {
                click.execute();
            }
        }
      
 
    }
   
}