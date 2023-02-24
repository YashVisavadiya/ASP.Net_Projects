namespace AddressBook_Replica.Models
{
    public class ContactCategoryModel
    {
        public int? ContactCategoryID { get; set; }

        public string ContactCategory { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }
    }

    public class ContactCategory_DropDownModel
    {
        public int ContactCategoryID { get; set; }

        public string ContactCategory { get; set; }
    }

    public class ContactCategory_SearchModel
    {
        public string ContactCategory { get; set; }
    }
}
