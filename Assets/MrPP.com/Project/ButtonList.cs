using GDGeek;
using Microsoft.MixedReality.Toolkit.Utilities;
using MrPP.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace MrPP.Project
{
    public class ButtonList<T, B> : MonoBehaviour  where B: ButtonComponent<T>
    {
        [SerializeField]
        GridObjectCollection _grid;
        List<T> list_;
        [SerializeField]
        private B _phototype;

        public Action<T> onSelected
        {
            get;
            set;
        }

        List<B> buttons_ = new List<B>();
        internal void setDatas(List<T> list)
        {
            if (list_ == null || !list_.SequenceEqual(list))
            {
                list_ = list;
                refresh();
            }
        }
        public void doClicked(IntId id)
        {
//            Debug.LogError(id.value);
            onSelected?.Invoke(list_[id.value]);
        }
        private void refresh()
        {
            foreach (B button in buttons_)
            {
                GameObject.DestroyImmediate(button.gameObject);
            }
            buttons_.Clear();

            for (int i = 0; i < list_.Count; ++i)
            {
                B button = GameObject.Instantiate(_phototype, this.transform, true);
                button.load(list_[i]);
                button.gameObject.SetActive(true);
                IntId id = button.gameObject.AskComponent<IntId>();
                id.value = i;
                buttons_.Add(button);
               // button.transform.SetParent(this.transform);

            }
            _grid.UpdateCollection();
        }
    }
}