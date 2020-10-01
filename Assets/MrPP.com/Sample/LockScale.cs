using GDGeek;
using UnityEngine;

namespace MrPP.SampleLib
{
    public class LockScale: MonoBehaviour
    {
        Vector3 scale;
        IsChanged changed = new IsChanged();
        void Awake() {
            //tsm = new TransformData(this.transform, TransformData.Type.World);
            scale = this.transform.lossyScale;
        }
        public void Update() {
            if (scale != this.transform.lossyScale) {
                this.transform.setGlobalScale(scale);
               // tsm.write(this.transform, TransformData.Type.World);
            }
        }   
    }
}