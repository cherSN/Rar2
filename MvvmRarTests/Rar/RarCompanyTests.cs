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
    public class RarCompanyTests
    {
        [TestMethod()]
        public void RarCompanyTestDefault()
        {
            // arrange
            RarCompany company = new RarCompany();
            // act
            //Assert
            Assert.IsTrue(company.ID != null);
            Assert.IsTrue(company.INN != null);
            Assert.IsTrue(company.KPP != null);
            Assert.AreEqual(false, company.IsUsed, "Is used");
            Assert.IsNotNull(company.Adress);
            Assert.IsNotNull(company.LicenseList);

        }

        [TestMethod()]
        public void RarCompanyTestWithArgs()
        {
            // arrange
            RarAdress adress = new RarAdress();
            RarCompany company = new RarCompany("ID", "NAME", "INN", "KPP", adress);
            // act

            //Assert
            Assert.AreEqual("ID", company.ID, "ID");
            Assert.AreEqual("NAME", company.Name, "Name");
            Assert.AreEqual("INN", company.INN, "INN");
            Assert.AreEqual("KPP", company.KPP, "KPP");
            Assert.AreEqual(false, company.IsUsed, "Is used");
            Assert.IsNotNull(company.Adress,"Adress");
            Assert.IsNotNull(company.LicenseList, "LicensesList");
        }

        [TestMethod()]
        public void ToStringTest()
        {
            // arrange
            RarAdress adress = new RarAdress();
            RarCompany company = new RarCompany("ID", "Company", "7701010101", "770101001", adress);
            // act
            string result = company.ToString();

            //Assert
            Assert.AreEqual("Company ИНН:7701010101; КПП:770101001", result);

        }
    }
}