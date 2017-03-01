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
    public class RarSubdevisionTests
    {
        [TestMethod()]
        public void RarSubdevisionTestDefault()
        {
            // arrange
            RarSubdevision subdevision = new RarSubdevision();
            // act
            //Assert

            Assert.IsNotNull(subdevision.Adress);
        }

        [TestMethod()]
        public void RarSubdevisionTestWithArgs()
        {
            // arrange
            RarAdress adress = new RarAdress();
            RarSubdevision subdevision = new RarSubdevision("Name", "KPP", adress);
            // act

            //Assert
            Assert.AreEqual("Name", subdevision.Name, "Name");
            Assert.AreEqual("KPP", subdevision.KPP, "KPP");
            Assert.AreEqual(adress, subdevision.Adress, "Adress");
        }

        [TestMethod()]
        public void RarSubdevisionTestCopyConstructor()
        {
            // arrange
            RarAdress adress = new RarAdress();
            RarSubdevision subdevisionOrigin = new RarSubdevision("Name", "KPP", adress);
            // act
            RarSubdevision subdevision = new RarSubdevision(subdevisionOrigin);
            //Assert
            Assert.AreEqual("Name", subdevision.Name, "Name");
            Assert.AreEqual("KPP", subdevision.KPP, "KPP");
            Assert.AreEqual(adress, subdevision.Adress, "Adress");
        }

        [TestMethod()]
        public void ToStringTest()
        {
            // arrange
            RarAdress adress = new RarAdress();
            RarSubdevision subdevision = new RarSubdevision("Subdevision", "770101001", adress);
            // act
            string result = subdevision.ToString();
            //Assert
            Assert.AreEqual("Subdevision КПП:770101001", result);

        }
    }
}