using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Common
{ 
    public abstract class QRCodeManager : GDGeek.Singleton<QRCodeManager>
    {
        public Action<string> onRecevie
        {
            get;
            set;
        }
      
        public abstract void open();
        public abstract void close();

    }
}