using GDGeek;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MrPP.UX {
    public abstract class Toolbar : MonoBehaviour
    {
        public abstract void loading(ToolbarItem item);
        public abstract void unloading();
        public abstract void submenu(ToolbarItem item, List<ToolbarItemSubmenu.Item> datas);
        public abstract void closeSubmenu();
    }
}