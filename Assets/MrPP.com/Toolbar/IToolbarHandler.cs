using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.UX {
    public interface IToolbarHandler  {


        Action onEnable
        {
            get;
            set;
        }
        Action onDisable
        {
            get;
            set;
        }
        Action<Transform> onTransforming {
            get;
            set;
        }
        Action<Transform> onTransformed {
            get;
            set;
        }
        GameObject gameObject {
            get;
        }
        Transform transform {
            get;
        }
        ToolbarItem customToolbar {
            get;
        }
        IToolbarTemplate template { get; set; }

        void disable();
        void enable();
        void transforming();
        void transformed();


    }
}