using GDGeek;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Restful { 
    public class UriManager : GDGeek.Singleton<UriManager>
    {
      
        public Uri getUri(string key) {
            Transform ret = this.transform.Find("uri_" + key);

            if (ret != null)
            {
                return ret.gameObject.AskComponent<Uri>();
            }
            else {
                GameObject obj = new GameObject("uri_" + key);
                Uri uri = obj.AskComponent<Uri>();
                return uri;
            }

        }
    }
}