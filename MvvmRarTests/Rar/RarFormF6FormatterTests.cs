using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvvmRar.Rar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace MvvmRar.Rar.Tests
{
    [TestClass()]
    public class RarFormF6FormatterTests
    {
        private XDocument xdoc;

        public RarFormF6FormatterTests()
        {
            Assembly assembly= Assembly.GetExecutingAssembly();
            using (Stream str = assembly.GetManifestResourceStream("MvvmRarTests.Resources.D6_Test2.xml")) //Должен быть внедренным
            {
                xdoc = XDocument.Load(str);
            }

        }


        [TestMethod()]
        public void RarFormF6FormatterTestSetupRootAttribute()
        {
            // arrange
            string str = 
            @"<Файл ДатаДок=""23.01.2017"" ВерсФорм=""4.31"" НаимПрог=""1С: ПРЕДПРИЯТИЕ 8.3 УТ 11.2.3.203""></Файл >";
            XElement el = XDocument.Parse(str).Root;

            RarFormF6Formatter f6formatter = new RarFormF6Formatter();
            var privateObject = new PrivateObject(f6formatter);

            RarFormF6 formF6 = new RarFormF6();
            //act
            privateObject.Invoke("SetupRootAttribute", el, formF6);

            DateTime resultDateDoc = formF6.DocumentDate;  //DateTime.Parse("23.01.2017");
            string resultVersion = formF6.Version;
            string resultProgramName = formF6.ProgramName;
 
            //assert
            Assert.AreEqual(DateTime.Parse("23.01.2017"), resultDateDoc, "resultDateDoc");
            Assert.AreEqual("4.31", resultVersion, "resultVersion");
            Assert.AreEqual("1С: ПРЕДПРИЯТИЕ 8.3 УТ 11.2.3.203", resultProgramName, "ProgrameName");

        }
        [TestMethod()]
        public void RarFormF6FormatterTestSetupHeader()
        {


            Assert.Fail();
        }

        [TestMethod()]
        public void SerializeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeserializeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SaveCompaniesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SaveTurnoverDataTest()
        {
            Assert.Fail();
        }
    }
}