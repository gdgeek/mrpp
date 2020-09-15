using GDGeek;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MrPP.Basis
{
    public class SimpleProcess : Process
    {

#if UNITY_EDITOR
        public string _stateName;
        void Update()
        {
            _stateName = fsm_.getCurrSubState().name;

        }

#endif


        [SerializeField]
        private UnityEvent _beginReady;
        [SerializeField]
        private UnityEvent _endReady;


        [SerializeField]
        private UnityEvent _beginPlay;
        [SerializeField]
        private UnityEvent _endPlay;



        private FSM fsm_ = new FSM();

        public override FSM fsm => fsm_;

        // Start is called before the first frame update
        void Start()
        {
            fsm_.addState("ready", ready());
            fsm_.addState("play", play());
            fsm_.init("ready");
            // fsm_.init("start", start());
        }

        private StateBase play()
        {
            State state = new State(); 
            
            state.onStart += delegate
            {
                _beginPlay?.Invoke();
            };

            state.onOver += delegate
            {
                _endPlay?.Invoke();
            };
            return state;
        }

        private StateBase ready()
        {
            State state = new State();
            state.onStart += delegate
            {
                _beginReady?.Invoke();
            };

            state.onOver += delegate
            {
                _endReady?.Invoke();
            };
            state.addAction("start", "play");
            return state;
        }

      
    }
}