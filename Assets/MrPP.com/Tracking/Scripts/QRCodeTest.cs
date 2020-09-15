using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Common { 
    public class QRCodeTest : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            if (!QRCodeManager.IsInitialized) {
                Destroy(this.gameObject);
            }
        }

       
    }
}