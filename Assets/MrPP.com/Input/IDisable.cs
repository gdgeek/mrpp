using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP {
    namespace Input { 
        public interface IDisable {
            void onEnabled();
            void onDisabled();
        }

    }
}