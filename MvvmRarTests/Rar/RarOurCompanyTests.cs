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
    public class RarOurCompanyTests
    {
        [TestMethod()]
        public void RarOurCompanyTest()
        {
            // arrange
            RarOurCompany company = new RarOurCompany();
            // act
            //Assert

            Assert.IsNotNull(company.Director);
            Assert.IsNotNull(company.Accountant);
            Assert.IsNotNull(company.SubdevisionList);

        }
    }
}