﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmRar.Rar
{
    public class RarTurnoverData
    {
        #region - Public Properties -
        public string AlcoCode { set; get; }
        public DateTime NotificationDate { set; get; }
        public string NotificationNumber { set; get; }
        public double NotificationTurnover { set; get; }
        public DateTime DocumentDate { set; get; }
        public string DocumentNumber { set; get; }
        public string CustomsDeclarationNumber { set; get; }
        public double Turnover { set; get; }
        public RarSubdevision Subdevision { set; get; }
        public RarCompany Manufacturer { set; get; }
        public RarCompany Buyer { set; get; }
        public RarLicense License { set; get; }
        #endregion
        #region - Constructor -
        public RarTurnoverData()
        {
            Subdevision = new RarSubdevision();
            Manufacturer = new RarCompany();
            Buyer = new RarCompany();
            License = new RarLicense();
        }

        public RarTurnoverData(RarTurnoverData turnoverData)
        {
            AlcoCode = turnoverData.AlcoCode;
            NotificationDate = turnoverData.NotificationDate;
            NotificationNumber = turnoverData.NotificationNumber;
            NotificationTurnover = turnoverData.NotificationTurnover;
            DocumentDate = turnoverData.DocumentDate;
            DocumentNumber = turnoverData.DocumentNumber;
            CustomsDeclarationNumber = turnoverData.CustomsDeclarationNumber;
            Turnover = turnoverData.Turnover;
            Subdevision = turnoverData.Subdevision;
            Manufacturer = turnoverData.Manufacturer;
            Buyer = turnoverData.Buyer;
            License = turnoverData.License;
        }
        #endregion
    }
}
