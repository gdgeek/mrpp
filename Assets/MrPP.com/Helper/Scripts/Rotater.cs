using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Helper { 
    public class Rotater : MonoBehaviour {
        [SerializeField]
        private Vector3 _eulerAngles;
        [SerializeField]
        private float _speed = 1f;
      
	
	    // Update is called once per frame
	    void Update () {
            this.transform.Rotate(_eulerAngles * _speed * Time.deltaTime);
	    }
    }
}