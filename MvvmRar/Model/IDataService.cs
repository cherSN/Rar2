using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvvmRar.Rar;

namespace MvvmRar.Model
{
    public interface IDataService
    {
        void GetData(Action<RarFormF6, Exception> callback);
    }
}
