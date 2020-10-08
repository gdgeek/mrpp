using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Restful {
    public class Station : GDGeek.Singleton<Station>
    {

        private uint last_ = 0;
        private Dictionary<string, HashSet<Listener>> _map = new Dictionary<string, HashSet<Listener>>();
        public interface Listener {
            string type { get; }
            void recevie(string data);

        }
      

        internal void broadcasting(Heartbeat.Data data)
        {
            IEventFactory[] factories = this.gameObject.GetComponentsInChildren<IEventFactory>();
            
            if (data.id > this.last_)
            {
                foreach (var factory in factories)
                {
                   // Debug.LogError("@" + factory.type);
                 //   Debug.LogError(data.type);
                    if (factory.type.ToLower() == data.type.ToLower())
                    {
                        factory.post(data.data);
                    }
                }
                last_ = data.id;
            }

           
        }
        internal void broadcasting(Heartbeat.Data[] datas)
        {
          //  Debug.LogError(datas.Length);
            IEventFactory[] factories = this.gameObject.GetComponentsInChildren<IEventFactory>();
            foreach (var data in datas) {

                if (data.id > this.last_) { 
                    foreach (var factory in factories) {
                        if (factory.type.ToLower() == data.type.ToLower()) {
                            factory.post(data.data);
                        }
                    }
                    last_ = data.id;
                }

            }
        }
    }
}