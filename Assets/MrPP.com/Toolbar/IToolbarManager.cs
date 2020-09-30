using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.UX { 
    public interface IToolbarManager{
        void enable(IToolbarHandler handler);
        void disable(IToolbarHandler handler);
      

    }
}