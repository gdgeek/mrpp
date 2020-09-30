using System;
using System.Collections;
using System.Collections.Generic;
using GDGeek;
using Microsoft.MixedReality.Toolkit.Utilities;
using TMPro;
using UnityEngine;
using MrPP.Helper;
namespace MrPP.UX
{
    public class Book : MonoBehaviour
    {  

        public Action<int> onSelected { set; get; }
        [SerializeField]
        GridObjectCollection _offset;
        [SerializeField]
        private BookItem _phototype;
        [SerializeField]
        private int _count = 12;
        public int count {
            get {
                return _count;
            }
        }
        private List<string> items_;
        [SerializeField]
        private int page_ = 0;

        public int page{ get { return page_; } set {  page_ = value; } }
        private int max { get; set; } = 1;

        [SerializeField]
        private Button _last;
        [SerializeField]
        private Button _next;

        [SerializeField]
        TextMeshPro _text;


        public void setItems(List<string> items){
            items_ = items;
            max = Math.Max(0, (items.Count - 1) / count + 1);
        }

        public void doSelected(IntId id) {
            onSelected?.Invoke(id.value);
        }
        public void refresh(){
          //  Debug.LogError("page" + page);
            if (page == 0)
            {
                _last.state = Button.State.Disable;
            }
            else {

                _last.state = Button.State.Enabled;
            }
            if (page + 1 >= max)
            {
                _next.state = Button.State.Disable;
            }
            else {
                _next.state = Button.State.Enabled;
            }
            _last.refresh();
            _next.refresh();

            _text.text = (page+1).ToString() + "/" + max.ToString();
            BookItem[] items = _offset.GetComponentsInChildren<BookItem>();
            foreach (var item in items) {
                item.gameObject.SetActive(false);
                GameObject.Destroy(item.gameObject);
            }
            List<Transform> list = new List<Transform>();
            for (int i = page * count; i<items_.Count && i < (page+1) * count; ++i) {
                string item = items_[i];
                var bi = GameObject.Instantiate(_phototype);
                bi.setText(item);
                bi.transform.parent = _offset.transform;
                bi.gameObject.SetActive(true);
                bi.transform.localScale = Vector3.one;
                bi.transform.localRotation = Quaternion.identity;
                bi.onSelected += doSelected;
                IntId id = bi.gameObject.AskComponent<IntId>();
                id.value = i;
            }
          
            _offset.UpdateCollection();

        }
    }
}