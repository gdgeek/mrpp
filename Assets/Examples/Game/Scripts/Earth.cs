using GDGeek;
using Mirror;
using MrPP.Myth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Game { 
    public class Earth : NetworkBehaviour, IBridgeReceiver
    {
        [SerializeField]
        [SyncVar(hook = "onOpen")]
        private bool _open = false;
        [SerializeField]
        InterfaceAnimManager _iam;
        public void onOpen(bool oldValue, bool newValue) {
            if (newValue) {
                _iam.startAppear();
            }
            else
            {
                _iam.startDisappear();

            }
        }
        
        [SerializeField]
        private Transform[] _targets;

        public string handle => this.longName();

        // Start is called before the first frame update
        void Start()
        {
            BridgeBroadcast.Instance?.addReceiver(this);
          
        }
        void OnDestory()
        {

            BridgeBroadcast.Instance?.removeReceiver(this);
        }

        public void change()
        {

            Bridge.Instance?.post(handle, "change");
        }
        public void open() {

            Bridge.Instance?.post(handle, "open");
        }
        public void close() {

            Bridge.Instance?.post(handle, "close");
        }

        public void broadcast(string evt, object data)
        {
            if (evt == "open")
            {
                _open = true;
            }
            else if (evt == "close")
            {

                _open = false;
            }
            else if (evt == "change")
            {
                _open = !_open;

            }
        }
    }
}