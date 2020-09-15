
using System;
using System.Net;
using UnityEngine;

namespace MrPP.Myth {


    [Serializable]
    public struct HeroData
    {
   //     [SerializeField]
   //     public PlatformInfo.Type platform;
        [SerializeField]
        public string ip;
        [SerializeField]
        public string name;
        [SerializeField]
        public bool isServer;
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
    }
}