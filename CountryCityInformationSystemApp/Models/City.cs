using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CountryCityInformationSystemApp.Models
{
    public class City
    {
        public int CityID { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public int NoOfDwellers{ get; set; }
        public string Location{ get; set; }
        public string Weather{ get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}