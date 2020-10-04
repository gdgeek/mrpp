using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace MrPP.Helper
{
    public class DataTask<T> : GDGeek.Task, IDataTask<T>
    {

        private bool over_ = false;
        public bool over {
            get { return over_; }
            set { over_ = value; }
        }
        public DataTask()
        {
            this.isOver = delegate
            {
                return over;
            };
        }
        private T data_;
        public T data { 
            get {

                return data_;
            } 
            
        }
        public void setData(T data) {
            data_ = data;
            over = true;
        }

        public GDGeek.Task task
        {
            get
            {
                return this;
            }
        }
    }
    
  
}