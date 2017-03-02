using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvvmRar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmRar.Rar;
using Moq;

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
            var result = ds.RarFormF6Data;
            //Assert

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void GetDataTest()
        {
            // arrange
            RarFormF6 result1 = new RarFormF6();
            Exception result2=new Exception();
            DataService ds = new DataService();
            //act
            ds.GetData(
                  (data, error) =>
                  {
                      result1 = data;
                      result2 = error;
                  });

            //Assert
            Assert.AreSame(ds.RarFormF6Data, result1, "RarFormF6");
            Assert.IsNull(result2,"Exception");
        }

        //[TestMethod()]
        //public void GetDataTest1()
        //{




        //    Mock<IDataService> mockDataService = new Mock<IDataService>();
        //    mockDataService.Setup(m => m.GetData(It.IsAny<Action<RarFormF6, Exception>>()));
        //    //mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>())).Returns<decimal>(total => total);

        //    RarFormF6 result1 = new RarFormF6();
        //    Exception result2 = new Exception();
        //    //act
        //    mockDataService.Object.GetData(
        //          (data, error) =>
        //          {
        //              result1 = data;
        //              result2 = error;
        //          });
            
        //    //Assert
        //    //Assert.AreSame(mockDataService.RarFormF6Data, result1, "RarFormF6");
        //    //Assert.IsNull(result2, "Exception");
        //}
    }
}