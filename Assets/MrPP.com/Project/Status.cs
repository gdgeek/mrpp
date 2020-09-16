using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Project { 
    public class Status : NetworkBehaviour
    {
        public Action onOnline
        {
            get;
            set;
        }
        public Action onPlaying
        {
            get;
            set;
        }
        [SyncVar(hook ="onPlay")]
        public bool playing = false;

        public void play() {
            Debug.LogWarning("!!play!!!!!!");
            playing = true;
        }
        void onPlay(System.Boolean oldValue, System.Boolean newValue) {
            Debug.LogWarning("!!!!!!!!");
            if (!oldValue&& newValue) { 
                onPlaying?.Invoke();
            }
        }  
        public override void OnStartClient() {
            onOnline?.Invoke();
        }
    }
}