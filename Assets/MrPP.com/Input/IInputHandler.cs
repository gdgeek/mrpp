using GDGeek;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Input { 
   
    public interface IInputHandler
    {
        void focusEnter();
        void focusExit();
        void inputDown();
        void inputUp();
    }
}