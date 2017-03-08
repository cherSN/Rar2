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
            Stream st = assembly.GetManifestResourceStream("MvvmRarTest.Rar.Resources.D6_Test.xml");


            Type type = Type.GetType("MvvmRar.Rar.Tests.RarFormF6FormatterTests", false);
            using (Stream str = type.Assembly.GetManifestResourceStream("MvvmRar.Rar.Tests.Resources.D6_Test.xml"))
            {
                using (XmlReader reader = new XmlTextReader(str))
                {
                    //string xsdForm6 = Rar.Model.Properties.Resources.xsd_F6_010117;
                    xdoc = XDocument.Load(reader);
                }
            }

        }

        [TestMethod()]
        public void RarFormF6FormatterTest()
        {
            RarFormF6Formatter f6formatter = new RarFormF6Formatter();
            var privateObject = new PrivateObject(f6formatter);
            //privateObject.SetField("name",_name);
            //privateObject.SetProperty("Name"? name);

            XElement el = new XElement("root", 
                new XAttribute("НаимПрог", "1СProgram"),
                new XAttribute("ВерсФорм", "3.1"),
                new XAttribute("ДатаДок", "01.01.2015"),
                    new XElement("ФормаОтч",
                        new XAttribute("НомФорм", "6.1"),
                        new XAttribute("ПризПериодОтч", "0"),
                        new XAttribute("ГодПериодОтч", "2015")
                    
                    )

                
                );
            RarFormF6 formF6 = new RarFormF6();
            privateObject.Invoke("SetupHeader", el, formF6);

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