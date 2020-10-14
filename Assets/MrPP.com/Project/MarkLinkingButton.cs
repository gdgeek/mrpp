using GDGeek;
using Mirror.Discovery;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrPP.Project
{

    public class MarkLinkingButton : MonoBehaviour
    {
        [SerializeField]
        private HostList _hostList;


        
        private Transform target_ = null;
        public void onDiscoveredServer(ServerResponse info)
        {
            refresh();
        }
        public void Start() {

            Network.NetworkSystem.Instance.discovery.OnServerFound.AddListener(onDiscoveredServer);

        }
        
        public void show(Transform target)
        {
            target_ = target;
            this.gameObject.SetActive(true);
        }
        void Update()
        {
            if (target_ != null)
            {
                this.transform.position = target_.position;
                this.transform.rotation = target_.rotation;
            
            }
        }
        public void hide()
        {
            target_ = null;
            this.gameObject.SetActive(false);
        }
        public enum State{ 
            None,
            Input,
            Host,//server
            Wait,//clien
            Play,
        }
        private State state_ = State.None;
        private void refresh()
        {
            var servers = Network.NetworkSystem.Instance.servers;
            List<ServerResponse> list = new List<ServerResponse>();
            foreach (var server in servers) 
            {
                list.Add(server.Value);

            }
            _hostList.setDatas(list);
            
            //_hostList.refresh();
            _adjust.SetActive(false);
            _start.SetActive(false);
            _host.SetActive(false);

            _wait.SetActive(false);
            _hostList.gameObject.SetActive(false);
            switch (state_)
            {
                case State.Input:

                    _hostList.gameObject.SetActive(true);

                    if (list.Count != 0)
                    {
                        _host.SetActive(false);
                    }
                    else
                    {
                        _host.SetActive(true);
                    }
                    break;
                case State.Host:
                    _start.SetActive(true);
                    break;
                case State.Play:
                    _adjust.SetActive(true);
                    break;
                case State.Wait:
                    _wait.SetActive(true);
                    break;
            }
        }
        public void setState(State state) {
            state_ = state;
            refresh();
        }
        [SerializeField]
        private GameObject _host;
        [SerializeField]
        private GameObject _start;
        [SerializeField]
        private GameObject _adjust;
        [SerializeField]
        private GameObject _wait;

    }
}