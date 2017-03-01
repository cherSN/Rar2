using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmRar.Rar
{
    public class RarCompany
    {
        #region - Public Properties -
        public string ID { set; get; }
        public string Name { set; get; }
        public string INN { set; get; }
        public string KPP { set; get; }

        public bool IsUsed { set; get; }

        public RarAdress Adress { set; get; }
        public List<RarLicense> LicenseList { set; get; }
        #endregion

        #region - Constructors -
        public RarCompany() 
        {
            ID = "";
            Name = "";
            INN = "";
            KPP = "";
            IsUsed = false;

            Adress = new RarAdress();
            LicenseList = new List<RarLicense>();
        }
        public RarCompany(string id, string name, string inn, string kpp, RarAdress adress) 
        {
            ID = id;
            Name = name;
            INN = inn;
            KPP = kpp;
            IsUsed = false;
            Adress = adress;
            LicenseList = new List<RarLicense>();
        }
        #endregion

        public override string ToString() { return Name + " ИНН:" + INN + "; КПП:" + KPP; }
    }
}
