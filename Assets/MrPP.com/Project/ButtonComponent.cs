using UnityEngine;


namespace MrPP.Project
{
    public abstract class ButtonComponent<D> : MonoBehaviour {
        public abstract void load(D data);
    }
}