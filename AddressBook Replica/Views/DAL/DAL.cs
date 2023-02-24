using AddressBook_Replica.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace AddressBook_Replica.Views.DAL
{
    public class DAL : DAL_Base
    {
        #region COUNTRY_DROPDOWN

        public List<Country_DropDownModel> Country_DropDown(string connectionString)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_LOC_Country_SelectComboBox]");

                DataTable dt = new DataTable();
                List<Country_DropDownModel> country_list = new List<Country_DropDownModel>();

                using (IDataReader dataReader = database.ExecuteReader(command))
                {
                    dt.Load(dataReader);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            Country_DropDownModel tuple = new Country_DropDownModel();
                            tuple.CountryID = Convert.ToInt32(dr["CountryID"]);
                            tuple.CountryName = Convert.ToString(dr["CountryName"]);
                            country_list.Add(tuple);
                        }
                    }
                }
                return country_list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        #endregion

        #region STATE_DROPDOWN

        public List<State_DropDownModel> State_DropDown(string connectionString, int CountryID)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_LOC_State_SelectComboBoxByCountryID]");

                database.AddInParameter(command, "@CountryID", DbType.Int32, CountryID);

                DataTable dt = new DataTable();
                List<State_DropDownModel> state_list = new List<State_DropDownModel>();

                using (IDataReader dataReader = database.ExecuteReader(command))
                {
                    dt.Load(dataReader);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            State_DropDownModel tuple = new State_DropDownModel();
                            tuple.StateID = Convert.ToInt32(dr["StateID"]);
                            tuple.StateName = Convert.ToString(dr["StateName"]);
                            state_list.Add(tuple);
                        }
                    }
                }
                return state_list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        #endregion

        #region CITY_DROPDOWN

        public List<City_DropDownModel> City_DropDown(string connectionString, int StateID)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_LOC_City_SelectComboBoxByStateID]");

                database.AddInParameter(command, "@StateID", DbType.Int32, StateID);

                DataTable dt = new DataTable();
                List<City_DropDownModel> city_list = new List<City_DropDownModel>();

                using (IDataReader dataReader = database.ExecuteReader(command))
                {
                    dt.Load(dataReader);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            City_DropDownModel tuple = new City_DropDownModel();
                            tuple.CityID = Convert.ToInt32(dr["CityID"]);
                            tuple.CityName = Convert.ToString(dr["CityName"]);
                            city_list.Add(tuple);
                        }
                    }
                }
                return city_list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        #endregion

        #region CONTACTCATEGORY_DROPDOWN

        public List<ContactCategory_DropDownModel> ContactCategory_DropDown(string connectionString)
        {
            try
            {
                SqlDatabase database = new SqlDatabase(connectionString);
                DbCommand command = database.GetStoredProcCommand("[dbo].[PR_CON_ContactCategory_SelectComboBox]");

                DataTable dt = new DataTable();
                List<ContactCategory_DropDownModel> contactCategory_list = new List<ContactCategory_DropDownModel>();

                using (IDataReader dataReader = database.ExecuteReader(command))
                {
                    dt.Load(dataReader);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            ContactCategory_DropDownModel tuple = new ContactCategory_DropDownModel();
                            tuple.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                            tuple.ContactCategory = Convert.ToString(dr["ContactCategory"]);
                            contactCategory_list.Add(tuple);
                        }
                    }
                }
                return contactCategory_list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        #endregion
    }
}
