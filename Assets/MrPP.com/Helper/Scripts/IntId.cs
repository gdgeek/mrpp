using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrPP.Helper
{
    public class IntId : MonoBehaviour
    {
        [SerializeField]
        private int _value;
        public int value
        {
            set {
                _value = value;
            }
            get {
                return _value;
            }
        }
    }
}