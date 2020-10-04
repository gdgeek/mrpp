using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.UX { 
    public abstract class ToolbarManager : GDGeek.Singleton<ToolbarManager>, IToolbarManager
    {
        public abstract void disable(IToolbarHandler handler);
        public abstract void enable(IToolbarHandler handler);
        public abstract IToolbarHandler getHandler();
        public abstract Toolbar toolbar { get; }
        
    }
}