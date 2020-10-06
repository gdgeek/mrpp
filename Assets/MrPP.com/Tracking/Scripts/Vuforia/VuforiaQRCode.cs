

using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using ZXing;

using ZXing.QrCode;
using ZXing.Common;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


#if NO_UNITY_VUFORIA

namespace MrPP.Common
{
    public class VuforiaQRCode : QRCodeManager
    {
       
        public override void open(){}
        public override void close(){}
    }
}
#else

using Vuforia;
namespace MrPP.Common
{

    public class VuforiaQRCode : QRCodeManager
    {



        private PIXEL_FORMAT m_PixelFormat = PIXEL_FORMAT.GRAYSCALE;
        private bool m_RegisteredFormat = false;

        public bool reading;

        public List<string> _qrMessage = new List<string>();
        // public UnityEngine.UI.Text labelQrc;
        public AudioSource audioSource;
        Thread qrThread;
        private Color32[] c;
        private int W, H;
        Image QCARoutput;
        bool updC;
        bool gotResult = false;

        private void Start()
        {
            

        }
        private bool running = false;
        public override void open()
        {
            //  CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);

            if (qrThread == null) {
                VuforiaARController.Instance.RegisterTrackablesUpdatedCallback(OnTrackablesUpdated);

                var isAutoFocus = CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
                if (!isAutoFocus)
                {
                    CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_NORMAL);
                }
                qrThread = new Thread(DecodeQR);
                qrThread.Start();
                running = true;
                Debug.LogError("open qrcode");
            }

        }

        private void OnDestroy()
        {
            running = false;
        }
        public override void close()
        {
            if (qrThread != null)
            {
                VuforiaARController.Instance.UnregisterTrackablesUpdatedCallback(OnTrackablesUpdated);

           
                qrThread.Abort();
                running = false;
                qrThread = null;
            }
          
        }
        public void OnTrackablesUpdated()
        {
            Vuforia.CameraDevice cam = Vuforia.CameraDevice.Instance;

            if (!m_RegisteredFormat)
            {
                Vuforia.CameraDevice.Instance.SetFrameFormat(m_PixelFormat, true);

                m_RegisteredFormat = true;
            }
            QCARoutput = cam.GetCameraImage(m_PixelFormat);
            if (QCARoutput != null)
            {
                reading = true;

                updC = true;
            }
            else
            {
                reading = false;
                Debug.Log(m_PixelFormat + " image is not available yet");
            }
        }

        void Update()
        {

            if (reading)
            {
                if (QCARoutput != null)
                {
                    if (updC)
                    {
                        updC = false;
                        Invoke("ForceUpdateC", 1f);
                        if (QCARoutput == null)
                        {
                            return;
                        }
                        c = null;
                        c = ImageToColor32(QCARoutput);
                        if (W == 0 | H == 0)
                        {
                            W = QCARoutput.BufferWidth;
                            H = QCARoutput.BufferHeight;
                        }
                        QCARoutput = null;
                    }
                }
            }

            //labelQrc.text = QRMessage;
            if (gotResult)
            {
                foreach (var message in _qrMessage) {
                    this.onRecevie?.Invoke(message);
                }

                // audioSource.Play();
                gotResult = false;
            }
        }
        void DecodeQR()
        {
            var barcodeReader = new BarcodeReader { AutoRotate = false, TryInverted = false };
            barcodeReader.ResultFound += OnResultF;
            while (running)
            {
          
                if (reading && c != null && W > 0 && H > 0)
                {
                    try
                    {
                        ZXing.Result[] result = barcodeReader.DecodeMultiple(c, W, H);
                        _qrMessage.Clear();
                        if (result != null) { 
                            foreach (var r in result)
                            {
                                _qrMessage.Add(r.Text);
                            }
                        }    

                    }
                    catch (Exception e)
                    {
                        Debug.LogWarning(e.Message);
                    }
                }

            }
        }
        void OnResultF(Result result)
        {

            Debug.Log(result.Text);
            gotResult = true;

        }
        void ForceUpdateC()
        {
            updC = true;
        }

        Color32[] ImageToColor32(Vuforia.Image a)
        {
            if (!Vuforia.Image.IsNullOrEmpty(a))
            {
                return null;
            }
            Color32[] r = new Color32[a.BufferWidth * a.BufferHeight];
            for (int i = 0; i < r.Length; i++)
            {
                r[i].r = r[i].g = r[i].b = a.Pixels[i];
            }
            return r;
        }
    }
}
#endif