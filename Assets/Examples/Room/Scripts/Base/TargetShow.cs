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
        Transform mark_ = null;
        public void Start()
        {
            //this.gameObject.SetActive(false);
        }
      
        internal void show(Transform mark, Vector2 size)
        {
            this.transform.setGlobalScale(new Vector3(size.x, size.y, 0.01f));
            mark_ = mark;
            this.transform.position = mark_.position;
            this.transform.rotation = mark_.rotation;
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
            mark_ = null;
        }
    }
}