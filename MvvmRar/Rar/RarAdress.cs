using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmRar.Rar
{
    public class RarAdress
    {
        #region - Public Properties -
        public string CountryId { set; get; }
        public string PostCode { set; get; }
        public string RegionId { set; get; }
        public string District { set; get; }
        public string City { set; get; }
        public string Locality { set; get; }
        public string Street { set; get; }
        public string Building { set; get; }
        public string Block { set; get; }
        public string Litera { set; get; }
        public string Apartment { set; get; }

        public bool StrictAdress { set; get; }
        public string AdressString { set; get; }
        #endregion
        #region - Constructors -
        public RarAdress()
        {
            CountryId = "";
            PostCode = "";
            RegionId = "";
            District = "";
            City = "";
            Locality = "";
            Street = "";
            Building = "";
            Block = "";
            Litera = "";
            Apartment = "";
            StrictAdress = false;
            AdressString = "";
        }
        public RarAdress(string countryId, string postCode, string regionId, string district,
            string city, string locality, string street, string building,
            string block, string litera, string apartment)
        {
            CountryId = countryId ?? "";
            PostCode = postCode ?? "";
            RegionId = regionId ?? "";
            District = district ?? "";
            City = city ?? "";
            Locality = locality ?? "";
            Street = street ?? "";
            Building = building ?? "";
            Block = block ?? "";
            Litera = litera ?? "";
            Apartment = apartment ?? "";
            StrictAdress = true;
            AdressString = "";
        }
        public RarAdress(string adress)
        {
            StrictAdress = false;
            AdressString = adress;

            CountryId = "";
            PostCode = "";
            RegionId = "";
            District = "";
            City = "";
            Locality = "";
            Street = "";
            Building = "";
            Block = "";
            Litera = "";
            Apartment = "";
        }

        public RarAdress(RarAdress adress)
        {
            CountryId = adress.CountryId;
            PostCode = adress.PostCode;
            RegionId = adress.RegionId;
            District = adress.District;
            City = adress.City;
            Locality = adress.Locality;
            Street = adress.Street;
            Building = adress.Building;
            Block = adress.Block;
            Litera = adress.Litera;
            Apartment = adress.Apartment;
            StrictAdress = adress.StrictAdress;
            AdressString = adress.AdressString;
        }
        #endregion
        public override string ToString()
        {
            if (StrictAdress)
            {
                return CountryId + ", " + PostCode + ", " + RegionId + ", " + District + ", " +
                City + ", " + Locality + ", " + Street + ", " + Building + ", " + Block + ", " + Litera + ", " + Apartment;
            }
            else
            {
                return AdressString;
            }

        }
    }
}
