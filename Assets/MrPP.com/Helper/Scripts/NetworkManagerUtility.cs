using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Mirror;

namespace MrPP.Helper
{
    public static class NetworkManagerUtility
    {
        public static GameObject create(this NetworkManager manager, string name) {
            //return (from sq in NetworkManager.singleton.spawnPrefabs
            //                                 where sq.name == name
            //                                 select sq).FirstOrDefault();
            foreach (GameObject obj in NetworkManager.singleton.spawnPrefabs) {
                if (obj.name == name) {
                    return GameObject.Instantiate(obj);
                }
            }
            return null;
        }
    }
}