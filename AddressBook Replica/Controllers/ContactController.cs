using AddressBook_Replica.Models;
using AddressBook_Replica.Views.DAL;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook_Replica.Controllers
{
    public class ContactController : Controller
    {
        #region PRIVATE_VAR

        private IConfiguration Configuration;
        private static string file_name="";

        #endregion

        #region CONSTRUCTOR

        public ContactController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        #region SELECT_ALL

        public IActionResult Index()
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            return View("ContactList", dal.Contact_SelectAll(connectionString));
        }

        #endregion

        #region DELETE

        public IActionResult Delete(int ContactID)
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            if (dal.Contact_Delete(connectionString, ContactID))
            {
                TempData["Contact_Delete_Msg"] = "Contact Deleted Successfully.";
            }
            else
            {
                TempData["Contact_Delete_Msg"] = "Error in Contact Deletion.";
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region ADD

        public IActionResult Add(int? ContactID)
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            ViewBag.CountryList = dal.Country_DropDown(connectionString);

            ViewBag.StateList = new List<State_DropDownModel>();

            ViewBag.CityList = new List<City_DropDownModel>();

            ViewBag.ContactCategoryList = dal.ContactCategory_DropDown(connectionString);

            // Fetch Tuple

            if (ContactID != null)
            {
                ContactModel contactModel = dal.Contact_SelectByPk(connectionString, ContactID);
                file_name = contactModel.PhotoPath.ToString();

                State_DropDownByCountry(contactModel.CountryID);

                City_DropDownByState(contactModel.StateID);

                return View("ContactAddEdit", contactModel);
            }
            return View("ContactAddEdit");
        }

        #endregion

        #region SAVE

        public IActionResult Save(ContactModel contactModel)
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            if (contactModel.ContactID == null)
            {
                if (dal.Contact_Insert(connectionString, contactModel))
                {
                    TempData["Contact_Insert_Msg"] = "Contact Inserted Successfully.";
                }
                else
                {
                    TempData["Contact_Insert_Msg"] = "Error in Contact Insertion.";
                }
            }
            else
            {
                if (dal.Contact_Update(connectionString, contactModel,file_name))
                {
                    TempData["Contact_Update_Msg"] = "Contact Updated Successfully.";
                }
                else
                {
                    TempData["Contact_Update_Msg"] = "Error in Contact Updation.";
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Add");
        }

        #endregion

        #region STATE_DROPDOWN

        public IActionResult State_DropDownByCountry(int CountryID)
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            ViewBag.StateList = dal.State_DropDown(connectionString, CountryID);

            var state_model = ViewBag.StateList;
            return Json(state_model);
        }

        #endregion

        #region CITY_DROPDOWN

        public IActionResult City_DropDownByState(int StateID)
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            ViewBag.CityList = dal.City_DropDown(connectionString, StateID);

            var city_model = ViewBag.CityList;
            return Json(city_model);
        }

        #endregion

        #region SEARCH_BOX

        public IActionResult Search()
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            Contact_SearchModel contact_SearchModel = new Contact_SearchModel();

            contact_SearchModel.CountryName = HttpContext.Request.Form["CountryName"].ToString();
            contact_SearchModel.StateName = HttpContext.Request.Form["StateName"].ToString();
            contact_SearchModel.CityName = HttpContext.Request.Form["CityName"].ToString();
            contact_SearchModel.Category = HttpContext.Request.Form["ContactCategory"].ToString();
            contact_SearchModel.Name = HttpContext.Request.Form["Name"].ToString();
            contact_SearchModel.Email = HttpContext.Request.Form["Email"].ToString();
            contact_SearchModel.Mobile = HttpContext.Request.Form["Mobile"].ToString();

            return View("ContactList",dal.Contact_Search(connectionString,contact_SearchModel));
        }

        #endregion

    }
}
