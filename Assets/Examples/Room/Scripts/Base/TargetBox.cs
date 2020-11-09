using MrPP.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Game { 
    public class TargetBox : MonoBehaviour
    {
        [SerializeField]
        private TargetShow _show;
        public Target target {
            private set;
            get;
        }
        internal TargetBox create(Target item, Transform tsf)
        {
            TargetBox box = GameObject.Instantiate(this, tsf);
            box.setup(item);
            return box;
        }

        private void setup(Target item)
        {
            this.target = item;
            _show.show(item.transform);
        }
    }
}