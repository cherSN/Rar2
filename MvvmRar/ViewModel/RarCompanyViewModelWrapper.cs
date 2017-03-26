using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmRar.Rar;
using System.ComponentModel;
using System.Reflection;
using GalaSoft.MvvmLight;

namespace MvvmRar.ViewModel
{
    public class RarCompanyViewModelWrapper : ObservableObject//RarCompany, INotifyPropertyChanged
    {
        private RarCompany _Company;
        
        //public RarCompanyViewModelWrapper() : base() { }

        public RarCompanyViewModelWrapper(RarCompany company) // : base(company)
        {
            Company = company;
        }

        public string Name { get => Company.Name; set  { Company.Name = value; RaisePropertyChanged("Name"); } }
        public string INN { get => Company.INN; set { Company.INN = value; RaisePropertyChanged("INN"); } }
        public string KPP { get => Company.KPP; set { Company.KPP = value; RaisePropertyChanged("KPP"); } }

        public RarCompany Company { get => _Company; set => _Company = value; }

        public override string ToString() {
            return Name;
        }

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected virtual void RaisePropertyChanged(string propertyName)
        //{
        //    if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}
