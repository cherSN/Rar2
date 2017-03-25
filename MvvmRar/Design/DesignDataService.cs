using System;
using MvvmRar.Model;
using MvvmRar.Rar;
using System.Collections.Generic;

namespace MvvmRar.Design
{
    public class DesignDataService : IDataService
    {
        public Rar.RarFormF6 RarFormF6Data { set; get; }

        private void SetFakeData()
        {
            RarFormF6Data.DocumentDate = DateTime.Parse("23.01.2017");
            RarFormF6Data.Version = "4.31";
            RarFormF6Data.ProgramName = "1С:ПРЕДПРИЯТИЕ 8.3 УТ 11.2.3.203";
            RarFormF6Data.FormNumber = "06";
            RarFormF6Data.ReportPeriod = "0";
            RarFormF6Data.ReportYear = "2013";
            RarFormF6Data.CorrectionNumber = "";

            RarFormF6Data.OurCompany.Name = "Наша фирма";

            RarAdress adress1 = new RarAdress("643","101111","77", "Московский район", "Москва", "Московский район", "Уличная ул","4б","2/1","А","1");
            RarAdress adress2 = new RarAdress("643", "101111", "77", "Московский район", "Москва", "Московский район", "Уличная ул", "4б", "2/1", "А", "2");
            RarAdress adress3 = new RarAdress("643", "101111", "77", "Московский район", "Москва", "Московский район", "Уличная ул", "4б", "2/1", "А", "3");


            RarSubdevision subdevision = new RarSubdevision() { Name = "Основное", KPP = "770101001" };
            RarLicense license = new RarLicense()
            {
                SeriesNumber = "00009",
                DateFrom = DateTime.Parse("01.01.2015"),
                DateTo = DateTime.Parse("01.01.2018"),
                Issuer = "РАР"
            };

            RarFormF6Data.BuyerList.Clear();
            RarCompany buyer1 = new RarCompany() { Name = "ООО Одуванчик",  INN = "7701010101", KPP = "770101011", Adress=adress1, ID="01", IsUsed=false};
            buyer1.LicenseList.Add(license);
            RarCompany buyer2 = new RarCompany() { Name = "ООО Ромашка", INN = "7701010102", KPP = "770101012" };
            RarCompany buyer3 = new RarCompany() { Name = "ООО Василек",  INN = "7701010103", KPP = "770101013" };
            RarFormF6Data.BuyerList.Add(buyer1);
            RarFormF6Data.BuyerList.Add(buyer2);
            RarFormF6Data.BuyerList.Add(buyer3);

            RarFormF6Data.ManufacturerList.Clear();
            RarCompany manufacturer1 = new RarCompany { Name = "Сибирский ЛВЗ", INN = "7701010101", KPP = "770101001" };
            RarCompany manufacturer2 = new RarCompany { Name = "Производитель 2", INN = "7701010102", KPP = "770101002" };
            RarCompany manufacturer3 = new RarCompany { Name = "Производитель 3", INN = "7701010103", KPP = "770101003" };
            RarFormF6Data.ManufacturerList.Add(manufacturer1);
            RarFormF6Data.ManufacturerList.Add(manufacturer2);
            RarFormF6Data.ManufacturerList.Add(manufacturer3);




            RarFormF6Data.TurnoverDataList.Clear();
            RarFormF6Data.TurnoverDataList.Add(new RarTurnoverData()
            {
                AlcoCode = "200",
                NotificationDate = DateTime.Parse("01.01.2017"),
                NotificationNumber = "213",
                NotificationTurnover = 0.005,
                DocumentDate = DateTime.Parse("01.01.2017"),
                DocumentNumber = "232",
                CustomsDeclarationNumber = "123213/09898/78979",
                Turnover = 0.005,
                Buyer = buyer1,
                Manufacturer = manufacturer1,
                Subdevision = subdevision,
                License = license
            });

            RarFormF6Data.TurnoverDataList.Add(new RarTurnoverData()
            {
                AlcoCode = "200",
                NotificationDate = DateTime.Parse("01.01.2017"),
                NotificationNumber = "213",
                NotificationTurnover = 0.006,
                DocumentDate = DateTime.Parse("01.02.2017"),
                DocumentNumber = "238",
                CustomsDeclarationNumber = "123213/09898/78979",
                Turnover = 0.006,
                Buyer = buyer1,
                Manufacturer = manufacturer2,
                Subdevision = subdevision,
                License = license
            });

            RarFormF6Data.TurnoverDataList.Add(new RarTurnoverData()
            {
                AlcoCode = "200",
                NotificationDate = DateTime.Parse("01.01.2017"),
                NotificationNumber = "212",
                NotificationTurnover = 0.008,
                DocumentDate = DateTime.Parse("01.03.2017"),
                DocumentNumber = "432",
                CustomsDeclarationNumber = "123213/09898/78979",
                Turnover = 0.008,
                Buyer = buyer2,
                Manufacturer = manufacturer3,
                Subdevision = subdevision,
                License = license
            });

        }

       public DesignDataService()
        {
            RarFormF6Data = new RarFormF6();
            SetFakeData();
        }
        
        public void GetData(Action<RarFormF6, Exception> callback)
        {
            callback(RarFormF6Data, null);
        }
   
    }
}