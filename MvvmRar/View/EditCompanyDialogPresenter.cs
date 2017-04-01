using MvvmRar.View;
using MvvmRar.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MvvmDialogs.ViewModels;

namespace MvvmDialogs.Presenters
{
    public class EditCompanyDialogPresenter : IDialogBoxPresenter<EditCompanyDialogViewModel>
    {
        public void Show(EditCompanyDialogViewModel vm)
        {
            RarCompanyView v = new RarCompanyView();
            v.Show();
            //vm.Result = true;
        }
    }
}