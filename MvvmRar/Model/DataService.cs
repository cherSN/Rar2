using System;
using System.Collections.Generic;
using MvvmRar.Rar;

namespace MvvmRar.Model
{
    public class DataService : IDataService
    {
        public Rar.RarFormF6 RarFormF6Data { set; get; }

        public DataService()
        {
            RarFormF6Data = new Rar.RarFormF6();
        }

        public void GetData(Action<RarFormF6, Exception> callback)
        {
            callback(RarFormF6Data, null);
        }


    }
}