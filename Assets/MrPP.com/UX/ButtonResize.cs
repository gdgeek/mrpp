using GDGeek;
using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.UX
{

    public class ButtonResize : MonoBehaviour, IExecute, IResize
    {
        [SerializeField]
        private Vector3 _size;
        [SerializeField]
        private Transform _box;
        [SerializeField]
        private Transform _backPlateQuad;
        [SerializeField]
        private Transform _frontPlate;
        [SerializeField]
        private Transform _text;
        [SerializeField]
        private BoxCollider _collider;


        [SerializeField]
        private NearInteractionTouchable _touchable;

        public void resize(Vector3 size) {
            _backPlateQuad.setGlobalScale(size);
            _box.setGlobalScale(size);
            _frontPlate.setGlobalScale(size);
            _collider.size = size;
            _touchable.SetBounds(new Vector2(size.x, size.y));
            _text.setGlobalScale(new Vector3(Mathf.Min(size.x, size.y), Mathf.Min(size.x, size.y), 1));
        }
        public void execute()
        {
            resize(_size);
           
        }

      
    }
}