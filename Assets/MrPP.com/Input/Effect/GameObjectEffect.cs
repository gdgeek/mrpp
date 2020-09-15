
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP
{
    namespace Input
    {
        [RequireComponent(typeof(Inputable))]
        public class GameObjectEffect : MonoBehaviour, IInputHandler
        {




            [SerializeField]
            private GameObject _down;

            [SerializeField]
            private GameObject _normal;

            [SerializeField]
            private GameObject _focus;



            private void close()
            {
                _down.SetActive(false);
                _normal.SetActive(false);
                _focus.SetActive(false);
            }
            public void focusExit()
            {
                enter_ = false;
                close();
                _normal.SetActive(true);
                //_target.sprite = _normal;
                //GDGeek.TweenScale.Begin(target, 0.05f, oldScale_);
            }
            private bool enter_ = false;
            public void focusEnter()
            {
                enter_ = true;
                close();
                _focus.SetActive(true);
                //_target.sprite = _focus;
                // GDGeek.TweenScale.Begin(target, 0f, oldScale_ * _scale);
            }


            virtual public void Awake()
            {

                close();
                _normal.SetActive(true);
            }

            public void inputDown()
            {
                close();
                _down.SetActive(true);
                //_target.sprite = _down;
                //   GDGeek.TweenScale.Begin(target, 0.05f, oldScale_);
            }

            public void inputUp()
            {
                close();
                if (enter_)
                {
                    _focus.SetActive(true);
                }
                else {

                    _normal.SetActive(true);
                }

                //_target.sprite = _normal;
                // GDGeek.TweenScale.Begin(target, 0f, oldScale_ * _scale);
            }
        }
    }
}
