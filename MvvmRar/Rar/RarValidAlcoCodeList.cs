using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace MvvmRar.Rar
{
    class RarValidAlcoCodeList
    {
        public static List<string> GetAlcoCodesListFromXSD()
        {
            List<string> listString = new List<string>();
            XDocument xdoc;

            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream str = assembly.GetManifestResourceStream("MvvmRar.Model.Resources.f6_010117.xsd"); //Должен быть внедренным
            using (XmlReader reader = new XmlTextReader(str))
                    xdoc = XDocument.Load(reader);

            XElement el = xdoc.Descendants().Where(item => (item.Attribute("name") != null) && (item.Attribute("name").Value == "П000000000003")).FirstOrDefault();
            XElement restriction = el.Element("{http://www.w3.org/2001/XMLSchema}simpleType").Element("{http://www.w3.org/2001/XMLSchema}restriction");
            foreach (XNode node in restriction.Elements("{http://www.w3.org/2001/XMLSchema}enumeration"))
            {
                XElement elAlcoCode = node as XElement;
                string val = (elAlcoCode.Attribute("value")).Value;
                listString.Add(val);
            }
            return listString;

        }
    }
}
