using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MrPP {
    namespace Input {
        public class DisableHandler : MonoBehaviour {




            public UnityEvent onDisabled;
            public UnityEvent onEnabled;

            private bool isEnabled_ = false;

            [SerializeField]
            private Collider _collider;

            public new Collider collider{
                get {
                    if (_collider == null) {
                        _collider = this.GetComponent<Collider>();
                    }
                    return _collider;
                }
            }
            private void Start()
            {

                isEnabled_ = collider.enabled;
                refresh();
            }
            // Update is called once per frame
            void Update () {
                if (isEnabled_ != collider.enabled) {
                    isEnabled_ = collider.enabled;
                    refresh();
                }

            }
            private void refresh() {
                if (isEnabled_)
                {
                    doEnabled();
                }
                else {
                    doDisable();
                }
            }
            private void doDisable()
            {
                IDisable[] disables = this.gameObject.GetComponents<IDisable>();
                foreach (var d in disables)
                {
                    d.onDisabled();
                }
                if (onDisabled != null)
                {
                    onDisabled.Invoke();
                }
            }
            private void doEnabled() {
                IDisable[] disables = this.gameObject.GetComponents<IDisable>();
                foreach (var d in disables)
                {
                    d.onEnabled();
                }
                if (onEnabled != null)
                {
                    onEnabled.Invoke();
                }
            }
        }
    }
}