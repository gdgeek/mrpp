using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Helper { 
    public class TimeStamp : MonoBehaviour
    {
        public static DateTime ConvertTimeStampToDateTime(long timeStamp)

        {

            DateTime defaultTime = new DateTime(1970, 1, 1, 0, 0, 0);

            long defaultTick = defaultTime.Ticks;

            long timeTick = defaultTick + timeStamp * 10000;

            //// 东八区 要加上8个小时

            DateTime dt = new DateTime(timeTick).AddHours(8);

            return dt;

        }

        public static long GetTimeStamp(long time)
        {


            return (time - (new DateTime(1970, 1, 1, 16, 0, 0)).ToUniversalTime().Ticks) / 10000;



        }
    }
}