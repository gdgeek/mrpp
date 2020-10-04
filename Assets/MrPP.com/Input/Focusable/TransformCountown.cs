using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MrPP.Input
{
    public class TransformCountown : MonoBehaviour, ICountdown
    {
        [SerializeField]
        private Transform _target;

        public void close()
        {
            _target.gameObject.SetActive( false);
        }

        public void open()
        {
            _target.gameObject.SetActive(true);
        }

        public void percent(float per)
        {
            _target.localScale = new Vector3(per, 1, 1);
            //_image.fillAmount = per;
            //_renderer.sharedMaterial.SetFloat("_Angle", per * 360f);
        }
    }
}