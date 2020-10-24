using GDGeek;
using Mirror;
using Mirror.Discovery;
using MrPP.Myth;
using MrPP.Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrPP.Project
{

    public class AreaLinking : MonoBehaviour
    {

        public void doFound(Transform target) {

            target_ = target;
            _button.show(target_);
            Yggdrasil.Instance.lockedMark(target_);
            //Yggdrasil.Instance.setupMark(target_);
            fsm_.post("found", target);
        }
        public void doLost() {
            fsm_.post("lost");
        }

#if UNITY_EDITOR
        public string _stateName;
        void Update(){
            _stateName = fsm_.getCurrSubState().name;

        }
      
#endif

        [SerializeField]
        private MarkLinkingButton _button;


        [SerializeField]
        private Status _status;

        [SerializeField]
        private HostList _hosts;
        [SerializeField]
        private ClientList _clients;

        [SerializeField]
        private bool _autoStart = false;

        [SerializeField]
        private Basis.Process _process = null;

        private Transform target_ = null;

        public void open()
        {

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
        public void doHost()
        {
            fsm_.post("host");

        }
        public void doAdjust()
        {

            fsm_.post("adjust");
        }
        public void doPlaying()
        {
            fsm_.post("play");
        }



        FSM fsm_ = new FSM();
        void Start()
        {
            _status.onOnline += doOnline;
            _status.onPlaying += doPlaying;
            _hosts.onSelected = doJoin;
            HeroAudoList.Instance.onChange += doClient;

            fsm_.addState("begin", begin());
            fsm_.addState("input", input());

            fsm_.addState("host", host());
            fsm_.addState("start", start());
            fsm_.addState("auto_start", autoStart());


            fsm_.addState("client", client());

            fsm_.addState("play", play());
            fsm_.init("begin");

        }

        private StateBase autoStart()
        {
            State state = TaskState.Create(delegate ()
            {
                Task task = new Task();
                TaskManager.PushFront(task, delegate
                {
                    Network.NetworkSystem.Instance.startHost();
                });
                return task;

            }, this.fsm_, "start");
            return state;
        }

        private void doOnline()
        {
            fsm_.post("online");
        }

        private void doClient()
        {
            List<HeroData> datas = new List<HeroData>();
            var list = HeroAudoList.Instance;
            //            Debug.LogError(list.count);
            foreach (var item in list)
            {
                datas.Add(item.data);
            }
            _clients.setDatas(datas);
            //  _clients.refresh();
        }

        private StateBase play()
        {
            State state = new State();
            state.onStart += delegate
            {
                _process.doStart();
                _button.setState(MarkLinkingButton.State.Play);
            };
            state.addAction("adjust", delegate {
                Yggdrasil.Instance.lockedMark(target_);
            });   
                
            return state;
        }

        private ServerResponse serverResponse_;

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
                    _status.play();
                    //
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
            state.addAction("client", "client");
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

                Network.NetworkSystem.Instance.startHost();
            };
            state.addAction("online", delegate
            {
                _button.setState(MarkLinkingButton.State.Host);
            });
            state.onOver += delegate
            {

            };

            state.addAction("start", "start");

            return state;
        }




        private StateBase begin()
        {
            State state = new State();
            state.addAction("found",delegate(FSMEvent evt) {

                target_ = (Transform)evt.obj;
                if (_autoStart) {
                    return "auto_start";
                }
                return "input";
            });
            return state;
        }
    }
}