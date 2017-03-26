using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmRar.Rar
{
    public class RarFormF6
    {
        #region - Public Properties -
        public DateTime DocumentDate { set; get; }
        public string Version { set; get; }
        public string ProgramName { set; get; }
        public string FormNumber { set; get; }
        public string ReportPeriod { set; get; }
        public string ReportYear { set; get; }
        public string CorrectionNumber { set; get; }
        public RarOurCompany OurCompany { set; get; }
        public List<RarCompany> BuyerList { set; get; }
        public List<RarCompany> ManufacturerList { set; get; }
        public List<RarTurnoverData> TurnoverDataList { set; get; }
        #endregion
        #region - Constructor -
        public RarFormF6()
        {
            OurCompany = new RarOurCompany();
            BuyerList = new List<RarCompany>();
            ManufacturerList = new List<RarCompany>();
            TurnoverDataList = new List<RarTurnoverData>();
        }
        #endregion
    }
}
