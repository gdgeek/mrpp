using GDGeek;
using System;
using UnityEngine;

namespace MrPP.PartyBuilding
{
    public class BoardWindow : GDGeek.Singleton<BoardWindow>
    {
        private TargetContent content_ = null;
        public void setContent(TargetContent content)
        {
            if (content_) {
                GameObject.Destroy(content_.gameObject);
            }
            content_ = GameObject.Instantiate(content, this.transform);

            content_.show();
            Canvas canvas = content_.GetComponentInChildren<Canvas>();
            if (canvas != null) {
                canvas.worldCamera = null;
            }
            content_.transform.localPosition = Vector3.zero;
            content_.transform.localRotation = Quaternion.identity;
            content_.doClose += onClose;


        }

        private void onClose()
        {
            InterfaceAnimManager iam = content_.GetComponent<InterfaceAnimManager>();
            iam.startDisappear(true);
         
            close();
        }

        public void open() {
            this.gameObject.SetActive(true);
            content_.gameObject.SetActive(true);
            InterfaceAnimManager iam = content_.GetComponent<InterfaceAnimManager>();
            TaskWait tw = new TaskWait(0.1f);
            TaskManager.PushBack(tw, delegate
            {
                iam.startAppear();
            });
            TaskManager.Run(tw);
        }
        public void close() {
            if (content_)
            {
                content_.doClose -= onClose;
                content_.hide();
                GameObject.Destroy(content_.gameObject);
            }
            //this.gameObject.SetActive(false);

        }
    }
}