using GDGeek;
using Mirror;
using Mirror.Discovery;
using MrPP.Myth;
using MrPP.Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Project {
   

    public class MarkLinking : MonoBehaviour
    {

#if UNITY_EDITOR
        public string _stateName;
        void Update(){
            _stateName = fsm_.getCurrSubState().name;

        }
        [SerializeField]
        private Transform _testTarget;

        public void testMark()
        {

            target_ = this._testTarget;
            _button.show(target_);
            fsm_.post("mark");
        }
#endif

        [SerializeField]
        private MarkLinkingButton _button;



        [SerializeField]
        private HostList _hosts;
        [SerializeField]
        private ClientList _clients;

        [SerializeField]
        private Basis.Process _process = null;
        [SerializeField]
        private Tracking.TrackingMark.Mark _mark;
        private Transform target_ = null;

        private Tracking.TrackingHandler tracking_ = null;
        
        public void open()
        {
            tracking_ = this.gameObject.AddComponent<Tracking.TrackingHandler>();
            tracking_.addMaker(_mark);

            tracking_.onFind += delegate (Transform target)
            {
                target_ = target;
                _button.show(target_);
                fsm_.post("mark");
                
            };
            tracking_.onLost += delegate (Transform tsm)
            {
                _button.hide();
            };
        }

        private void doJoin(ServerResponse serverResponse)
        {
            Network.NetworkSystem.Instance.serverResponse = serverResponse;
            this.fsm_.post("client");
        }

        public void doStart()
        {
            this.fsm_.post("start");
        }
        public void doHost() {
            fsm_.post("host");

        }
        public void doAdjust()
        {

            fsm_.post("adjust");
        }
        public void doPlay()
        {
            fsm_.post("play");
        }

    

        FSM fsm_ = new FSM();
        void Start()
        {
            _hosts.onSelected = doJoin;
            // _clients.onSelected += dele
            HeroAudoList.Instance.onChange += doClient;

            fsm_.addState("begin", begin());
            fsm_.addState("input", input());

            fsm_.addState("host", host());
            fsm_.addState("start", start());



            fsm_.addState("client", client());

            fsm_.addState("play", play());
            fsm_.init("begin");

        }

        private void doClient()
        {
            List<HeroData> datas = new List<HeroData>();
            var list = HeroAudoList.Instance;
            foreach (var item in list) {
                datas.Add(item.data);
            }
            _clients.setDatas(datas);
        }

        private StateBase play()
        {
            State state = new State();
            state.onStart += delegate
            {
                _button.setState(MarkLinkingButton.State.Play);
            };
            state.addAction("adjust", delegate {
                Yggdrasil.Instance.setupMark(target_); 
            });

            return state;
        }

        private ServerResponse serverResponse_ = null;
      
        private StateBase client()
        {

            State state = new State();
            state.onStart += delegate
            {

                _button.setState(MarkLinkingButton.State.Wait);
                Network.NetworkSystem.Instance.client();
            };
            state.addAction("play", "play");

            return state;
        }

        private StateBase start()
        {
            State state = new State();
            state.onStart += delegate
            {
                TaskWait tw = new TaskWait(0.03f);
                TaskManager.PushBack(tw, delegate
                {
                    _process.fsm.post("start");
                });
                TaskManager.Run(tw);
                
            };
            state.addAction("play", "play");
            
            return state;


        }

        private StateBase input()
        {
            State state = new State();

            state.onStart += delegate
            {
                Network.NetworkSystem.Instance.listening();
                _button.setState(MarkLinkingButton.State.Input);
            };


            state.addAction("host", "host");
            state.addAction("client", delegate {

                return "client";
            });
            state.onOver += delegate
            {

                Yggdrasil.Instance.setupMark(target_);
            };
            return state;
        }

      

        private StateBase host()
        {

            State state = new State();
            state.onStart += delegate
            {
                _button.setState(MarkLinkingButton.State.Host);
                Network.NetworkSystem.Instance.startHost();
            };
            state.onOver += delegate
            {

            };

            state.addAction("start", "start");

            return state;
        }

   
      

        private StateBase begin()
        {
            State state = new State();
            state.addAction("mark", "input");
            return state;
        }
    }
}