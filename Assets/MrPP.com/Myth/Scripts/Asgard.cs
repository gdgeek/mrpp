using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDGeek;
namespace MrPP.Myth
{
    public class Asgard : MonoBehaviour
    {
        public enum MountPoint
        {
            Yggdrasil,
            Mark,
            Balance,
        }
        [SerializeField]
        private MountPoint _mountPoint = MountPoint.Balance;
        [SerializeField]
        private Transform _target = null;

        [SerializeField]
        private Transform _transform;

        private Transform getTransform() {
            if(_transform == null) {
                _transform = this.transform;
            }
            return _transform;


        }

        private bool _enable = false;


        private Yggdrasil.AsgardPose asgardPose_;
        // Use this for initialization
        void Start()
        {
            asgardPose_ = Yggdrasil.WorldToAsgard(new Yggdrasil.WorldPose(this.getTransform()), this._target);
            mount();
        }

        private IsChanged changed_ = new IsChanged();
        private bool hasChanged()
        {
            return changed_.hasChanged(this.getMountPoint());
        }

        private Transform getMountPoint()
        {
            switch (_mountPoint)
            {
                case MountPoint.Yggdrasil:
                    return Yggdrasil.Instance.transform;
                case MountPoint.Mark:
                    return Yggdrasil.Instance.mark;
                case MountPoint.Balance:
                    return Yggdrasil.Instance.balance;
            }
            return null;
        }
        private void mount()
        {
            if (!_enable || hasChanged())
            {
                Transform point = this.getMountPoint();
                Yggdrasil.WorldPose wp = Yggdrasil.AsgardToWorld(this.asgardPose_, point);
                Transform tsfm = this.getTransform();
                tsfm.position = wp.position;
                tsfm.rotation = Quaternion.LookRotation(wp.forward, wp.up);
                tsfm.setGlobalScale(wp.scale);
                _enable = true;
            }
        }
        // Update is called once per frame
        void Update()
        {
           // Debug.Log(_enable);
            mount();
        }

    
    }
}