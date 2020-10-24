using System;
using UnityEngine;
using UnityEngine.Events;

namespace MrPP.PartyBuilding
{
    public class TargetContent: MonoBehaviour
    {
      
        public enum Type{ 
            UI,
            Article,
        }
        public Action doClose {
            get;
            set;
        }
        public void onClose() {
            doClose?.Invoke();
            Debug.LogError("on close!");
        }

        public void show()
        {
            Debug.LogError("show");
            doShow?.Invoke();
        }
        public void hide()
        {
            doHide?.Invoke();
        }
        [SerializeField]
        private UnityEvent doShow;

        [SerializeField]
        private UnityEvent doHide;



    }
}