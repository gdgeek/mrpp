using System;
using System.Collections;
using System.Collections.Generic;
using GDGeek;

using UnityEngine;
namespace MrPP
{
    namespace Input
    {

        [RequireComponent(typeof(DisableHandler)), RequireComponent(typeof(Inputable))]
        public class MaterialEffect : MonoBehaviour, IInputHandler, IDisable
        {


            [SerializeField]
            private Renderer _target;


            [SerializeField]
            private Material _disable;


            [SerializeField]
            private Material _normal;

            [SerializeField]
            private Material _focus;


            [SerializeField]
            private Material _down;


            private Renderer target
            {
                get
                {
                    if (_target == null)
                    {
                        _target = this.gameObject.GetComponent<Renderer>();
                    }
                    return _target;

                }
            }


           


            public void focusExit()
            {

                _target.material = _normal;
            }

            public void focusEnter()
            {
                _target.material = _focus;
            }

          
            public void inputDown()
            {

                _target.material = _down;
            }

            public void inputUp()
            {

                _target.material = _normal;
            }

            public void onEnabled()
            {
                _target.material = _normal;
            }

            public void onDisabled()
            {

                _target.material = _disable;
            }
        }
    }
}
