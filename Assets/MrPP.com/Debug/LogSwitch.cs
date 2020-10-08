using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP { 
    public class LogSwitch : MonoBehaviour
    {
        public void onOff() {
            var debug = LogManager.Instance.debug;
            debug.gameObject.SetActive(!debug.gameObject.activeSelf);
        }
    }
}