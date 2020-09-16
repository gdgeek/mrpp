

using MrPP.Restful;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MrPP.Restful
{
    public class HeartbeatTrigger : MonoBehaviour, IEventFactory
    {
        public UnityEvent _onTrigger;
        [SerializeField]
        private string _type;
        public string type => _type;
          
        public void post(string data)
        {
            _onTrigger?.Invoke();
        }


    }
}

