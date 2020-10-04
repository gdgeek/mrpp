using System;
using GDGeek;
using TMPro;
using UnityEngine;
using MrPP.Helper;
namespace MrPP.UX
{
    public class BookItem : MonoBehaviour
    {
        public Action<IntId> onSelected { set; get; }

        [SerializeField]
        TextMeshPro _text;
        internal void setText(string text)
        {
            _text.text = text;
        }
        public void doSelected() {
            onSelected?.Invoke(this.gameObject.AskComponent<IntId>());
        }
    }
}