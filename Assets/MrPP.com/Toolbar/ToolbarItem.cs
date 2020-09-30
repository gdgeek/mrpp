using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace MrPP.UX { 
    public class ToolbarItem : MonoBehaviour {
        [System.Serializable]
        public class Icon {

            [SerializeField]
            private string _key;
            public string key
            {
                get
                {
                    return _key;
                }
                set {
                    _key = value;
                }
            }
        
            [SerializeField]
            private string _title;
            public string title
            {
                get
                {
                    return _title;
                }
                set {
                    _title = value;
                }
            }
        }

        public void execute()
        {
            ToolbarManager.Instance.toolbar.closeSubmenu();
            IToolbarItemExecuter[] executes = this.gameObject.GetComponents<IToolbarItemExecuter>();
            foreach (IToolbarItemExecuter exe in executes) {
                exe.execute();
            }
        }

      


        [SerializeField]
        private Icon _icon;
        public Icon icon
        {
            get
            {
                return _icon;
            }
        }
        public void setup(string key, string title) {
            if (_icon == null) {
                _icon = new Icon();
            }
            _icon.key = key;
            _icon.title = title;
        }
      
        public ToolbarItem parent{
            get{
                if (this.transform.parent == null) {
                    return null;
                }
                return this.transform.parent.GetComponent<ToolbarItem>();
            }
        }

        [SerializeField]
        private List<ToolbarItem> _children = new List<ToolbarItem>();
        public List<ToolbarItem> children
        {
            get
            {
                return _children;
            }
        }

    }
}