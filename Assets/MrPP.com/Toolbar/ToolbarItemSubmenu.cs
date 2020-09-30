using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.UX
{
    public class ToolbarItemSubmenu : MonoBehaviour, IToolbarItemExecuter
    {
        [System.Serializable]
        public class Item{
            public string name;
            public string icon;
            public Action action;
            public Color color = Color.white;
        }
        [SerializeField]
        public List<Item> datas = new List<Item>();

        public void Awake()
        {
            datas.Clear();
            datas.Add(new Item
            {
                name = "test1a",
                action = delegate {
                    Debug.Log("cool1");
                }
            });
            datas.Add(new Item
            {
                name = "test2b",
                action = delegate {
                    Debug.Log("cool2");
                }
            });
            datas.Add(new Item
            {
                name = "test3c",
                action = delegate {
                    Debug.Log("cool3");
                }
            });
        }
        public void execute()
        {
            ToolbarManager.Instance.toolbar.submenu(this.gameObject.GetComponent< ToolbarItem >(), datas);
           
        }


       
    }
}
