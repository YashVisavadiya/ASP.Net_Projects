using AddressBook_Replica.Models;
using AddressBook_Replica.Views.DAL;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook_Replica.Controllers
{
    public class ContactCategoryController : Controller
    {

        #region PRIVATE_VAR

        private IConfiguration Configuration;

        #endregion

        #region CONSTRUCTOR

        public ContactCategoryController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        #region SELECT_ALL

        public IActionResult Index()
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            return View("ContactCategoryList", dal.ContactCategory_SelectAll(connectionString));
        }

        #endregion

        #region DELETE_BY_PK

        public IActionResult Delete(int ContactCategoryID)
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            if (dal.ContactCategory_Delete(connectionString, ContactCategoryID))
            {
                TempData["ContactCategory_Delete_Msg"] = "Contact Category Deleted Successfully.";
            }
            else
            {
                TempData["ContactCategory_Delete_Msg"] = "Error in Contact Category Deletion.";
            }

            return RedirectToAction("Index");
        }

        #endregion

        #region ADD

        public IActionResult Add(int? ContactCategoryID)
        {
            if (ContactCategoryID != null)
            {
                string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
                DAL dal = new DAL();

                return View("ContactCategoryAddEdit", dal.ContactCategory_SelectByPK(connectionString, ContactCategoryID));
            }
            return View("ContactCategoryAddEdit");
        }

        #endregion

        #region SAVE

        public IActionResult Save(ContactCategoryModel ContactCategoryModel)
        {

            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            if (ContactCategoryModel.ContactCategoryID == null)
            {
                if (dal.ContactCategory_Insert(connectionString, ContactCategoryModel))
                {
                    TempData["ContactCategory_Insert_Msg"] = "Contact Category Inserted Successfully.";
                }
                else
                {
                    TempData["ContactCategory_Insert_Msg"] = "Error in Contact Category Insertion.";
                }
            }
            else
            {
                if (dal.ContactCategory_Update(connectionString, ContactCategoryModel))
                {
                    TempData["ContactCategory_Update_Msg"] = "Contact Category Updated Successfully.";
                }
                else
                {
                    TempData["ContactCategory_Update_Msg"] = "Error in Contact Category Updation.";
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Add");
        }

        #endregion

        #region SEARCH_BOX

        public IActionResult Search()
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            ContactCategory_SearchModel contactCategory_SearchModel = new ContactCategory_SearchModel();

            contactCategory_SearchModel.ContactCategory = HttpContext.Request.Form["ContactCategory"].ToString();

            ViewBag.ContactCategory = contactCategory_SearchModel.ContactCategory;

            return View("ContactCategoryList", dal.ContactCategory_Search(connectionString, contactCategory_SearchModel));
        }

        #endregion
    }
}
