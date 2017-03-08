using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace MvvmRar.Rar
{
    class RarCompaniesFormatter
    {

        private XElement GetAdressElement(RarAdress adress)
        {
            XElement el = new XElement("П000000000008",
                new XElement("КодСтраны", "643"),
                new XElement("Индекс", adress.PostCode),
                new XElement("КодРегион", adress.RegionId),
                new XElement("Район", adress.District),
                new XElement("Город", adress.City),
                new XElement("НаселПункт", adress.Locality),
                new XElement("Улица", adress.Street),
                new XElement("Дом", adress.Building),
                new XElement("Корпус", adress.Block),
                new XElement("Литера", adress.Litera),
                new XElement("Кварт", adress.Apartment)
                );

            return el;
        }
        private XElement GetCompanyElement(RarCompany company)
        {
            if ((company.Adress.CountryId == null) || (company.Adress.CountryId == "643"))
            {
                XElement domestic = new XElement("Резидент",
                    GetAdressElement(company.Adress),
                    new XElement("ЮЛ",
                        new XAttribute("П000000000009", company.INN == null ? "" : company.INN),
                        new XAttribute("П000000000010", company.KPP == null ? "" : company.KPP)),
                    new XElement("Производитель", new XAttribute("value", "True")),
                    new XElement("Перевозчик", new XAttribute("value", "False"))
                    );
                return domestic;
            }

            XElement foreigner = new XElement("Иностр",
             new XAttribute("П000000000081", company.Adress.CountryId));

            return foreigner;


        }
        public void SaveCompanies(List<RarCompany> companyList, string filename)
        {
            int i = 1;
            XDocument xdoc = new XDocument(
                 new XDeclaration("1.0", "windows-1251", "yes"),
                 new XElement("Файл",
                 new XAttribute(XNamespace.Xmlns + "xs", "http://www.w3.org/2001/XMLSchema"),
                 new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                     companyList.Select(p => new XElement("Контрагенты",
                     new XAttribute("ИдКонтр", i++),
                     new XAttribute("П000000000007", p.Name),
                     GetCompanyElement(p))
                     )
                 )
             );
            xdoc.Save(filename);
        }
        public void SaveTurnoverData(List<RarTurnoverData> turnoverList, string filename)
        {

            XDocument xdoc = new XDocument(
                new XDeclaration("1.0", "windows-1251", "yes"),
                new XElement("Файл",
                new XAttribute("ДатаДок", DateTime.Now.ToShortDateString()),
                new XAttribute("ВерсФорм", "4.20"),
                new XAttribute("НаимПрог", "1C"),
                    new XElement("Документ",
                        GetAlcoCodes(turnoverList)
                ))
            );
            xdoc.Save(filename);
        }
        private XElement[] GetAlcoCodes(List<RarTurnoverData> turnoverList)
        {
            int i = 1;
            List<string> alcoCodeList = turnoverList.Select(a => a.AlcoCode).Distinct().ToList();
            return alcoCodeList.Select(p =>
                new XElement("Оборот",
                new XAttribute("ПN", i++),
                new XAttribute("П000000000003", p),
                    GetManufacturList(turnoverList, p)
                )
            ).ToArray();
        }
        private XElement[] GetManufacturList(List<RarTurnoverData> turnoverList, string alcoCode)
        {

            List<RarTurnoverData> cutTurnoverList = turnoverList.Where(s => s.AlcoCode == alcoCode).ToList();
            List<RarCompany> manufacturList = cutTurnoverList.Select(a => a.Manufacturer).Distinct().ToList();

            return manufacturList.Select(p =>
                new XElement("СведПроизвИмпорт",
                    new XAttribute("NameOrg", p.Name),
                    new XAttribute("INN", p.INN),
                    new XAttribute("KPP", p.KPP),
                        GetProductList(cutTurnoverList, p)
                    )
            ).ToArray();
        }
        private XElement[] GetProductList(List<RarTurnoverData> turnoverList, RarCompany manufacture)
        {
            List<RarTurnoverData> cutTurnoverList = turnoverList.Where(s => s.Manufacturer == manufacture).ToList();

            return cutTurnoverList.Select(p =>
                new XElement("Продукция",
                    new XAttribute("П200000000013", p.DocumentDate.ToShortDateString()),
                    new XAttribute("П200000000014", p.DocumentNumber),
                    p.CustomsDeclarationNumber == null ? null : new XAttribute("П200000000015", p.CustomsDeclarationNumber),
                    new XAttribute("П200000000016", p.Turnover)
                    )
            ).ToArray();

        }

    }
}
