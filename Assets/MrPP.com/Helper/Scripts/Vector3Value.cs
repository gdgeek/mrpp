using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrPP.Helper
{
    public class Vector3Value : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _value;
        public Vector3 value
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