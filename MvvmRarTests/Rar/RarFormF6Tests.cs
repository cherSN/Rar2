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
    public class RarFormF6Tests
    {
        [TestMethod()]
        public void RarFormF6Test()
        {
            // arrange
            RarOurCompany company = new RarOurCompany();
            RarFormF6 formF6 = new RarFormF6();
            // act
            //Assert

            Assert.IsNotNull(formF6.OurCompany);
            Assert.IsNotNull(formF6.BuyerList);
            Assert.IsNotNull(formF6.ManufacturerList);
            Assert.IsNotNull(formF6.TurnoverDataList);
        }
    }
}