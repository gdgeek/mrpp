using System.Collections.Generic;

namespace MrPP.Myth
{
    public class BridgeBroadcast : GDGeek.Singleton<BridgeBroadcast>
    {
        Dictionary<string, HashSet<IBridgeReceiver>> map_ = new Dictionary<string, HashSet<IBridgeReceiver>>();

        public void addReceiver(IBridgeReceiver receiver) {

            if (!map_.ContainsKey(receiver.handle)) {
                map_[receiver.handle] = new HashSet<IBridgeReceiver>();
            }
            map_[receiver.handle].Add(receiver);
        }

        public void removeReceiver(IBridgeReceiver receiver)
        {

            if (map_.ContainsKey(receiver.handle))
            {
                map_[receiver.handle].Remove(receiver);
            }
        }
        public void broadcast(string handle, string evt, object data)
        {
            if (map_.ContainsKey(handle)) {
                foreach (var receiver in map_[handle]) {
                    receiver.broadcast(evt, data);
                }
            }
        }
    }
}