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

        public void GetData(Action<DataItem, Exception> callback)
        {
            // Use this to connect to the actual data service

            var item = new DataItem("Welcome to MVVM Light");
            callback(item, null);
        }
        public void GetCompanies(Action<List<RarCompany>, Exception> callback)
        {
            //            List<RarCompany> buyersList = RarFormF6Data.BuyersList;
            List<RarCompany> buyersList = new List<RarCompany>();
            buyersList.Add(new RarCompany() { Name = "ООО Одуванчик", CounryID = "643", INN = "7701010101", KPP = "770101011" });
            buyersList.Add(new RarCompany() { Name = "ООО Ромашка", CounryID = "643", INN = "7701010102", KPP = "770101012" });
            buyersList.Add(new RarCompany() { Name = "ООО василек", CounryID = "643", INN = "7701010103", KPP = "770101013" });

            callback(buyersList, null);
        }
    }
}