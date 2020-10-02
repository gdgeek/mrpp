using GDGeek;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.UX { 
    public class ResizeList : MonoBehaviour,IResize, IExecute
    {
        [SerializeField]
        private Vector3 _size;
        public void execute()
        {
            resize(_size);
        }

        public void resize(Vector3 size)
        {
            IResize[] resize = this.gameObject.GetComponentsInChildren<IResize>(true);
            Array.ForEach<IResize>(resize, delegate (IResize item) {
                IResize self = this;
                if (self != item) {
                    item.resize(size);
                }
            });

        }

      
    }
}