using GDGeek;
using UnityEngine;
using UnityEngine.Events;

namespace MrPP.Input
{

#if UNITY_EDITOR
    [ExecuteInEditMode]
#endif
    [RequireComponent(typeof(Inputable))]
    public class Clicker : MonoBehaviour, IClicker
    {
        [SerializeField]
        public UnityEvent onClicked;

        public void Start() {
            check();
        }
        public void check() {
           // InputCrossPlatform input = this.gameObject.AskComponent<InputCrossPlatform>();
            
        } 

        public void execute()
        {
            if (onClicked != null && enabled)
            {
                Debug.Log("clicked!");
                onClicked.Invoke();
            }
        }
    }
}