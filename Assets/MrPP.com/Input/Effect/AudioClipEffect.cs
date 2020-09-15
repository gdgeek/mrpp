

using System;
using System.Collections;
using System.Collections.Generic;
using GDGeek;

using UnityEngine;
namespace MrPP
{
    namespace Input
    {
        [RequireComponent(typeof(Inputable))]
        public class AudioClipEffect : MonoBehaviour, IInputHandler
        {

            [SerializeField]
            private AudioClip _focusExit;
            [SerializeField]
            private AudioClip _focusEnter;
            [SerializeField]
            private AudioClip _inputDown;
            [SerializeField]
            private AudioClip _inputUp;

            [SerializeField]
            private AudioSource _source;
            private void Start()
            {
               
            }
            public void play(AudioClip clip)
            {
                if (_source != null)
                {
                    _source.PlayOneShot(clip);
                }
                else {
                    AudioSoundPlayer.GetOrCreateInstance.play(this.transform.position, clip);
                }
            }
            public void focusExit()
            {
                if (_focusExit != null)
                {

                    play(_focusExit);
                }
            }

            public void focusEnter()
            {

                if (_focusEnter != null)
                {
                    play(_focusEnter);
                }

            }


            public void inputDown()
            {

                if (_inputDown != null)
                {
                    play(_inputDown);
                }
            }

            public void inputUp()
            {


                if (_inputUp != null)
                {
                    play(_inputUp);
                }
            }
        }
    }
}
