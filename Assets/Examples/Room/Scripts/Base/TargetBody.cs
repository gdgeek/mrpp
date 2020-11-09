using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Game { 
    public class TargetBody : MonoBehaviour
    {
        [SerializeField]
        private TargetContent _content;

        public void Start()
        {
            this.gameObject.SetActive(false);
        }
        public TargetContent content
        {
            get
            {
                return _content;
            }
        }

        public string id
        {
            get
            {
                return this.gameObject.name;
            }
        }
    }
}