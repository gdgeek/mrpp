using GDGeek;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Myth
{
    public class Yggdrasil : GDGeek.Singleton<Yggdrasil>
    {

        [SerializeField]
        private float _dilatation = 1.0f;
        public enum MountPoint
        {
            Yggdrasil,
            Mark,
            Balance,

        }



        [SerializeField]
        private MountPoint _mount = MountPoint.Balance;


        public Transform point {
            get {
                switch (_mount)
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
        }
       

        [SerializeField]
        private Transform _mark;
        public Transform mark
        {

            get
            {
                return _mark;
            }
        }
        [SerializeField]
        private Transform _balance;
        public Transform balance
        {

            get
            {
                return _balance;
            }
        }
     

        [System.Serializable]
        public struct AsgardPose {

     
            public AsgardPose(Vector3 aPosition,
                Vector3 aUp,
                Vector3 aForward,
                Vector3 aScale)
            {
                this.position = aPosition;
                this.up = aUp;
                this.forward = aForward;
                this.scale = aScale;
            }
            [SerializeField]
            public Vector3 position;
            [SerializeField]
            public Vector3 up;

            [SerializeField]
            public Vector3 forward;

            [SerializeField]
            public Vector3 scale;
        }


        [System.Serializable]
        public class WorldPose
        {
            public WorldPose(Transform transform)
            {
                position = transform.position;
                forward = transform.forward;
                up = transform.up;
                scale = transform.lossyScale;
            }

            public WorldPose(Vector3 aPosition,
                 Vector3 aUp,
                 Vector3 aForward,
                 Vector3 aScale)
            {
                this.position = aPosition;
                this.up = aUp;
                this.forward = aForward;
                this.scale = aScale;
            }

            [SerializeField]
            public Vector3 position;
            [SerializeField]
            public Vector3 up;

            [SerializeField]
            public Vector3 forward;

            [SerializeField]
            public Vector3 scale;
        }
       

        public WorldPose getWorldPose(AsgardPose asgard) {

            return AsgardToWorld(asgard, this.point);
        }

        public static WorldPose AsgardToWorld(AsgardPose asgard, Transform transform) {

            //Vector3 position = transform.TransformDirection(asgard.position);
            Vector3 position = transform.TransformPoint(asgard.position) * Yggdrasil.Instance._dilatation;
            Vector3 up = transform.TransformDirection(asgard.up);
            Vector3 forward = transform.TransformDirection(asgard.forward);
            Vector3 scale = asgard.scale;
            return new WorldPose(position, up, forward, scale);
        }
        public AsgardPose GetAsgardPose(Transform tfm) {
            return WorldToAsgard(new WorldPose(tfm), this.point);
        }
        public AsgardPose getAsgardPose(WorldPose world) {

            return WorldToAsgard(world, this.point);// new AsgardPose(position, up, forward, scale);
        }
        public static AsgardPose WorldToAsgard(WorldPose world, Transform transform) {

            Vector3 o = transform.localScale;
            transform.setGlobalScale(Vector3.one);
            Vector3 position = transform.InverseTransformPoint(world.position)/ Yggdrasil.Instance._dilatation;
         
            Vector3 up = transform.InverseTransformDirection(world.up);
            Vector3 forward = transform.InverseTransformDirection(world.forward);
            Vector3 scale = world.scale;
            transform.localScale = o;
            return new AsgardPose(position, up, forward, scale);
        }
        public void setupMark(Transform mark) {
          //  TransformData data = new TransformData(mark, TransformData.Type.World);
       //     data.write(ref _mark, TransformData.Type.World);
            _mark.transform.position = mark.position;
            _mark.transform.rotation = mark.rotation;
        }
        /*
        public void setupMark(TransformData data) {
            data.write(ref _mark, TransformData.Type.Local);
        }*/
        public void setup(Transform hero, Transform qrmark)
        {
            Yggdrasil.AsgardPose ap = Yggdrasil.WorldToAsgard(new Yggdrasil.WorldPose(Yggdrasil.Instance.transform), hero);

      
            this.transform.position = qrmark.position;
            this.transform.rotation = qrmark.rotation;

          
            Yggdrasil.WorldPose wp = Yggdrasil.AsgardToWorld(ap, this.transform);
            this.transform.position = wp.position;
            this.transform.rotation = Quaternion.LookRotation(wp.forward, wp.up);
        

        }
    }
}