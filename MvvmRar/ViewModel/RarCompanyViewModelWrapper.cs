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

        public RarCompanyViewModelWrapper(RarCompany company) // : base(company)
        {
            _Company = company;
        }
        public RarCompany Company { get => _Company; set => _Company = value; }
        public string Name { get => Company.Name; set { Company.Name = value; RaisePropertyChanged("Name"); } }
        public string INN { get => Company.INN; set { Company.INN = value; RaisePropertyChanged("INN"); } }
        public string KPP { get => Company.KPP; set { Company.KPP = value; RaisePropertyChanged("KPP"); } }

        #region -Adress Properties -
        public string CountryId
        {
            get => (Company.Adress != null ? Company.Adress.CountryId : null);
            set
            {
                if (Company.Adress == null) Company.Adress = new RarAdress();
                Company.Adress.CountryId = value;
                RaisePropertyChanged("CountryId");
            }
        }
        public string PostCode
        {
            get => (Company.Adress != null ? Company.Adress.PostCode : null);
            set
            {
                if (Company.Adress == null) Company.Adress = new RarAdress();
                Company.Adress.PostCode = value;
                RaisePropertyChanged("PostCode");
            }
        }
        public string RegionId
        {
            get => (Company.Adress != null ? Company.Adress.RegionId : null);
            set
            {
                if (Company.Adress == null) Company.Adress = new RarAdress();
                Company.Adress.RegionId = value;
                RaisePropertyChanged("RegionId");
            }
        }
        public string District
        {
            get => (Company.Adress != null ? Company.Adress.District : null);
            set
            {
                if (Company.Adress == null) Company.Adress = new RarAdress();
                Company.Adress.District = value;
                RaisePropertyChanged("District");
            }
        }
        public string City
        {
            get => (Company.Adress != null ? Company.Adress.City : null);
            set
            {
                if (Company.Adress == null) Company.Adress = new RarAdress();
                Company.Adress.City = value;
                RaisePropertyChanged("City");
            }
        }
        public string Locality
        {
            get => (Company.Adress != null ? Company.Adress.Locality : null);
            set
            {
                if (Company.Adress == null) Company.Adress = new RarAdress();
                Company.Adress.Locality = value;
                RaisePropertyChanged("Locality");
            }
        }
        public string Street
        {
            get => (Company.Adress != null ? Company.Adress.Street : null);
            set
            {
                if (Company.Adress == null) Company.Adress = new RarAdress();
                Company.Adress.Street = value;
                RaisePropertyChanged("Street");
            }
        }
        public string Building
        {
            get => (Company.Adress != null ? Company.Adress.Building : null);
            set
            {
                if (Company.Adress == null) Company.Adress = new RarAdress();
                Company.Adress.Building = value;
                RaisePropertyChanged("Building");
            }
        }
        public string Block
        {
            get => (Company.Adress != null ? Company.Adress.Block : null);
            set
            {
                if (Company.Adress == null) Company.Adress = new RarAdress();
                Company.Adress.Block = value;
                RaisePropertyChanged("Block");
            }
        }
        public string Litera
        {
            get => (Company.Adress != null ? Company.Adress.Litera : null);
            set
            {
                if (Company.Adress == null) Company.Adress = new RarAdress();
                Company.Adress.Litera = value;
                RaisePropertyChanged("Litera");
            }
        }
        public string Apartment
        {
            get => (Company.Adress != null ? Company.Adress.Apartment : null);
            set
            {
                if (Company.Adress == null) Company.Adress = new RarAdress();
                Company.Adress.Apartment = value;
                RaisePropertyChanged("Apartment");
            }
        } 
        #endregion

        public override string ToString()
        {
            return Name + "; INN:" + INN + "; KPP:" + KPP;
        }

    }
}
