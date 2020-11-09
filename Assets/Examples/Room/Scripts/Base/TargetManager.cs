using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Game 
{ 
    public class TargetManager : GDGeek.Singleton<TargetManager>
    {
        [SerializeField]
        private TargetPoint _targetPoint;
        [SerializeField]
        private TargetBox _boxPhototype;

        private List<Target> list_ = null;
        private List<TargetBody> bodys_ = null;
        public void open() {
            Target[] list =  _targetPoint.targets;
            list_ = new List<Target>(list);
            foreach (Target item in list_) {
                if (item.content != null) { 
                    TargetBox box = _boxPhototype.create(item, this.transform);
                }
            }
            bodys_ = new List<TargetBody>(_targetPoint.bodyTargets);
            foreach (TargetBody body in bodys_)
            {
                if (body.content != null)
                {
                    Debug.LogError(body.name);
                    var content =  GameObject.Instantiate(body.content,this.transform);
                    content.transform.SetParent(this.transform);     
                    content.transform.position = body.transform.position;
                    content.transform.rotation = body.transform.rotation;
                    //  content.transform.
                    content.gameObject.SetActive(true);
                   // TargetBox box = _boxPhototype.create(item, this.transform);
                }
            }

        }
        public void close() { 
        
        }
    }
}