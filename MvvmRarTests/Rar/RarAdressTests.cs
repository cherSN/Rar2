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
    public class RarAdressTests
    {
        [TestMethod()]
        public void RarAdressTest_ConstructorStrictAdress()
        {
            // arrange
            RarAdress adress = new RarAdress("Strict adress");
            // act
            //Assert
            Assert.IsFalse(adress.StrictAdress);
            Assert.AreEqual("Strict adress", adress.AdressString, "Strict adress");
        }

        [TestMethod()]
        public void RarAdressTest_ConstructorNonStrictAdress()
        {
            // arrange
            RarAdress adress = new RarAdress("Country", "PostCode", "RegionId", "District", 
                "City", "Locality", "Street", "Building", "Block", "Litera", "Apartment");
            adress.AdressString = "AdressString";
            // act

            //Assert
            Assert.AreEqual("Country", adress.CountryId, "Country");
            Assert.AreEqual("PostCode", adress.PostCode, "PostCode");
            Assert.AreEqual("RegionId", adress.RegionId, "RegionId");
            Assert.AreEqual("District", adress.District, "District");
            Assert.AreEqual("City", adress.City, "City");
            Assert.AreEqual("Locality", adress.Locality, "Locality");
            Assert.AreEqual("Street", adress.Street, "Street");
            Assert.AreEqual("Building", adress.Building, "Building");
            Assert.AreEqual("Block", adress.Block, "Block");
            Assert.AreEqual("Litera", adress.Litera, "Litera");
            Assert.AreEqual("Apartment", adress.Apartment, "Apartment");
            Assert.IsTrue(adress.StrictAdress, "StrictAdress");
            Assert.AreEqual("AdressString", adress.AdressString, "AdressString");
        }

        [TestMethod()]
        public void RarAdressTest_CopyConstructor()
        {
            // arrange
            RarAdress adressOrigin = new RarAdress("Country", "PostCode", "RegionId", "District",
                "City", "Locality", "Street", "Building", "Block", "Litera", "Apartment");
            // act
            RarAdress adress = new RarAdress(adressOrigin);
            //Assert
            Assert.AreEqual("Country", adress.CountryId, "Country");
            Assert.AreEqual("PostCode", adress.PostCode, "PostCode");
            Assert.AreEqual("RegionId", adress.RegionId, "RegionId");
            Assert.AreEqual("District", adress.District, "District");
            Assert.AreEqual("City", adress.City, "City");
            Assert.AreEqual("Locality", adress.Locality, "Locality");
            Assert.AreEqual("Street", adress.Street, "Street");
            Assert.AreEqual("Building", adress.Building, "Building");
            Assert.AreEqual("Block", adress.Block, "Block");
            Assert.AreEqual("Litera", adress.Litera, "Litera");
            Assert.AreEqual("Apartment", adress.Apartment, "Apartment");
            Assert.IsTrue(adress.StrictAdress);

        }

        [TestMethod()]
        public void RarAdressTest_ConstructorWithNullProperties()
        {
            // arrange
            RarAdress adress = new RarAdress(null,null,null,null,null,null,null,null,null,null,null);
            // act

            //Assert
            Assert.IsTrue(adress.CountryId!= null);
            Assert.IsTrue(adress.PostCode != null);
            Assert.IsTrue(adress.RegionId != null);
            Assert.IsTrue(adress.District != null);
            Assert.IsTrue(adress.City != null);
            Assert.IsTrue(adress.Locality != null);
            Assert.IsTrue(adress.Street != null);
            Assert.IsTrue(adress.Building != null);
            Assert.IsTrue(adress.Block != null);
            Assert.IsTrue(adress.Litera != null);
            Assert.IsTrue(adress.Apartment != null);
        }

        [TestMethod()]
        public void RarAdressTest_ToStringNonStrictAdress()
        {
            // arrange
            RarAdress adress = new RarAdress("Country", "PostCode", "RegionId", "District",
                 "City", "Locality", "Street", "Building", "Block", "Litera", "Apartment");
            // act
            string result = adress.ToString();
            //Assert

            Assert.AreEqual("Country, PostCode, RegionId, District, City, Locality, Street, Building, Block, Litera, Apartment", result);

        }


        [TestMethod()]
        public void RarAdressTest_ToStringStrictAdress()
        {
            // arrange
            RarAdress adress = new RarAdress("Strict adress");
            // act
            string result = adress.ToString();
            //Assert
            Assert.AreEqual("Strict adress", result);

        }
    }
}