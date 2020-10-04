using MrPP.Input;
using System;
using UnityEngine;

namespace MrPP.Helper
{
    public class GazeTarget:MonoBehaviour
    {
        public void focusExit()
        {
            IInputHandler[] inputs = this.gameObject.GetComponents<IInputHandler>();
            foreach (var input in inputs)
            {
                input.focusExit();
            }
        }

        public void focusEnter()
        {
            IInputHandler[] inputs = this.gameObject.GetComponents<IInputHandler>();
            foreach (var input in inputs)
            {
                input.focusEnter();
            }
        }
    }
}