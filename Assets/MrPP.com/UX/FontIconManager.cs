using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.UX { 
    public class FontIconManager : GDGeek.Singleton<FontIconManager>, GDGeek.IExecute
    {

        [SerializeField]
        private TextAsset _json;

        [System.Serializable]
        class KeyValue {
            public string key = null;
            public int value = 0;
        }
        [System.Serializable]
        class Data {
            public KeyValue[] list = null; 
        }

       
        private Dictionary<string, char> map_ = null;
        private Dictionary<string, char> map {
            get {
                if (map_ == null) {
                    map_ = new Dictionary<string, char>();
                    execute();
                    Debug.Log(_json.text);
                    Data data = JsonUtility.FromJson<Data>(_json.text);
                    foreach (var kv in data.list)
                    {
                        
                        map_.Add(kv.key, (char)(kv.value));
                    }
                }
                return map_;
            }
        }
      
        

        public char getFontIcon(string key) {


           
            if (key != null)
            {
                var m = map;
                if (m.ContainsKey(key))
                {
                    return m[key];
                }
               
            }
            Debug.Log(0);
            return '\0';
        }

        public void execute()
        {
            if (_json == null)
            {
                _json = Resources.Load<TextAsset>("Font/font.json");
            }
        }
    }
}