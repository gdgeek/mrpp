using GDGeek;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using Mirror;

namespace MrPP.Restful
{
    public class KVHeartbeat : NetworkBehaviour
    {
        [SerializeField]
        private Uri _uri;
        [Serializable]
        public struct Data
        {

            [SerializeField]
            public string key;
            [SerializeField]
            public string value;
        }
        [System.Serializable]
        public class SyncMap : SyncDictionary<string, string> { }

        //  public class DataSyncList : SyncListStruct<Data>{

        //}

        // [SyncVar(hook = "dataChanged")]
        public SyncMap _map = new SyncMap();

        void changed(SyncMap.Operation op, string key, string item)
        {
            
            KVStation.Instance.broadcasting(key, item);
           
        }
        public static KVHeartbeat Instance = null;
        public void Awake()
        {
            Instance = this;
            _map.Callback += changed;
        }





        //private System.Timers.Timer timer_ = null;

        [SerializeField]
        private float _interval = 1f;

        private TaskCircle circle_ = new TaskCircle();
        public void open()
        {
            // Debug.LogError("open!!!2");
            circle_ = new TaskCircle();
            Task tw = new TaskWait(_interval);
            TaskManager.PushBack(tw, delegate
            {
                this.heartbeat();
            });
            circle_.push(tw);
            TaskManager.Run(circle_);
        }
        public void close()
        {
            circle_.forceQuit();
        }
        private void heartbeat()
        {


            var op = Restful.RestfulManager.Instance.options(Restful.RestfulManager.Uri(_uri.value, "receiver"), new Dictionary<string, string> { { "d", "s" } });


            Restful.RestfulManager.Instance.getArray<Data>(op, delegate (Data[] datas)
            {
                if (datas.Length != 0)
                {
                    foreach (var d in datas)
                    {
                        if (_map.ContainsKey(d.key) && _map[d.key] == d.value) {
                            continue;
                        }
                        _map[d.key] = d.value;
                     
                    }
                }
                //   _datas = new SyncList<Data>();
            });
        }

    }
}
