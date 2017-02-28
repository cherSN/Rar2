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
    public class RarFIOTests
    {

        [TestMethod()]
        public void ToStringTest()
        {
            // arrange
            RarFIO rarFIO = new RarFIO("Surname", "Name", "Midlename");
            // act
            string result = rarFIO.ToString();
            //Assert
            Assert.AreEqual("Surname Name Midlename", result);
        }
    }
}