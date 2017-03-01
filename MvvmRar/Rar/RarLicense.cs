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

        public RarLicense()
        {
            ID = "";
            SeriesNumber = "";
            Issuer = "";
            BusinesType = "";
            DateFrom = DateTime.Now;
            DateTo = DateTime.Now;

        }
        public override string ToString()
        {
            return SeriesNumber + "; " + DateFrom.ToShortDateString() + "-" + DateTo.ToShortDateString();
        }
    }
}
