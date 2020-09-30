using GDGeek;
using Microsoft.MixedReality.Toolkit.UI;
using MrPP.UX;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MrPP.UX
{
    public class Button : MonoBehaviour, IExecute
    {
        public enum State { 
            Enabled,
            Selected,
            Disable,
        }


        // [SerializeField]
        //  PressableButtonHoloLens2 _pressable;
        [SerializeField]
        Interactable _interactable;
        [SerializeField]
        private TextMeshPro _tipUI;
        [SerializeField]
        private GameObject _tip;
        [SerializeField]
        private GameObject _selected;
        [SerializeField]
        private GameObject _disabled;
        [SerializeField]
        private GameObject _enabled;
        [SerializeField]
        private string _command = "选择";
        // [SerializeField]
        //    private BoxCollider _collider;
        //

        [SerializeField]
        private State _state = Button.State.Enabled;
        public State state {
            set {
                _state = value;
            }
        }


        [SerializeField]
        private TextMeshPro _textUI; 
        [SerializeField]
        private string _text;
        public string text {
         
            set {
                _text = value;
            }
        }
        [SerializeField]
        private TextMeshPro _iconUI;
        [SerializeField]
        private string _icon;
        public string icon
        {

            set
            {
                _icon = value;
            }
        }
        public void refresh() {
            _textUI.text = _text;
            _iconUI.text = FontIconManager.Instance.getFontIcon(_icon).ToString();

            _tipUI.text = "说:'" + _command + "'";
            if (_state == State.Selected)
            {
             //   Debug.LogError("sel" + this.gameObject.name);
                _tip.SetActive(false);
                _interactable.enabled = true;
                _selected.SetActive(true);

                _disabled.SetActive(false);
                _enabled.SetActive(false);
                Collider collder = this.gameObject.GetComponent<Collider>();
                if (collder) { collder.enabled = true; }
            }
            else if (_state == State.Enabled)
            {
                //Debug.LogError("ena" + this.gameObject.name);
                _tip.SetActive(true);
                _interactable.enabled = true;
                _selected.SetActive(false);

                Collider collder = this.gameObject.GetComponent<Collider>();
                if (collder) { collder.enabled = true; }
                _disabled.SetActive(false);
                _enabled.SetActive(true);
            }
            else
            {
                _tip.SetActive(false);
                _interactable.enabled = false;
                _selected.SetActive(false);
                _disabled.SetActive(true);
                _enabled.SetActive(false);
                Collider collder = this.gameObject.GetComponent<Collider>();
                if (collder) { collder.enabled = false; }

            }
        }

        public void execute()
        {
            refresh();
        }
    }
}