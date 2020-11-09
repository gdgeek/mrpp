using GDGeek;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace MrPP.Game
{
    public class MeidaManager : MonoBehaviour
    {
        FSM fsm_ = new FSM();
        [SerializeField]
        private GameObject _root;

        [SerializeField]
        private VideoPlayer _player;

        [SerializeField]
        private GameObject _gazePlayButton;

        [SerializeField]
        private Transform _playButton;
        [SerializeField]
        private Transform _playClose;


        [SerializeField]
        private Transform _pauseButton;
        [SerializeField]
        private Transform _pauseClose;

#if UNITY_EDITOR
        [SerializeField]
        private string _stateName;
        void Update()
        {
            _stateName = fsm_.getCurrSubState().name;
        }
#endif
        void Start()
        {
            fsm_.addState("close", close());
            fsm_.addState("open", open());
            fsm_.addState("play", play(), "open");
            fsm_.addState("pause", pause(), "open");
            fsm_.init("close");

            _player.loopPointReached += (a) =>
            {
                doStop();
            };
        }

        private StateBase pause()
        {
            State state = new State();

            state.onStart += delegate
            {
                _player.Pause();

                _pauseButton.gameObject.SetActive(false);
                _pauseClose.gameObject.SetActive(true);
            };
            state.onOver += delegate
            {
                _pauseButton.gameObject.SetActive(true);
                _pauseClose.gameObject.SetActive(false);
            };
            state.addAction("play", "play");
            return state;
        }

        private StateBase play()
        {
            State state = new State();
            state.onStart += delegate
            {
                _playButton.gameObject.SetActive(false);
                _playClose.gameObject.SetActive(true);
                Debug.LogError("Play!");
                _player.Play();
            };
            state.onOver += delegate
            {
                _playButton.gameObject.SetActive(true);
                _playClose.gameObject.SetActive(false);
            };
            state.addAction("pause", "pause");
            return state;
        }

        private StateBase open()
        {
            State state = new State();
            state.onStart += delegate
            {
                _root.SetActive(true);
                _gazePlayButton.SetActive(false);
            };

            state.addAction("stop", "close");
            return state;
        }
        public void doPlay()
        {
            fsm_.post("play");
        }
        public void doStop()
        {
            fsm_.post("stop");
        }
        public void doPause()
        {
            fsm_.post("pause");
        }
        private StateBase close()
        {
            State state = new State();
            state.onStart += delegate
            {
                _gazePlayButton.SetActive(true);
                _player.Stop();
                _root.SetActive(false);
            };
            state.addAction("play", "play");
            return state;
        }

    }
}