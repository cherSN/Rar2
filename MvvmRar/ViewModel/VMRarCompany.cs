using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmRar.Rar;
using System.ComponentModel;

namespace MvvmRar.ViewModel
{
    public class VMRarCompany: RarCompany, INotifyPropertyChanged
    {
        public VMRarCompany() : base() { }

        public VMRarCompany(RarCompany company) : base()
        {
            Name = company.Name;
            INN = company.INN;
            KPP = company.KPP;
        }


        public event PropertyChangedEventHandler PropertyChanged;
  
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
