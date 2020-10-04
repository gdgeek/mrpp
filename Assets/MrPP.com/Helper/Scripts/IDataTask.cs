using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace MrPP.Helper
{
    public interface IDataTask<T>
    {

        GDGeek.Task task { get; }
        T data { get; }
        void setData(T data);

    }
     
    
}