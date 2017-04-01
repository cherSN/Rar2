//using Microsoft.Analytics.Interfaces;
//using Microsoft.Analytics.Types.Sql;
using MvvmDialogs.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MvvmDialogs.ViewModels
{
   public class EditCompanyDialogViewModel : IDialogViewModel
    {
        public bool Result { get; set; }
        public bool Show(IList<IDialogViewModel> collection)
        {
            collection.Add(this);
            Result = true;
            return this.Result;
        }
    }
}