using Mirror.Discovery;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MrPP.Helper;
using Microsoft.MixedReality.Toolkit.Utilities;
using GDGeek;

namespace MrPP.Project
{
    public class HostList : MonoBehaviour
    {

        [SerializeField]
        GridObjectCollection _grid;
        List<ServerResponse> list_;
        [SerializeField]
        private HostButton _phototype;

        public Action<ServerResponse> onSelected {
            get;
            set;
        }

        List<GameObject> buttons_ = new List<GameObject>();
        internal void setData(List<ServerResponse> list)
        {
            list_ = list;
        }
        public void doClicked(IntId id) {
            onSelected?.Invoke(list_[id.value]);
        }
        internal void refresh()
        {
            foreach (GameObject obj in buttons_) {
                GameObject.DestroyImmediate(obj);
            }

            for(int i =0;i<list_.Count; ++i) {
                HostButton button = GameObject.Instantiate(_phototype);
                IntId id = button.gameObject.AskComponent<IntId>();
                id.value = i;
                button.transform.SetParent(this.transform);

            }
            _grid.UpdateCollection();
        }
    }
}