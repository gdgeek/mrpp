using MrPP.Myth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Network {
    public class PoseModel : MonoBehaviour, IPoseModel
    {

        [SerializeField]
        private GameObject _model;

        private IPoseModel model {
            get {
                return _model.GetComponent<IPoseModel>();
            }
        }
        public bool controler {
            get {
                return model.controler;
            }
        }

        public void askControl()
        {
            model.askControl();

        }

        public void unlocked()
        {
            model.unlocked();
        }

        public void update(Yggdrasil.AsgardPose aPose)
        {
            model.update(aPose);
        }

        public void setPose(Yggdrasil.AsgardPose aPose)
        {
            model.setPose(aPose);
        }

    }
}