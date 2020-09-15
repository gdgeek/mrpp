using GDGeek;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrPP.Project
{

    public class OriginButton : MonoBehaviour
    {
        public Action onClicked {
            get;
            set;
        }
     
        private Transform target_ = null;
        public void show(Transform target) {
            target_ = target;
            this.gameObject.SetActive(true);
        }
        void Update() {
            if (target_ != null) {
                this.transform.position = target_.position;
                this.transform.rotation = target_.rotation;
                //TransformData td = new TransformData(target_, TransformData.Type.World);
              // Transform tsf = this.transform;
               // td.write(ref tsf, TransformData.Type.World);
            }
        }
        public void hide() {
            target_ = null;
            this.gameObject.SetActive(false);
        }

    }
}