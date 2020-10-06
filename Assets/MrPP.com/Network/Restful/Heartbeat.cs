using GDGeek;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using Mirror;

namespace MrPP.Restful
{
    public class Heartbeat : NetworkBehaviour
    {
        [SerializeField]
        private Uri _uri;

        [SerializeField]
        private string _uriKey = "api";
        public Uri uri{
            get {
                if (_uri == null) {
                    _uri = UriManager.Instance.getUri(_uriKey);
                }
                return _uri;
            
            }
        }
        [Serializable]
        public struct Data
        {

            [SerializeField]
            public uint id;

            [SerializeField]
            public string type;

            [SerializeField]
            public string data;

            [SerializeField]
            public long time;
        }
        [System.Serializable]
        public class DataSyncList : SyncList<Data> { }

      //  public class DataSyncList : SyncListStruct<Data>{

        //}
   
     // [SyncVar(hook = "dataChanged")]
        public DataSyncList _datas = new DataSyncList() ;

        void changed(DataSyncList.Operation op, int index, Data oldItem, Data newItem)
        {
            if (op == DataSyncList.Operation.OP_ADD) { 
                Station.Instance.broadcasting(_datas[index]);
            }
        }
        public static Heartbeat Instance = null;
        public void Awake() {
            Instance = this;
            _datas.Callback += changed;
        }
        
        
          
     
       
        //private System.Timers.Timer timer_ = null;

        [SerializeField]
        private float _interval = 1f;

        private TaskCircle circle_ = new TaskCircle();
        public void open() {
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
        public void close() {
            circle_.forceQuit();
        }
        private void heartbeat()
        {

          
            var op = Restful.RestfulManager.Instance.options(Restful.RestfulManager.Uri(uri.value, "heartbeats"), new Dictionary<string, string> { { "d", "s" } });
           

            Restful.RestfulManager.Instance.getArray<Data>(op, delegate (Data[] datas)
            {
                if (datas.Length != 0) { 
                   _datas.Clear();
                    Debug.LogError(datas.Length);
                    foreach (Data d in datas) {
                        Debug.LogError(d.type);
                        _datas.Add(d);
                        Debug.LogError("=====");
                    }
                }
                //   _datas = new SyncList<Data>();
            });
        }

    }
}
