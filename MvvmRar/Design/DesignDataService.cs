using System;
using MvvmRar.Model;

namespace MvvmRar.Design
{
    public class DesignDataService : IDataService
    {
        public void GetData(Action<DataItem, Exception> callback)
        {
            // Use this to create design time data

            var item = new DataItem("Welcome __ to MVVM Light [design]");
            callback(item, null);
        }
        public void GetCompanies(Action<DataItem,Exception> callback)
        {
            var item = new DataItem("Welcome __ to MVVM Light [design]");
            callback(item, null);
        }
    }
}