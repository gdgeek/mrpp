using MrPP.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;

public class TestQRCode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        QRCodeManager.Instance.open();
        MultipleVuforiaQRCode.Instance.onResult += doResult;
    }

    private void doResult(Result result)
    {
        Debug.LogError(result.Text);
        foreach (var point in result.ResultPoints) {

            Debug.LogError("x:"+point.X+",y:"+ point.Y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
