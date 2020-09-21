using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MrPP.Myth.Yggdrasil;

namespace MrPP.Network
{
    public interface IPoseModel
    {
        bool controler { get; }

        void askControl();
        void unlocked();
        void update(AsgardPose aPose);
        void setPose(AsgardPose aPose);
    }
}