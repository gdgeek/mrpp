using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Input
{
    public interface ICountdown
    {

        void close();
        void open();
        void percent(float per);
    }
}
