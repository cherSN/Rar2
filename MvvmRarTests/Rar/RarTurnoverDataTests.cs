using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvvmRar.Rar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmRar.Rar.Tests
{
    [TestClass()]
    public class RarTurnoverDataTests
    {
        [TestMethod()]
        public void RarTurnoverDataTestDefaultConstructor()
        {
            // arrange
            RarTurnoverData data = new RarTurnoverData();
            // act
            //Assert

            Assert.IsNotNull(data.Subdevision);
            Assert.IsNotNull(data.Manufacturer);
            Assert.IsNotNull(data.Buyer);
            Assert.IsNotNull(data.License);

        }

        [TestMethod()]
        public void RarTurnoverDataTestCopyConstructor()
        {
            // arrange
            DateTime date1 = DateTime.Parse("01.01.2016");
            DateTime date2 = DateTime.Parse("02.01.2016");

            RarSubdevision subdevision = new RarSubdevision();
            RarCompany manufacturer = new RarCompany();
            RarCompany buyer = new RarCompany();
            RarLicense license = new RarLicense();


            RarTurnoverData dataOrigin = new RarTurnoverData()
            {
                AlcoCode = "200",
                NotificationDate = date1,
                NotificationNumber = "N001",
                NotificationTurnover = 2.1,
                DocumentDate = date2,
                DocumentNumber = "D01",
                CustomsDeclarationNumber = "D001",
                Turnover = 1.0,
                Subdevision = subdevision,
                Manufacturer = manufacturer,
                Buyer = buyer,
                License=license
            };
            // act
            RarTurnoverData data = new RarTurnoverData(dataOrigin);
            //Assert
            Assert.AreEqual("200", data.AlcoCode, "AlcoCode");
            Assert.AreEqual(date1, data.NotificationDate, "NotificationDate");
            Assert.AreEqual("N001", data.NotificationNumber, "NotificationNumber");
            Assert.AreEqual(2.1, data.NotificationTurnover, "NotificationTurnover");
            Assert.AreEqual(date2, data.DocumentDate, "DocumentDate");
            Assert.AreEqual("D01", data.DocumentNumber, "DocumentNumber");
            Assert.AreEqual("D001", data.CustomsDeclarationNumber, "CustomsDeclarationNumber");
            Assert.AreEqual(1.0, data.Turnover, "Turnover");
            Assert.AreSame(subdevision, data.Subdevision, "Subdevision");
            Assert.AreSame(manufacturer, data.Manufacturer, "Manufacturer");
            Assert.AreSame(buyer, data.Buyer, "Buyer");
            Assert.AreSame(license, data.License, "License");
        }
    }
}