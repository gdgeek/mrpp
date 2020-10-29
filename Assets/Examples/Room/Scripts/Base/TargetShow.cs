using GDGeek;
using MrPP.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrPP.PartyBuilding
{
    public class TargetShow : MonoBehaviour
    {
        public Action onSelected
        {
            get;
            set;
        }
        [SerializeField]
        Transform target_ = null;
        public void Start()
        {
            //this.gameObject.SetActive(false);
        }
      
        internal void show(Transform target)
        {
           // Debug.LogError(size);
            this.transform.setGlobalScale(target.lossyScale);
            target_ = target;
            this.transform.position = target_.position;
            this.transform.rotation = target_.rotation;
            this.gameObject.SetActive(true);
        }
        [SerializeField]
        internal Inputable inputable;
        internal void disableInput()
        {
            inputable.enabled = false;
        }
        internal void enableInput()
        {
            inputable.enabled = true;
        }
        internal void hide()
        {
            this.gameObject.SetActive(false);
            target_ = null;
        }
    }
}