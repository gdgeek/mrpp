using GDGeek;
using MrPP.Myth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MrPP.Network {
    public abstract class ATransformView : MonoBehaviour
    {
        public ITransformModel model{
            get;
            set;
        }
        public abstract void onLocked(uint oldValue, uint newValue);
        public abstract void onPose(Yggdrasil.AsgardPose oldValue, Yggdrasil.AsgardPose newValue);
    }
    public class TransformView : ATransformView
    {

        [SerializeField]
        private Transform _target;
        public Transform target
        {
            get
            {
                if (_target)
                {
                    return _target;
                }
                return this.transform;
            }
        }

        [SerializeField]
        UnityEvent hasControler;
        [SerializeField]
        UnityEvent lostControler;
        void Start()
        {
          //  this.model.setPose();

     
        }

        public override void onLocked(uint oldValue, uint newValue)
        {
            if (Hero.Instance.id == newValue)
            {
                hasControler?.Invoke();
               
            }
            else {
                lostControler?.Invoke();
            }
          
        }

     
        public override void onPose(Yggdrasil.AsgardPose oldValue, Yggdrasil.AsgardPose newValue)
        {
            if (!model.controler)
            {
                Yggdrasil.WorldPose world = Yggdrasil.Instance.getWorldPose(newValue);
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
            if (model.controler)
            {
                if (this.target.hasChanged)
                {
                    MrPP.Myth.Yggdrasil.AsgardPose pose = MrPP.Myth.Yggdrasil.Instance.GetAsgardPose(this.target);
                    model.update(pose);
                }

            }
        }
    }
}