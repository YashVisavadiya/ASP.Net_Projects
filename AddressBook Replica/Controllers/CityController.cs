using AddressBook_Replica.Models;
using AddressBook_Replica.Views.DAL;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook_Replica.Controllers
{
    public class CityController : Controller
    {

        #region PRIVATE_VAR

        private IConfiguration Configuration;

        #endregion

        #region CONSTRUCTOR

        public CityController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        #region SELECT_ALL

        public IActionResult Index()
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            return View("CityList", dal.City_SelectAll(connectionString));
        }

        #endregion

        #region DELETE

        public IActionResult Delete(int CityID)
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            if (dal.City_Delete(connectionString, CityID))
            {
                TempData["City_Delete_Msg"] = "City Deleted Successfully.";
            }
            else
            {
                TempData["City_Delete_Msg"] = "Error in City Deletion.";
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region ADD

        public IActionResult Add(int? CityID)
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            ViewBag.CountryList = dal.Country_DropDown(connectionString);

            ViewBag.StateList = new List<State_DropDownModel>();

            // Fetch Tuple

            if (CityID != null)
            {
                CityModel cityModel = dal.City_SelectByPk(connectionString, CityID);

                State_DropDownByCountry(cityModel.CountryID);

                return View("CityAddEdit", cityModel);
            }
            return View("CityAddEdit");
        }

        #endregion

        #region SAVE

        public IActionResult Save(CityModel CityModel)
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            if (CityModel.CityID == null)
            {
                if (dal.City_Insert(connectionString, CityModel))
                {
                    TempData["City_Insert_Msg"] = "City Inserted Successfully.";
                }
                else
                {
                    TempData["City_Insert_Msg"] = "Error in City Insertion.";
                }
            }
            else
            {
                if (dal.City_Update(connectionString, CityModel))
                {
                    TempData["City_Update_Msg"] = "City Updated Successfully.";
                }
                else
                {
                    TempData["City_Update_Msg"] = "Error in City Updation.";
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

        #region SEARCH_BOX

        public IActionResult Search()
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            City_SearchModel city_SearchModel = new City_SearchModel();

            city_SearchModel.CountryName = HttpContext.Request.Form["CountryName"].ToString();
            city_SearchModel.StateName = HttpContext.Request.Form["StateName"].ToString();
            city_SearchModel.CityName = HttpContext.Request.Form["CityName"].ToString();

            return View("CityList", dal.City_Search(connectionString, city_SearchModel));
        }

        #endregion

    }
}
