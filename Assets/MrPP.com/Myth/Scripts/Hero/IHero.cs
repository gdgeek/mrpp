using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Myth {
    public interface IHero
    {
   

        uint netId { get; }
        HeroData data { get; }
        HeroState state { get;}
        void ready();
        Transform transform { get; }
        bool isLocalPlayer { get; }
    }
}