using GDGeek;
using MrPP.Myth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MrPP.Network {
    public class PoseView : MonoBehaviour
    {
        [SerializeField]
        private Transform _target;
        public Transform target {
            get {
                if (_target) {
                    return _target;
                }
                return this.transform;
            }
        }
        [SerializeField]
        PoseModel _model;
        [SerializeField]
        PoseBinding _binding;
        [SerializeField]
        UnityEvent lostControler;
        [SerializeField]
        UnityEvent hasControler;
        void Start()
        {
            _model.setPose(MrPP.Myth.Yggdrasil.Instance.GetAsgardPose(this.target));
            _binding.onPose += doPose;
            doPose(_binding.getPose());
            _binding.onLocked += doLocked;
        }

        private void doLocked(uint id)
        {
//            Debug.LogError("id" + id);
            if (!_binding.controler)
            {
  //              Debug.LogError("lost");
                lostControler?.Invoke();
            }
            else {

    //            Debug.LogError("has");
                hasControler?.Invoke();
            }
        }

        void OnDestroy() {
            if (_binding) {
                _binding.onPose -= doPose;
            }
        }
        private void doPose(Yggdrasil.AsgardPose pose)
        {
            if (!_model.controler)
            {
                Yggdrasil.WorldPose world = Yggdrasil.Instance.getWorldPose(pose);
                if (Vector3.Distance(world.position, target.position) < 0.3f)
                {
                    TweenTransformData.Begin(target.gameObject, 0.03f, new TransformData(world.position, Quaternion.LookRotation(world.forward, world.up), world.scale));
                }
                else
                {
                    target.position = world.position;
                    target.rotation = Quaternion.LookRotation(world.forward, world.up);
                    target.setGlobalScale(world.scale);
                }
            }
        }

        void Update()
        {
            if (_model.controler)
            {
                if (this.target.hasChanged)
                {
                    MrPP.Myth.Yggdrasil.AsgardPose pose = MrPP.Myth.Yggdrasil.Instance.GetAsgardPose(this.target);
                    _model.update(pose);

                    this.target.hasChanged = false;

                }

            }
        }
    }
}