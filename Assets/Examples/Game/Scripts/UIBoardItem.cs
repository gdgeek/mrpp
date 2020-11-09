using GDGeek;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MrPP.Game { 
    public class UIBoardItem : MonoBehaviour, IExecute
    {
        [SerializeField]
        private string _title;

        [SerializeField]
        private Material _material;

        [SerializeField]
        private Renderer[] _renderers;

        [SerializeField]
        private Text _text;
        [SerializeField]
        private Transform _checked;
        public void execute()
        {
            _text.text = _title;
            foreach (var renderer in _renderers) {
                renderer.sharedMaterial = _material;
            }
        }

        public void open()
        {
            _checked.gameObject.SetActive(true);
        }

        public void close()
        {
            _checked.gameObject.SetActive(false);
        }
        public void Start() {
            close();
        }
    }
}