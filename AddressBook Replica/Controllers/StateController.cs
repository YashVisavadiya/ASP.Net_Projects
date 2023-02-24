using AddressBook_Replica.Models;
using AddressBook_Replica.Views.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace AddressBook_Replica.Controllers
{
    public class StateController : Controller
    {

        #region PRIVATE_VAR

        private IConfiguration Configuration;

        #endregion

        #region CONSTRUCTOR

        public StateController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        #region SELECT_ALL

        public IActionResult Index()
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            return View("StateList", dal.State_SelectAll(connectionString));
        }

        #endregion

        #region DELETE

        public IActionResult Delete(int StateID)
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            if (dal.State_Delete(connectionString, StateID))
            {
                TempData["State_Delete_Msg"] = "State Deleted Successfully.";
            }
            else
            {
                TempData["State_Delete_Msg"] = "Error in State Deletion.";
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region ADD

        public IActionResult Add(int? StateID)
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            ViewBag.CountryList = dal.Country_DropDown(connectionString);

            if (StateID != null)
            {
                return View("StateAddEdit", dal.State_SelectByPk(connectionString, StateID));
            }
            return View("StateAddEdit");
        }

        #endregion

        #region SAVE

        public IActionResult Save(StateModel stateModel) 
        {
            string connectionString = this.Configuration.GetConnectionString("AddressBook Replica");
            DAL dal = new DAL();

            if(stateModel.StateID == null)
            {
                if (dal.State_Insert(connectionString, stateModel))
                {
                    TempData["State_Insert_Msg"] = "State Inserted Successfully.";
                }
                else
                {
                    TempData["State_Insert_Msg"] = "Error in State Insertion.";
                }
            }
            else
            {
                if (dal.State_Update(connectionString,stateModel))
                {
                    TempData["State_Update_Msg"] = "State Updated Successfully.";
                }
                else
                {
                    TempData["State_Update_Msg"] = "Error in State Updation.";
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

            State_SearchModel state_SearchModel = new State_SearchModel();

            state_SearchModel.CountryName = HttpContext.Request.Form["CountryName"].ToString();
            state_SearchModel.StateName = HttpContext.Request.Form["StateName"].ToString();

            return View("StateList", dal.State_Search(connectionString,state_SearchModel));
        }

        #endregion

    }
}
