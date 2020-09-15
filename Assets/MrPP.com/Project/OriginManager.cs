using MrPP.Myth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Project { 
    public class OriginManager : MonoBehaviour
    {
        [SerializeField]
        private Tracking.TrackingMark.Mark _mark;
        [SerializeField]
        private OriginButton _button;
        [SerializeField]
        private Basis.Process _process = null;
        //[SerializeField]
        private Tracking.TrackingHandler tracking_ = null;

        private Transform target_ = null;

        public void test() {

            target_ = this.transform;
            doClicked();
        }
        public void open() {

            tracking_ = this.gameObject.AddComponent<Tracking.TrackingHandler>();
            tracking_.addMaker(_mark);

            tracking_.onFind += delegate (Transform target)
            {
                target_ = target;
                _button.show(target);
            };
            tracking_.onLost += delegate (Transform tsm)
            {
                _button.hide();
            };
            _button.onClicked += doClicked;
        }
        public void doClicked() {
            if (target_ != null) {
                Yggdrasil.Instance.setupMark(target_);
                _process.fsm.post("start");

            }
        }
        public void close()
        {
            _button.onClicked -= doClicked;
            DestroyImmediate(tracking_);
            tracking_ = null;
        }
        
    }
}