using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace MvvmRar.Rar
{
   public class RarFormF6Formatter //: IFormatter
    {
        #region -- Private Members --
        SerializationBinder binder;
        StreamingContext context;
        ISurrogateSelector surrogateSelector;
        #endregion

        #region -- Public Properties --
        public ISurrogateSelector SurrogateSelector
        {
            get { return surrogateSelector; }
            set { surrogateSelector = value; }
        }
        public SerializationBinder Binder
        {
            get { return binder; }
            set { binder = value; }
        }
        public StreamingContext Context
        {
            get { return context; }
            set { context = value; }
        }
        #endregion

        #region -- Public Constructor --
        public RarFormF6Formatter()
        {
            //context = new StreamingContext(StreamingContextStates.All);
        }
        #endregion
        public void Serialize(System.IO.Stream serializationStream, object f6)
        { }

        #region -- Deserialization --
        private string GetElementStringValue(XElement elm)
        {
          return  (elm==null)? "" : ((string)elm).Trim();
        }
        private string GetAttributeStringValue(XAttribute atr)
        {
            return (atr == null) ? "" : ((string)atr).Trim();
        }

        public object Deserialize(System.IO.Stream serializationStream)
        {
            RarFormF6 formF6 = new RarFormF6();

            XDocument xdoc = XDocument.Load(serializationStream);
            if (!IsDocumentValid(xdoc)) throw new Exception("Не соответствует схеме");

            SetupRootAttribute(xdoc.Root, formF6);
            SetupReportParameters(xdoc.Root.Element("ФормаОтч"), formF6);
            SetupBuyers(xdoc.Root.Element("Справочники"), formF6);
            SetupManufacturers(xdoc.Root.Element("Справочники"), formF6);
            SetupOrganization(xdoc.Root.Element("Документ").Element("Организация"), formF6.OurCompany);

            SetupTurnoverData(xdoc.Root.Element("Документ").Element("ОбъемОборота"), formF6);
            return formF6;
        }
        private void SetupRootAttribute(XElement root, RarFormF6 formF6)
        {
            //Атрибуты
            formF6.ProgramName =    GetAttributeStringValue(root.Attribute("НаимПрог"));
            formF6.Version =        GetAttributeStringValue(root.Attribute("ВерсФорм"));
            formF6.DocumentDate =   DateTime.Parse(root.Attribute("ДатаДок").Value);
        }
        private void SetupReportParameters(XElement elm, RarFormF6 formF6)
        {
            //ФормаОтч
            formF6.FormNumber =     GetAttributeStringValue(elm.Attribute("НомФорм"));
            formF6.ReportPeriod =   GetAttributeStringValue(elm.Attribute("ПризПериодОтч"));
            formF6.ReportYear =     GetAttributeStringValue(elm.Attribute("ГодПериодОтч"));

            XElement corrections = elm.Element("Корректирующая");
            if (corrections == null)    formF6.CorrectionNumber = "0";
            else formF6.CorrectionNumber = GetAttributeStringValue(corrections.Attribute("НомерКорр"));
        }
        private RarAdress SetupAdress(XElement adress)
        {
            RarAdress adr = new RarAdress();
            adr.StrictAdress = true;
            adr.CountryId =     GetElementStringValue(adress.Element("КодСтраны"));
            adr.PostCode =      GetElementStringValue(adress.Element("Индекс"));
            adr.RegionId =      GetElementStringValue(adress.Element("КодРегион"));
            adr.District =      GetElementStringValue(adress.Element("Район"));
            adr.City =          GetElementStringValue(adress.Element("Город"));
            adr.Locality =      GetElementStringValue(adress.Element("НаселПункт"));
            adr.Street =        GetElementStringValue(adress.Element("Улица"));
            adr.Building =      GetElementStringValue(adress.Element("Дом"));
            adr.Block =         GetElementStringValue(adress.Element("Корпус"));
            adr.Litera =        GetElementStringValue(adress.Element("Литера"));
            adr.Apartment =     GetElementStringValue(adress.Element("Кварт"));
            adr.AdressString =
                adr.CountryId + "," +
                adr.PostCode + "," +
                adr.RegionId + "," +
                adr.District + "," +
                adr.City + "," +
                adr.Locality + "," +
                adr.Street + "," +
                adr.Building + "," +
                adr.Block + "," +
                adr.Litera + "," +
                adr.Apartment + ",";
            return adr;
        }
        private void SetupLisences(XElement lisenses, RarCompany rc)
        {
            foreach (XNode node in lisenses.Elements("Лицензия"))
            {
                RarLicense license = new RarLicense();
                XElement el = (XElement)node;
                license.ID =            GetAttributeStringValue(el.Attribute("ИдЛицензии"));
                license.SeriesNumber =  GetAttributeStringValue(el.Attribute("П000000000011"));
                license.DateFrom =      DateTime.Parse(el.Attribute("П000000000012").Value);
                license.DateTo =        DateTime.Parse(el.Attribute("П000000000013").Value);
                license.Issuer =        GetAttributeStringValue(el.Attribute("П000000000014"));
                rc.LicenseList.Add(license);
            }
        }
        private void SetupBuyers(XElement references, RarFormF6 formF6)
        {
            foreach (XNode node in references.Elements("Контрагенты"))
            {
                XElement el = (XElement)node;
                RarCompany rc = new RarCompany();
                rc.Name =   GetAttributeStringValue(el.Attribute("П000000000007"));
                rc.ID =     GetAttributeStringValue(el.Attribute("ИдКонтр"));

                XElement resident = el.Element("Резидент");
                if (resident != null)
                {
                    SetupLisences(resident.Element("Лицензии"), rc);
                    XElement adress = resident.Element("П000000000008");
                    rc.Adress = SetupAdress(adress);

                    XElement company = resident.Element("ЮЛ");
                    if (company != null)
                    {
                        rc.INN = GetAttributeStringValue(company.Attribute("П000000000009"));
                        rc.KPP = GetAttributeStringValue(company.Attribute("П000000000010"));
                    }
                    else
                    {
                        XElement individual = resident.Element("ФЛ");
                        if (individual != null)
                            rc.INN = GetAttributeStringValue(individual.Attribute("П000000000009"));
                    }
                }
                else
                {
                    XElement foreigner = el.Element("Иностр");
                    if (foreigner != null)
                    {
                        rc.INN = GetAttributeStringValue(foreigner.Attribute("Номер")); //Учетный номер

                        rc.Adress = new RarAdress();
                        rc.Adress.StrictAdress = false;
                        rc.Adress.AdressString =    GetAttributeStringValue(foreigner.Attribute("П000000000082"));
                        rc.Adress.CountryId =       GetAttributeStringValue(foreigner.Attribute("П000000000081"));
                    }
                }

                formF6.BuyerList.Add(rc);
            }

        }
        private void SetupManufacturers(XElement references, RarFormF6 formF6)
        {
            foreach (XNode node in references.Elements("ПроизводителиИмпортеры"))
            {
                XElement el = (XElement)node;
                RarCompany rc = new RarCompany();
                rc.ID =     GetAttributeStringValue(el.Attribute("ИдПроизвИмп"));
                rc.Name =   GetAttributeStringValue(el.Attribute("П000000000004"));
                rc.INN =    GetAttributeStringValue(el.Attribute("П000000000005"));
                rc.KPP =    GetAttributeStringValue(el.Attribute("П000000000006")); //необязат
                formF6.ManufacturerList.Add(rc);
            }
        }
        private void SetupBigBoss(XElement bigBoss, RarFIO fio) {
            fio.Surname =       GetElementStringValue(bigBoss.Element("Фамилия"));
            fio.Name =          GetElementStringValue(bigBoss.Element("Имя"));
            fio.Middlename =    GetElementStringValue(bigBoss.Element("Отчество")); //необязательный
        }
        private void SetupOrganization(XElement organization, RarOurCompany OurCompany)
        {
            SetupBigBoss(organization.Element("ОтветЛицо").Element("Руководитель"), OurCompany.Director);
            SetupBigBoss(organization.Element("ОтветЛицо").Element("Главбух"), OurCompany.Accountant);

            OurCompany.Name =   GetAttributeStringValue(organization.Element("Реквизиты").Attribute("Наим"));
            OurCompany.Phone =  GetAttributeStringValue(organization.Element("Реквизиты").Attribute("ТелОрг"));
            OurCompany.Email =  GetAttributeStringValue(organization.Element("Реквизиты").Attribute("EmailОтпр"));

            OurCompany.Adress = SetupAdress(organization.Element("Реквизиты").Element("АдрОрг"));
            XElement company = organization.Element("Реквизиты").Element("ЮЛ");
            if (company != null)
            {
                OurCompany.INN =    GetAttributeStringValue(company.Attribute("ИННЮЛ"));
                OurCompany.KPP =    GetAttributeStringValue(company.Attribute("КППЮЛ"));
            }
            else
            {
                XElement individual = organization.Element("Реквизиты").Element("ФЛ");
                if (individual != null)
                    OurCompany.INN = GetAttributeStringValue(individual.Attribute("ИННФЛ")); 
            }

            XElement lactivity = organization.Element("Деятельность").Element("Лицензируемая");
            if (lactivity != null)
            {
                foreach (XNode node in lactivity.Elements("Лицензия"))
                {
                    RarLicense license = new RarLicense();
                    XElement el = (XElement)node;
                    license.SeriesNumber =  GetAttributeStringValue(el.Attribute("СерНомЛиц"));
                    license.DateFrom =      DateTime.Parse(el.Attribute("ДатаНачЛиц").Value);
                    license.DateTo =        DateTime.Parse(el.Attribute("ДатаОконЛиц").Value);
                    license.BusinesType =   GetAttributeStringValue(el.Attribute("ВидДеят"));
                    OurCompany.LicenseList.Add(license);
                }
            }
            else
            {
                OurCompany.UnLisenseActivity = (string)organization.Element("Деятельность").Element("Нелицензируемая").Attribute("ВидДеят");
            }


        }
        private void SetupTurnoverData(XElement turnoverdata, RarFormF6 formF6)
        {
            RarSubdevision subdevision = new RarSubdevision();
            subdevision.Name = (string)turnoverdata.Attribute("Наим");
            subdevision.KPP = (string)turnoverdata.Attribute("КППЮЛ");
            //subdevision.SalePresented = (bool)turnoverdata.Attribute("НаличиеПоставки");
            //subdevision.ReturnPresented = (bool)turnoverdata.Attribute("НаличиеВозврата");
            subdevision.Adress = SetupAdress(turnoverdata.Element("АдрОрг"));
            formF6.OurCompany.SubdevisionList.Add(subdevision);
            foreach (XNode nodeAlcoCode in turnoverdata.Elements("Оборот"))
            {
                XElement elAlcoCode = (XElement)nodeAlcoCode;
                foreach (XNode nodeManufacturer in elAlcoCode.Elements("СведПроизвИмпорт"))
                {
                    XElement elManufacturer = (XElement)nodeManufacturer;
                    string manufacturID = (string)elManufacturer.Attribute("ИдПроизвИмп");
                    RarCompany manufacturer = formF6.ManufacturerList.Where(p => p.ID == manufacturID).First();
                    foreach (XNode nodeBuyer in elManufacturer.Elements("Получатель"))
                    {
                        XElement elBuyer = (XElement)nodeBuyer;
                        string buyerID = (string)elBuyer.Attribute("ИдПолучателя");
                        RarCompany buyer = formF6.BuyerList.Where(p => p.ID == buyerID).First();
                        buyer.IsUsed = true;
                        string licenseID = (string)elBuyer.Attribute("ИдЛицензии");
                        RarLicense license = buyer.LicenseList.Where(s => s.ID == licenseID).FirstOrDefault();

                        foreach (XNode nodeDocument in elBuyer.Elements("Поставка"))
                        {
                            XElement elDocument = (XElement)nodeDocument;

                            RarTurnoverData data = new RarTurnoverData();
                            data.Subdevision = subdevision;
                            data.AlcoCode = (string)elAlcoCode.Attribute("П000000000003");
                            data.Manufacturer = manufacturer;
                            data.Buyer = buyer;
                            data.License = license;

                            //data.NotificationDate=      DateTime.Parse(el.Element("СведПроизвИмпорт").Element("Получатель").Element("Поставка").Attribute("П000000000015").Value);
                            //data.NotificationNumber =   (string)el.Element("СведПроизвИмпорт").Element("Получатель").Element("Поставка").Attribute("П000000000016");
                            //data.NotificationTurnover = (double)el.Element("СведПроизвИмпорт").Element("Получатель").Element("Поставка").Attribute("П000000000017");
                            data.DocumentDate = DateTime.Parse(elDocument.Attribute("П000000000018").Value);
                            data.DocumentNumber = (string)elDocument.Attribute("П000000000019");
                            //data.CustomsDeclarationNumber = (string)el.Element("СведПроизвИмпорт").Element("Получатель").Element("Поставка").Attribute("П000000000020");
                            data.Turnover = (double)elDocument.Attribute("П000000000021");
                            formF6.TurnoverDataList.Add(data);
                        }
                    }
                }

            }
        }
        #endregion

        private bool IsDocumentValid(XDocument xdoc)
        {
            //Type type = Type.GetType("Rar.Model.ParserF6", false);
            //using (Stream str = type.Assembly.GetManifestResourceStream("Rar.Model.Resources.f6_010117.xsd"))
            //{
            //    using (XmlReader reader = new XmlTextReader(str))
            //    {
            //        XmlSchemaSet schemas = new XmlSchemaSet();
            //        schemas.Add("", reader);

            //        bool errors = false;
            //        List<string> errNodes = new List<string>();
            //        xdoc.Validate(schemas, (o, ee) =>
            //        {
            //            string errNode = ee.Message;
            //            errNodes.Add(errNode);
            //            errors = true;
            //        });
            //        if (errors)
            //        {
            //            string mess = "Не соответствует схеме: " + "\n";
            //            foreach (string item in errNodes) mess = mess + item + "\n";
            //            return false;
            //        }
            //        else return true;
            //    }
            //}

            return true;
        }

    }

  
}
