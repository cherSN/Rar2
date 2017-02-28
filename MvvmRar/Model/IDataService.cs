using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvvmRar.Rar;

namespace MvvmRar.Model
{
    public interface IDataService
    {
        void GetData(Action<DataItem, Exception> callback);
        void GetCompanies(Action<List<RarCompany>, Exception> callback);
    }
}
