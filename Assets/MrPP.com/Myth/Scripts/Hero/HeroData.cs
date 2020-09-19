
using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

namespace MrPP.Myth {


    [Serializable]
    public struct HeroData : IEqualityComparer<HeroData>
    {
        [SerializeField]
        public string ip;
        [SerializeField]
        public string name;
        [SerializeField]
        public bool isServer;

        /*
        public uint id
        {
            get
            {

                if (string.IsNullOrEmpty(this.ip))
                {
                    return 0;
                }
                return BitConverter.ToUInt32(IPAddress.Parse(this.ip).GetAddressBytes(), 0);
            }
        }

    */

        public bool Equals(HeroData x, HeroData y)
        {
            if (x.ip == y.ip && x.name == y.name && x.isServer == y.isServer)
                return true;

            return false;
        }

        public int GetHashCode(HeroData obj)
        {
            return obj.GetHashCode();
        }
    }
}