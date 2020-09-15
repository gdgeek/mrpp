using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Tracking
{
    public interface ITrackingHandler
    {
        TrackingMark.Mark[] marks {
            get;
        }

        GameObject gameObject {
            get;
        }

         void find(TrackingMark mark);
         void lost(TrackingMark mark);
    }
}