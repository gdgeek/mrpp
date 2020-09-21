using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Restful { 
    public class KVStation : GDGeek.Singleton<KVStation>
    {

        private Dictionary<string, HashSet<Listener>> _map = new Dictionary<string, HashSet<Listener>>();
        public interface Listener
        {
            string type { get; }
            void recevie(string data);

        }


        internal void broadcasting(string key, string value)
        {
            IEventFactory[] factories = this.gameObject.GetComponentsInChildren<IEventFactory>();

            foreach (var factory in factories)
            {
                if (factory.type.ToLower() == key.ToLower())
                {
                    factory.post(value);
                }
            }
               
            


        }
      
    }
}