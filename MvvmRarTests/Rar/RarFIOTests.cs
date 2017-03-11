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
        public void RarFIOTests_ToStringTest()
        {
            // arrange
            RarFIO rarFIO = new RarFIO("Surname", "Name", "Middlename");
            // act
            string result = rarFIO.ToString();
            //Assert
            Assert.AreEqual("Surname Name Middlename", result);
        }

        [TestMethod()]
        public void RarFIOTest_ConstructorWithNullArguments()
        {
            // arrange
            RarFIO rarFIO = new RarFIO(null, null, null);
            // act

            //Assert

            Assert.IsNull(rarFIO.Name);
            Assert.IsNull(rarFIO.Middlename);
            Assert.IsNull(rarFIO.Name);
        }

        [TestMethod()]
        public void RarFIOTests_Constructor()
        {
            // arrange
            RarFIO rarFIO = new RarFIO("Surname", "Name", "Middlename");
            // act

            //Assert

            Assert.AreEqual("Surname", rarFIO.Surname,"Неверно инициализировано Surname");
            Assert.AreEqual("Name", rarFIO.Name, "Неверно инициализировано Name");
            Assert.AreEqual("Middlename", rarFIO.Middlename, "Неверно инициализировано Middlename");
        }
        [TestMethod()]
        public void RarFIOTest_CopyConstructor()
        {
            // arrange
            RarFIO rarFioOrigin = new RarFIO("Surname", "Name", "Middlename");
            // act
            RarFIO rarFio = new RarFIO(rarFioOrigin);
            //Assert

            Assert.AreEqual("Surname", rarFio.Surname, "Неверно инициализировано Surname");
            Assert.AreEqual("Name", rarFio.Name, "Неверно инициализировано Name");
            Assert.AreEqual("Middlename", rarFio.Middlename, "Неверно инициализировано Middlename");
        }
    }
}