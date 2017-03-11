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
    public class RarLicenseTests
    {
        [TestMethod()]
        public void RarLicenseTest_DefaultConstructor()
        {
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void RarLicenseTest_CopyConstructor()
        {
            // arrange
            DateTime date1 = DateTime.Parse("01.01.2015");
            DateTime date2 = DateTime.Parse("01.01.2016");
            RarLicense licenseOrigin = new RarLicense()
            {
                ID = "ID",
                SeriesNumber = "SeriesNumber",
                Issuer = "Issuer",
                BusinesType = "BusinesType",
                DateFrom = date1,
                DateTo = date2
            };
            // act
            RarLicense license = new RarLicense(licenseOrigin);
            //Assert

            Assert.AreEqual("ID", license.ID, "ID");
            Assert.AreEqual("SeriesNumber", license.SeriesNumber, "SeriesNumber");
            Assert.AreEqual("Issuer", license.Issuer, "Issuer");
            Assert.AreEqual("BusinesType", license.BusinesType, "BusinesType");
            Assert.AreEqual(date1, license.DateFrom, "DateFrom");
            Assert.AreEqual(date2, license.DateTo, "DateTo");
        }

        [TestMethod()]
        public void RarLicenseTest_ToString()
        {
            // arrange
            DateTime date1 = DateTime.Parse("01.01.2015");
            DateTime date2 = DateTime.Parse("01.01.2016");
            RarLicense license = new RarLicense()
            {
                ID = "ID",
                SeriesNumber = "SeriesNumber",
                Issuer = "Issuer",
                BusinesType = "BusinesType",
                DateFrom = date1,
                DateTo = date2
            };
            // act
            string result = license.ToString();
            //Assert

            Assert.AreEqual("SeriesNumber; 01.01.2015-01.01.2016", result);
        }
  
    }
}