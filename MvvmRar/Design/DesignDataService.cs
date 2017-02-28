using System;
using MvvmRar.Model;
using MvvmRar.Rar;
using System.Collections.Generic;

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
        public void GetCompanies(Action<List<RarCompany>, Exception> callback)
        {
            List<RarCompany> buyersList = new List<RarCompany>();
            buyersList.Add(new RarCompany() { Name = "ООО Одуванчик", CounryID="643", INN="7701010101", KPP="770101011"  });
            buyersList.Add(new RarCompany() { Name = "ООО Ромашка", CounryID = "643", INN = "7701010102", KPP = "770101012" });
            buyersList.Add(new RarCompany() { Name = "ООО Василек", CounryID = "643", INN = "7701010103", KPP = "770101013" });

            callback(buyersList, null);
        }
    }
}