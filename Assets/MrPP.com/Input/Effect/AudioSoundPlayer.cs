using GDGeek;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Input { 
    public class AudioSoundPlayer : GDGeek.Singleton<AudioSoundPlayer> {

        private AudioSource source_ = null;
        // Use this for initialization
        void Awake () {
            GameObject obj = new GameObject("Player");
            obj.transform.SetParent(this.transform);
            source_ = obj.AddComponent <AudioSource> ();
            source_.playOnAwake = false;
        }
	

        internal void play(Vector3 position, AudioClip clip)
        {
            source_.transform.position = position;
            source_.PlayOneShot(clip);
        }
    }
}