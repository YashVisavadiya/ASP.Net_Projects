namespace AddressBook_Replica.Models
{
    public class CountryModel
    {
        public int? CountryID { get; set; }

        public string CountryName { get; set; }

        public string CountryCode { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }
    }

    public class Country_DropDownModel
    {
        public int CountryID { get; set;}

        public string CountryName { get; set;}
    }

    public class Country_SearchModel
    {
        public string CountryName { get; set;}

        public string CountryCode { get; set; }
    }
}
