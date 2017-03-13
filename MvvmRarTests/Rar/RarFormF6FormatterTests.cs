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
        public void RarFormF6FormatterTest_SetupAdress()
        {
            // arrange
            string str =
            @"<АдрОрг>
                    <КодСтраны> 643 </КодСтраны>
                    <Индекс> 124460 </Индекс>
                    <КодРегион> 77 </КодРегион>
                    <Район> Московский </Район>
                    <Город> Зеленоград г </Город>
                    <НаселПункт> Пункт </НаселПункт>
                    <Улица> Западный 2-й проезд </Улица>
                    <Дом> 8 </Дом>
                    <Корпус> 12 </Корпус>
                    <Литера> Б </Литера>
                    <Кварт> 2 </Кварт>
              </АдрОрг>";

            XElement el = XDocument.Parse(str).Root;

            RarFormF6Formatter f6formatter = new RarFormF6Formatter();
            var privateObject = new PrivateObject(f6formatter);

            RarFormF6 formF6 = new RarFormF6();
            //act
            RarAdress adress = (RarAdress)privateObject.Invoke("SetupAdress", el);

            string resultCountryId = adress.CountryId;
            string resultPostCode = adress.PostCode;
            string resultRegionId = adress.RegionId;
            string resultDistrict = adress.District;
            string resultCity = adress.City;
            string resultLocality = adress.Locality;
            string resultStreet = adress.Street;
            string resultBuilding = adress.Building;
            string resultBlock = adress.Block;
            string resultLitera = adress.Litera;
            string resultApartment = adress.Apartment;

            ////assert
            Assert.AreEqual("643", resultCountryId, "CountryId");
            Assert.AreEqual("124460", resultPostCode, "PostCode");
            Assert.AreEqual("77", resultRegionId, "RegionId");
            Assert.AreEqual("Московский", resultDistrict, "District");
            Assert.AreEqual("Зеленоград г", resultCity, "City");
            Assert.AreEqual("Пункт", resultLocality, "Locality");
            Assert.AreEqual("Западный 2-й проезд", resultStreet, "Street");
            Assert.AreEqual("12", resultBlock, "Block");
            Assert.AreEqual("Б", resultLitera, "Litera");
            Assert.AreEqual("8", resultBuilding, "Building");
            Assert.AreEqual("2", resultApartment, "Apartment");
        }
        [TestMethod()]
        public void RarFormF6FormatterTest_SetupLisences()
        {
            // arrange
            string str =
            @"<Лицензии>
				<Лицензия ИдЛицензии=""11"" П000000000011=""18РПА0001901"" П000000000012=""31.08.2016"" П000000000013=""31.08.2018"" П000000000014=""Министерство промышленности и торговли""/>
				<Лицензия ИдЛицензии=""22"" П000000000011=""18РПА0001902"" П000000000012=""31.08.2016"" П000000000013=""31.08.2018"" П000000000014=""Министерство промышленности и торговли""/>
				<Лицензия ИдЛицензии=""33"" П000000000011=""18РПА0001903"" П000000000012=""31.08.2016"" П000000000013=""31.08.2018"" П000000000014=""Министерство промышленности и торговли""/>
            </Лицензии>";

            XElement el = XDocument.Parse(str).Root;

            RarFormF6Formatter f6formatter = new RarFormF6Formatter();
            var privateObject = new PrivateObject(f6formatter);

            RarCompany company = new RarCompany();
            //act
            privateObject.Invoke("SetupLisences", el, company);
            List<RarLicense> lisenceList = company.LicenseList;

            int resultNumberOfLisences      =   lisenceList.Count;
            string      resultID            =   lisenceList[0].ID;
            string      resultSeriesNumber  =   lisenceList[0].SeriesNumber;
            DateTime    resultDateFrom      =   lisenceList[0].DateFrom;
            DateTime    resultDateTo        =   lisenceList[0].DateTo;
            string      resultIssuer        =   lisenceList[0].Issuer;

            ////assert
            Assert.AreEqual(3, resultNumberOfLisences, "NumberOfLisences");
            Assert.AreEqual("11", resultID, "ID");
            Assert.AreEqual("18РПА0001901", resultSeriesNumber, "SeriesNumber");
            Assert.AreEqual(DateTime.Parse("31.08.2016"), resultDateFrom, "DateFrom");
            Assert.AreEqual(DateTime.Parse("31.08.2018"), resultDateTo, "DateTo");
            Assert.AreEqual("Министерство промышленности и торговли", resultIssuer, "Issuer");

        }
        [TestMethod()]
        public void RarFormF6FormatterTest_SetupOrganization()
        {
            // arrange
            string str =
      @"<Организация>
            <Реквизиты Наим = ""Общество с ограниченной ответственностью"" ТелОрг = ""+7 (926) 150-01-01"" EmailОтпр = ""mail@mail.ru"">
                <АдрОрг>
                    <КодСтраны> 643 </КодСтраны>
                    <Индекс> 124460 </Индекс>
                    <КодРегион> 77 </КодРегион>
                    <Район/>
                    <Город> Зеленоград г </Город>
                    <НаселПункт/>
                    <Улица> Западный 2 - й проезд </Улица>
                    <Дом> 1 </Дом>
                    <Корпус> 2 </Корпус>
                    <Литера/>
                    <Кварт> 2 </Кварт>
                </АдрОрг>
                <ЮЛ ИННЮЛ = ""7735146496"" КППЮЛ = ""773501001"" />
            </Реквизиты>
            <ОтветЛицо>
                <Руководитель>
                    <Фамилия> Иванов </Фамилия>
                    <Имя> Иван </Имя>
                    <Отчество> Иванович </Отчество>
                </Руководитель>
                <Главбух>
                    <Фамилия> Петров </Фамилия>
                    <Имя> Петр </Имя>
                    <Отчество> Петрович </Отчество>
                </Главбух>
            </ОтветЛицо>
            <Деятельность> 
                <Лицензируемая>
                    <Лицензия ВидДеят = ""03"" СерНомЛиц = ""РА, 003355"" ДатаНачЛиц = ""11.04.2016"" ДатаОконЛиц = ""10.04.2021""/>
                </Лицензируемая>
            </Деятельность>
         </Организация>";

            XElement el = XDocument.Parse(str).Root;

            RarFormF6Formatter f6formatter = new RarFormF6Formatter();
            var privateObject = new PrivateObject(f6formatter);
            RarOurCompany company = new RarOurCompany();

            //act
            privateObject.Invoke("SetupOrganization", el, company);

            string resultName = company.Name;
            string resultPhone = company.Phone;
            string resultEmail = company.Email;
            string resultINN = company.INN;
            string resultKPP = company.KPP;

            string resulDirectorSurname = company.Director.Surname;
            string resulDirectorName = company.Director.Name;
            string resulDirectorMiddlename = company.Director.Middlename;

            string resulAccountantSurname = company.Accountant.Surname;
            string resulAccountantName = company.Accountant.Name;
            string resulAccountantMiddlename = company.Accountant.Middlename;

            //assert
            Assert.AreEqual("Общество с ограниченной ответственностью", resultName, "Name");
            Assert.AreEqual("+7 (926) 150-01-01", resultPhone, "Phone");
            Assert.AreEqual("mail@mail.ru", resultEmail, "Email");
            Assert.AreEqual("7735146496", resultINN, "INN");
            Assert.AreEqual("773501001", resultKPP, "KPP");

            Assert.AreEqual("Иванов", resulDirectorSurname, "DirectorSurname");
            Assert.AreEqual("Иван", resulDirectorName, "DirectorName");
            Assert.AreEqual("Иванович", resulDirectorMiddlename, "DirectorMiddlename");

            Assert.AreEqual("Петров", resulAccountantSurname, "AccountantSurname");
            Assert.AreEqual("Петр", resulAccountantName, "AccountantName");
            Assert.AreEqual("Петрович", resulAccountantMiddlename, "AccountantMiddlename");
        }

        //[TestMethod()]
        //public void SerializeTest()
        //{
        //    Assert.Fail();
        //}


        //[TestMethod()]
        //public void SaveTurnoverDataTest()
        //{
        //    Assert.Fail();
        //}
    }
}