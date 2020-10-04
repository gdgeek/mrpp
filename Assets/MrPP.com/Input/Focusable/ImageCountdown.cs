using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MrPP.Input
{
    public class ImageCountdown : MonoBehaviour, ICountdown
    {
        [SerializeField]
        private Image _image;
      
        public void close()
        {
            _image.enabled = false;
        }

        public void open()
        {
            _image.enabled = true;

            _image.fillAmount = 0;
        }

        public void percent(float per)
        {
            _image.fillAmount = per;
            //_renderer.sharedMaterial.SetFloat("_Angle", per * 360f);
        }
    }
}