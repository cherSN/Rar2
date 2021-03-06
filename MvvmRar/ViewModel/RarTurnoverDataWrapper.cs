﻿using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmRar.Rar;

namespace MvvmRar.ViewModel
{
    public class RarTurnoverDataWrapper : ObservableObject
    {
        private RarTurnoverData _TurnoverData;
        private RarCompanyWrapper _Manufacturer;
        private RarCompanyWrapper _Buyer;
        private RarLicenseWrapper _License;

        public RarTurnoverDataWrapper(RarTurnoverData turnoverData) {
            _TurnoverData = turnoverData;
            _Manufacturer = new RarCompanyWrapper(turnoverData.Manufacturer);
            _Buyer = new RarCompanyWrapper(turnoverData.Buyer);
            _License = new RarLicenseWrapper(turnoverData.License);
        }

        public string AlcoCode { get => _TurnoverData.AlcoCode; set { _TurnoverData.AlcoCode = value; RaisePropertyChanged("AlcoCode"); } }
        public DateTime NotificationDate { get => _TurnoverData.NotificationDate; set { _TurnoverData.NotificationDate = value; RaisePropertyChanged("NotificationDate"); } }
        public string NotificationNumber { get => _TurnoverData.NotificationNumber; set { _TurnoverData.NotificationNumber = value; RaisePropertyChanged("NotificationNumber"); } }
        public double NotificationTurnover { get => _TurnoverData.NotificationTurnover; set { _TurnoverData.NotificationTurnover = value; RaisePropertyChanged("NotificationTurnover"); } }
        public DateTime DocumentDate { get => _TurnoverData.DocumentDate; set { _TurnoverData.DocumentDate = value; RaisePropertyChanged("DocumentDate"); } }
        public string DocumentNumber { get => _TurnoverData.DocumentNumber; set { _TurnoverData.DocumentNumber = value; RaisePropertyChanged("DocumentNumber"); } }
        public string CustomsDeclarationNumber { get => _TurnoverData.CustomsDeclarationNumber; set { _TurnoverData.CustomsDeclarationNumber = value; RaisePropertyChanged("CustomsDeclarationNumber"); } }
        public double Turnover { get => _TurnoverData.Turnover; set { _TurnoverData.Turnover = value; RaisePropertyChanged("Turnover"); } }

        public RarCompanyWrapper Manufacturer { get => _Manufacturer; set { _Manufacturer = value; RaisePropertyChanged("Manufacturer");  } }
        public RarCompanyWrapper Buyer { get =>  _Buyer; set { _Buyer = value; RaisePropertyChanged("Buyer"); } }
        public RarLicenseWrapper License { get => _License; set { _License = value; RaisePropertyChanged("License"); } }
    }
}
