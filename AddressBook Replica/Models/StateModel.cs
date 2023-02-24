namespace AddressBook_Replica.Models
{
    public class StateModel
    {

        public int? StateID { get; set; }

        public string StateName { get; set; }

        public int CountryID { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }
    }

    public class State_DropDownModel
    {
        public int StateID { get; set; }

        public string StateName { get; set; }
    }

    public class State_SearchModel
    {
        public string CountryName { get; set; }

        public string StateName { get; set; }
    }
}
