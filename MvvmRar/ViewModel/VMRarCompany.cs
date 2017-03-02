using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmRar.Rar;
using System.ComponentModel;
using System.Reflection;

namespace MvvmRar.ViewModel
{
    public class VMRarCompany: RarCompany, INotifyPropertyChanged
    {
        public VMRarCompany() : base() { }

        public VMRarCompany(RarCompany company) : base(company)
        {
            //var properties = GetType().GetProperties().Select(prop => new { prop.Name, Value = prop.GetValue(company) });
            //foreach (var item in properties)
            //{
            //    PropertyInfo pi = this.GetType().GetProperty((string)item.Name);
            //    pi.SetValue(this, item.Value);
            //}
        }


        public event PropertyChangedEventHandler PropertyChanged;
  
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
