using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP { 
    public class LogManager : GDGeek.Singleton<LogManager>
    {

        [SerializeField]
        private LogDebug _debug;
        public LogDebug debug {
            get {
                return _debug;
            }
        }
    }
}