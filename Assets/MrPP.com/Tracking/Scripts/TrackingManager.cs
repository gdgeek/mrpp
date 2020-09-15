using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Tracking
{ 
    public class TrackingManager : GDGeek.Singleton<TrackingManager>
    {
        protected Dictionary<TrackingMark.Mark, HashSet<ITrackingHandler>> _map = new Dictionary<TrackingMark.Mark, HashSet<ITrackingHandler>>();
        public virtual void addHandler(ITrackingHandler handler)
        {
          //  Debug.LogError("TrackingManager add " + handler.gameObject.name);
            TrackingMark.Mark[] marks = handler.marks;
            foreach (TrackingMark.Mark mark in marks)
            {
                if (!_map.ContainsKey(mark))
                {
                    _map[mark] = new HashSet<ITrackingHandler>();
                }
                _map[mark].Add(handler);
            }
        }

        public virtual void removeHandler(ITrackingHandler handler)
        {
           // Debug.LogError("TrackingManager remove " + handler.gameObject.name);
            TrackingMark.Mark[] marks = handler.marks;
            foreach (TrackingMark.Mark mark in marks)
            {
                if (_map.ContainsKey(mark))
                {
                    _map[mark].Remove(handler);
                    // _map[mark] = new HashSet<VuforiaHandler>();
                }

            }
        }
        public virtual void close() { 
        
        }


    }
}