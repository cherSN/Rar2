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
        public void RarFormF6FormatterTest_SetupRootAttribute()
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
            Assert.AreEqual(DateTime.Parse("23.01.2017"), resultDateDoc, "DateDoc");
            Assert.AreEqual("4.31", resultVersion, "Version");
            Assert.AreEqual("1С: ПРЕДПРИЯТИЕ 8.3 УТ 11.2.3.203", resultProgramName, "ProgrameName");

        }
        [TestMethod()]
        public void RarFormF6FormatterTest_SetupReportParametersPrimary()
        {
            // arrange
            string str =
            @"<ФормаОтч НомФорм=""06"" ПризПериодОтч=""0"" ГодПериодОтч=""2016"">
                  <Первичная/>
              </ФормаОтч>";
            XElement el = XDocument.Parse(str).Root;

            RarFormF6Formatter f6formatter = new RarFormF6Formatter();
            var privateObject = new PrivateObject(f6formatter);

            RarFormF6 formF6 = new RarFormF6();
            //act
            privateObject.Invoke("SetupReportParameters", el, formF6);

            string resultFormNumber = formF6.FormNumber;
            string resultReportPeriod = formF6.ReportPeriod;
            string resultReportYear = formF6.ReportYear;
            string resultCorrectionNumber = formF6.CorrectionNumber;

            //assert
            Assert.AreEqual("06", resultFormNumber, "FormNumber");
            Assert.AreEqual("0", resultReportPeriod, "ReportPeriod");
            Assert.AreEqual("2016", resultReportYear, "ReportYear");
            Assert.AreEqual("0", resultCorrectionNumber, "CorrectionNumber");
        }
        [TestMethod()]
        public void RarFormF6FormatterTest_SetupReportParametersAdjustment()
        {
            // arrange
            string str =
            @"<ФормаОтч НомФорм=""06"" ПризПериодОтч=""0"" ГодПериодОтч=""2016"">
                  <Корректирующая НомерКорр=""21""/>
              </ФормаОтч>";
            XElement el = XDocument.Parse(str).Root;

            RarFormF6Formatter f6formatter = new RarFormF6Formatter();
            var privateObject = new PrivateObject(f6formatter);

            RarFormF6 formF6 = new RarFormF6();
            //act
            privateObject.Invoke("SetupReportParameters", el, formF6);

            string resultFormNumber = formF6.FormNumber;
            string resultReportPeriod = formF6.ReportPeriod;
            string resultReportYear = formF6.ReportYear;
            string resultCorrectionNumber = formF6.CorrectionNumber;

            //assert
            Assert.AreEqual("06", resultFormNumber, "FormNumber");
            Assert.AreEqual("0", resultReportPeriod, "ReportPeriod");
            Assert.AreEqual("2016", resultReportYear, "ReportYear");
            Assert.AreEqual("21", resultCorrectionNumber, "CorrectionNumber");

        }

        [TestMethod()]
        public void SerializeTest()
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