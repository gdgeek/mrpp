using System;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Tracking
{
    public class TrackingHandler : MonoBehaviour, ITrackingHandler
    {
        [SerializeField]
        private List<TrackingMark.Mark> _marks = new List<TrackingMark.Mark>();
        public void clear() {
            _marks.Clear();
        }
        public void addMaker(TrackingMark.Mark mark)
        {
            _marks.Add(mark);
        }
        public TrackingMark.Mark[] marks => _marks.ToArray();

       // public GameObject gameObject => this.gameObject;

        public void Start() {
            TrackingManager.Instance.addHandler(this);
        }
        public void OnDestroy() {
            if (TrackingManager.IsInitialized) { 
                TrackingManager.Instance.removeHandler(this);
            }
        }
        public Action<Transform> onFind { get; set; }
        public Action<Transform> onLost { get; set; }

        public void find(TrackingMark mark)
        {
            onFind?.Invoke(mark.transform);
        }

        public void lost(TrackingMark mark)
        {
            onLost?.Invoke(mark.transform);

        }
    }
}