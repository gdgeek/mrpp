using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Helper
{
    public class JustEditor : MonoBehaviour
    {
     


#if !UNITY_EDITOR
        // Use this for initialization
        void Awake()
        {
                DestroyImmediate(this.gameObject);
        }

#endif

    }
}