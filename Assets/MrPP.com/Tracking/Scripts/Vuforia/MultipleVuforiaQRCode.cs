

using UnityEngine;
using System;
using System.Threading;
using ZXing;
using ZXing.Common;
using System.Collections.Concurrent;
using static ZXing.RGBLuminanceSource;
using ZXing.Multi;
using ZXing.QrCode;



using Vuforia;
namespace MrPP.Common
{

    public class MultipleVuforiaQRCode : QRCodeManager
    {
        static MultipleVuforiaQRCode instance_ = null;

        void Awake() {
            MultipleVuforiaQRCode.instance_ = this;
        }
        void OnDestory() {

            MultipleVuforiaQRCode.instance_ = null;
        }

        public new static MultipleVuforiaQRCode Instance
        {
            get
            {
              
                return MultipleVuforiaQRCode.instance_;
            }

        }


        public Action<ZXing.Result> onResult
        {
            get;
            set;
        }

        private PIXEL_FORMAT _pixelFormat = PIXEL_FORMAT.GRAYSCALE;
        private bool _registeredFormat = false;

        public bool _reading;
        ConcurrentQueue<ZXing.Result> messageQueue_ = new ConcurrentQueue<ZXing.Result>();


        Thread qrThread_;
        private BinaryBitmap bitmap_;
        private Image output_;
        private bool update_;

        private bool running_ = false;
        public override void open()
        {
            
            if (qrThread_ == null)
            {
                VuforiaARController.Instance.RegisterTrackablesUpdatedCallback(onTrackablesUpdated);

                var isAutoFocus = CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
                if (!isAutoFocus)
                {
                    CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_NORMAL);
                }
                qrThread_ = new Thread(decodeQR);
                running_ = true;
                qrThread_.Start();
            }

        }

        private void OnDestroy()
        {
            running_ = false;
        }
        public override void close()
        {
            if (qrThread_ != null)
            {
                VuforiaARController.Instance.UnregisterTrackablesUpdatedCallback(onTrackablesUpdated);

                qrThread_.Abort();
                running_ = false;
                qrThread_ = null;
            }

        }
        public void onTrackablesUpdated()
        {
            Vuforia.CameraDevice cam = Vuforia.CameraDevice.Instance;
            if (!_registeredFormat)
            {
                Vuforia.CameraDevice.Instance.SetFrameFormat(_pixelFormat, true);
                _registeredFormat = true;
            }


            output_ = cam.GetCameraImage(_pixelFormat);
            if (output_ != null)
            {
                _reading = true;
                update_ = true;
            }
            else
            {
                _reading = false;
                Debug.Log(_pixelFormat + " image is not available yet");
            }
        }

        void Update()
        {

            if (_reading)
            {
                if (output_ != null)
                {
                    if (update_)
                    {
                        update_ = false;
                        Invoke("ForceUpdateC", 1f);
                        if (output_ == null)
                        {
                            return;
                        }

                        bitmap_ = new BinaryBitmap(new GlobalHistogramBinarizer(new RGBLuminanceSource(output_.Pixels, output_.BufferWidth, output_.BufferHeight, BitmapFormat.Gray8)));

                        output_ = null;
                    }
                }
            }

           
            while (messageQueue_.Count > 0)
            {

                if (messageQueue_.TryDequeue(out ZXing.Result result))
                {
                    Debug.LogError(result.Text);
                    this.onRecevie?.Invoke(result.Text);
                    this.onResult?.Invoke(result);
                }
            }

        }
        void decodeQR()
        {
            GenericMultipleBarcodeReader reader = new GenericMultipleBarcodeReader(new QRCodeReader());
            
            while (running_)
            {
                if (_reading && bitmap_ != null) {
                    try
                    {
                        ZXing.Result[] list = reader.decodeMultiple(bitmap_);
                        if (list != null)
                        {
                            foreach (var result in list)
                            {
                                messageQueue_.Enqueue(result);
                            }
                        }

                    }catch (Exception e)
                    {
                        Debug.LogWarning(e.Message);
                    }
                }

            }
        }
       
        void ForceUpdateC()
        {
            update_ = true;
        }
      
    }
}