using GalaSoft.MvvmLight;
using MvvmRar.Rar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmRar.ViewModel
{
    public class RarLicenseWrapper : ObservableObject
    {
        private RarLicense _License;

        public RarLicense License { get => _License; set => _License = value; }
        public string SeriesNumber { get => License.SeriesNumber; set => License.SeriesNumber = value; }
        public DateTime DateFrom { get => License.DateFrom; set => License.DateFrom = value; }
        public DateTime DateTo { get => License.DateTo; set => License.DateTo = value; }
        public string Issuer { get => License.Issuer; set => License.Issuer = value; }
        public string BusinesType { get => License.BusinesType; set => License.BusinesType = value; }

        public RarLicenseWrapper(RarLicense license)
        {
            _License = license;
        }
        public override string ToString()
        {
            return SeriesNumber + ";" + DateFrom.ToShortDateString() + "-" + DateTo.ToShortDateString();
        }
    }
}
