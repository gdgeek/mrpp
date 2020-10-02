using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDGeek;
namespace MrPP.UX { 
    public class ResizeTransform : MonoBehaviour,IResize,IExecute
    {


        [SerializeField]
        private Vector3 _size;
        [SerializeField]
        private List<Transform> _list;

        [SerializeField]
        private bool _x = true;
        [SerializeField]
        private bool _y = true;

        [SerializeField]
        private bool _z = true;
        public void resize(Vector3 size)
        {
            _list.ForEach(delegate(Transform item) {

                Vector3 oldScale = item.lossyScale;
                item.setGlobalScale(new Vector3(_x ? size.x : oldScale.x, _y ? size.y : oldScale.y, _z ? size.z : oldScale.z));


            });
        }
        public void execute()
        {
            resize(_size);

        }
    }
}