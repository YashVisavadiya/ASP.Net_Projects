using AddressBook_Replica.Models;
using AddressBook_Replica.Views.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace AddressBook_Replica.Controllers
{
    public class CountryController : Controller
    {

        #region PRIVATE_VAR

        private IConfiguration Configuration;

        #endregion

        #region CONSTRUCTOR

        public CountryController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        #region SELECT_ALL

        public IActionResult Index()
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            return View("CountryList", dal.Country_SelectAll(connectionString));
        }

        #endregion

        #region DELETE_BY_PK

        public IActionResult Delete(int CountryID)
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            if (dal.Country_Delete(connectionString, CountryID))
            {
                TempData["Country_Delete_Msg"] = "Country Deleted Successfully.";
            }
            else
            {
                TempData["Country_Delete_Msg"] = "Error in Country Deletion.";
            }

            return RedirectToAction("Index");
        }

        #endregion

        #region ADD

        public IActionResult Add(int? CountryID)
        {
            if (CountryID != null)
            {
                string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
                DAL dal = new DAL();

                return View("CountryAddEdit", dal.Country_SelectByPK(connectionString, CountryID));
            }
            return View("CountryAddEdit");
        }

        #endregion

        #region SAVE

        public IActionResult Save(CountryModel countryModel)
        {

            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            if (countryModel.CountryID == null)
            {
                if (dal.Country_Insert(connectionString, countryModel))
                {
                    TempData["Country_Insert_Msg"] = "Country Inserted Successfully.";
                }
                else
                {
                    TempData["Country_Insert_Msg"] = "Error in Country Insertion.";
                }
            }
            else
            {
                if (dal.Country_Update(connectionString, countryModel))
                {
                    TempData["Country_Update_Msg"] = "Country Updated Successfully.";
                }
                else
                {
                    TempData["Country_Update_Msg"] = "Error in Country Updation.";
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

            Country_SearchModel country_SearchModel = new Country_SearchModel();

            country_SearchModel.CountryName = HttpContext.Request.Form["CountryName"].ToString();
            country_SearchModel.CountryCode = HttpContext.Request.Form["CountryCode"].ToString();

            ViewBag.CountryName = country_SearchModel.CountryName;
            ViewBag.CountryCode = country_SearchModel.CountryCode;

            return View("CountryList", dal.Country_Search(connectionString,country_SearchModel));
        }

        #endregion

    }
}
