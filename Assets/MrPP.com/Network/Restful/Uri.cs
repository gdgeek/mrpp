using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Restful { 
    public class Uri : MonoBehaviour
    {
        [SerializeField]
        private string _value = null;
        public string value {
            set {
                _value = value;
            }
            get {
                return _value;
            }
        }


    }
}