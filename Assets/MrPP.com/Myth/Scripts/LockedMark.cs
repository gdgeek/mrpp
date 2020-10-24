using GDGeek;
using Microsoft.MixedReality.Toolkit;
using System;
using UnityEngine;

namespace MrPP.Myth
{
    internal class LockedMark : MonoBehaviour
    {

        private Transform from_ = null;
        private Transform to_ = null;
        public void locking(Transform from, Transform to)
        {
            from_ = from;
            to_ = to;
            from_.position = to_.position;
            from_.rotation = to_.rotation;
        }
        public void Update() {
          
            if (from_ != null && to_ != null && to_.hasChanged) {

                
                if (!VectorExtensions.CloseEnough(to_.position, from_.position, 0.05f) || ! QuaternionExtensions.AlignedEnough(to_.rotation, from_.rotation, 3.0f)) { 
                    TweenTransformData.Begin(from_.gameObject, 0.1f, new TransformData(to_.position, to_.rotation, from_.lossyScale));
                }
                to_.hasChanged = false;
            }
        }
    }
}