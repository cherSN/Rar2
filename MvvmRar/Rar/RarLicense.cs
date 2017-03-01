using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmRar.Rar
{
    public class RarLicense
    {
        #region - Public Properties -
        public string ID { set; get; }
        public string SeriesNumber { set; get; }
        public DateTime DateFrom { set; get; }
        public DateTime DateTo { set; get; }
        public string Issuer { set; get; }
        public string BusinesType { set; get; }
        #endregion

        #region - Public Constructor - 
        public RarLicense()
        {
            //ID = "";
            //SeriesNumber = "";
            //Issuer = "";
            //BusinesType = "";
            //DateFrom = DateTime.Now;
            //DateTo = DateTime.Now;
        }

        public RarLicense(RarLicense license)
        {
            ID = license.ID;
            SeriesNumber = license.SeriesNumber;
            Issuer = license.Issuer;
            BusinesType = license.BusinesType;
            DateFrom = license.DateFrom;
            DateTo = license.DateTo;
        } 
        #endregion

        public override string ToString()
        {
            return SeriesNumber + "; " + DateFrom.ToShortDateString() + "-" + DateTo.ToShortDateString();
        }
    }
}
