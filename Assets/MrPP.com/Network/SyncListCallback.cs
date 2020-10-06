using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Network { 
    public class SyncListCallback : MonoBehaviour
    {

        public Action action
        {
            set;
            get;

        }
        public Action start
        {
            set;
            get;

        }
        public float allTime
        {
            set;
            get;

        }
        private float time
        {
            set;
            get;

        }

        //public float _begin = 0.0f;

        void Start() {
            start?.Invoke();
        }
        
        public void reset() {
            time = 0.0f;
        }
        
        // Update is called once per frame
        void Update()
        {
            time += Time.deltaTime;
            if (time > allTime)
            {
                action?.Invoke();
                GameObject.Destroy(this);
            }
        }
    }
}