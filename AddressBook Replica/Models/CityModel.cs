using Microsoft.AspNetCore.Authentication;

namespace AddressBook_Replica.Models
{
    public class CityModel
    {
        public int? CityID { get; set; }

        public string CityName { get; set; }

        public int CountryID { get; set; }

        public int StateID { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }
    }

    public class City_DropDownModel
    {
        public int CityID { get; set; }

        public string CityName { get; set; }
    }

    public class City_SearchModel
    {
        public string CountryName { get; set; }

        public string StateName { get; set; }

        public string CityName { get; set; }
    }
}
