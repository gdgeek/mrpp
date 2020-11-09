using GDGeek;
using Mirror;
using MrPP.Helper;
using MrPP.Myth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrPP.Game { 
    public class UIBoard : NetworkBehaviour, IBridgeReceiver
    {

        void Start() {
            BridgeBroadcast.GetOrCreateInstance.addReceiver(this);
        }
        void OnDestory() {
            BridgeBroadcast.GetOrCreateInstance.removeReceiver(this);
        }
        [SerializeField]
        [SyncVar(hook ="onIndex")]
        private int _index = -1;


        private void onIndex(int oldValue, int newValue)
        {

            if (oldValue >= 0 && oldValue < _items.Length)
            {
                _items[oldValue].close();

            }
            if (newValue >= 0 && newValue < _items.Length)
            {

                _items[newValue].open();
            }
        }



        [SerializeField]
        [SyncVar(hook = "onOpen")]
        private bool _open = false;

        private void onOpen(bool oldValue, bool newValue)
        {

            if (newValue)
            {
               TweenScale.Begin(this.gameObject, 0.3f, Vector3.one);

            }
            else {

                TweenScale.Begin(this.gameObject, 0.3f, Vector3.zero);
            }
           
        }




        [SerializeField]
        private UIBoardItem[] _items;

        public string handle => this.longName();

        public void broadcast(string evt, object data)
        {
            if (evt == "touch")
            {
                _index = (int)(data);
            }
            else if (evt == "open")
            {
                _open = true;
            }
            else if (evt == "close") {
                _open = false;
            }
        }
        public void touchID(IntId id)
        {
            touch(id.value);
        }
        public void touch(int index) {
            Bridge.Instance?.post(this.handle, "touch", index);
        }


        private void open()
        {

            Bridge.Instance?.post(this.handle, "open");
        }
        private void close()
        {

            Bridge.Instance?.post(this.handle, "close");
        }

    }
}