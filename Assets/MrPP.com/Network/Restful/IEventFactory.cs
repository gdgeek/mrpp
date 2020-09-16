using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrPP.Restful
{
    public interface IEventFactory
    {
        void post(string data);
        string type { get; }
    }   
    
}