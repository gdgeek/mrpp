

using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using ZXing;

using ZXing.QrCode;
using ZXing.Common;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections.Concurrent;


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
        ConcurrentQueue<ZXing.Result> messageQueue_ = new ConcurrentQueue<ZXing.Result>();
       // public List<string> _qrMessage = new List<string>();
        // public UnityEngine.UI.Text labelQrc;
        public AudioSource audioSource;
        Thread qrThread;
        private Color32[] color32_;
        private int width, height;
        Image output;
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
              //  Debug.LogError("open qrcode");
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
            output = cam.GetCameraImage(m_PixelFormat);
            if (output != null)
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
                if (output != null)
                {
                    if (updC)
                    {
                        updC = false;
                        Invoke("ForceUpdateC", 1f);
                        if (output == null)
                        {
                            return;
                        }
                        color32_ = null;
                        color32_ = ImageToColor32(output);
                        if (width == 0 | height == 0)
                        {
                            width = output.BufferWidth;
                            height = output.BufferHeight;
                        }
                        output = null;
                    }
                }
            }

            //labelQrc.text = QRMessage;
            if (gotResult)
            {

                while (messageQueue_.Count > 0)
                {
                    
                    if(messageQueue_.TryDequeue(out ZXing.Result result))
                    { 
                        Debug.LogError(result.Text);
                        this.onRecevie?.Invoke(result.Text);
                    }
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
          
                if (reading && color32_ != null && width > 0 && height > 0)
                {
                    try
                    {
                        ZXing.Result[] list = barcodeReader.DecodeMultiple(color32_, width, height);
                       // _qrMessage.Clear();
                        if (list != null) { 
                            foreach (var result in list)
                            {
                                messageQueue_.Enqueue(result);  
                                //_qrMessage.Add(r.Text);
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