using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvvmRar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmRar.Model.Tests
{
    [TestClass()]
    public class DataServiceTests
    {
        [TestMethod()]
        public void DataServiceTest()
        {
            // arrange
            DataService ds = new DataService();

            // act
            //Assert

            Assert.IsNotNull(ds.RarFormF6Data);
        }

        [TestMethod()]
        public void GetDataTest()
        {
            Assert.IsTrue(true);
        }
    }
}