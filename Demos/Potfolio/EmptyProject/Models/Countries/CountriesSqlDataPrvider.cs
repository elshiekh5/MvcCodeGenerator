using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;
using Portfolio.Models;

namespace Portfolio
{
    /// <summary>
    /// News SQL data provider which represents the data access layer of Countries.
    /// </summary>
    public class CountriesSqlDataPrvider
    {

        #region --------------Instance--------------
        //------------------------------------------------------------------------------------------------------
        private static CountriesSqlDataPrvider _Instance;
        //--------------------------------------------------------------------
        /// <summary>
        /// Gets instance of CountriesSqlDataPrvider calss.
        /// <example>CountriesSqlDataPrvider dp=CountriesSqlDataPrvider.Instance.</example>
        /// </summary>
        //--------------------------------------------------------------------
        public static CountriesSqlDataPrvider Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new CountriesSqlDataPrvider();
                }
                return _Instance;
            }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region /*--------- GetSqlConnection ---------*\
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Creates and returns a new SqlConnection Which its connection string depends on AppSettings["Connectionstring"].
        /// </summary>
        /// <returns></returns>
        //--------------------------------------------------------------------
        public SqlConnection GetSqlConnection()
        {
            return new SqlConnection(ConfigurationSettings.AppSettings["Connectionstring"]);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region /*--------- Create ---------*\
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Converts the  object properties to SQL paramters and executes the create  procedure 
        /// and updates the object with the SQL data by reference.
        /// </summary>
        /// <param name="CountriesObj">Model object.</param>
        /// <returns>The result of create query.</returns>
        //--------------------------------------------------------------------
        public bool Create(CountriesModel CountriesObj)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[Countries_Create]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
				myCommand.Parameters.Add("@CountryID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
				myCommand.Parameters.Add("@Code", SqlDbType.NVarChar, 5).Value = CountriesObj.Code;
				myCommand.Parameters.Add("@EnglishName", SqlDbType.NVarChar, 255).Value = CountriesObj.EnglishName;
				myCommand.Parameters.Add("@ArabicName", SqlDbType.NVarChar, 255).Value = CountriesObj.ArabicName;
				myCommand.Parameters.Add("@TIMEZONE_H", SqlDbType.Int, 4).Value = CountriesObj.TIMEZONE_H;
				myCommand.Parameters.Add("@TIMEZONE_M", SqlDbType.Int, 4).Value = CountriesObj.TIMEZONE_M;
				myCommand.Parameters.Add("@Reg_ID", SqlDbType.Int, 4).Value = CountriesObj.Reg_ID;
				myCommand.Parameters.Add("@SimpleArName", SqlDbType.NVarChar, 255).Value = CountriesObj.SimpleArName;
				myCommand.Parameters.Add("@SimpleEnName", SqlDbType.NVarChar, 255).Value = CountriesObj.SimpleEnName;

                //----------------------------------------------------------------------------------------------
                // Execute the command
                //----------------------------------------------------------------------------------------------
                myConnection.Open();
                if (myCommand.ExecuteNonQuery() > 0)
                {
                    result = true;
                    //Get ID value from database and set it in object
				CountriesObj.CountryID = (int)myCommand.Parameters["@CountryID"].Value;

                }
                myConnection.Close();
                return result;
                //----------------------------------------------------------------------------------------------
            }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region /*--------- Update ---------*\
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Converts the object properties to SQL paramters and executes the update procedure.
        /// </summary>
        /// <param name="CountriesObj">Model object.</param>
        /// <returns>The result of update query.</returns>
        //--------------------------------------------------------------------
        public bool Update(CountriesModel CountriesObj)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[Countries_Update]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                
				myCommand.Parameters.Add("@CountryID", SqlDbType.Int, 4).Value = CountriesObj.CountryID;
				myCommand.Parameters.Add("@Code", SqlDbType.NVarChar, 5).Value = CountriesObj.Code;
				myCommand.Parameters.Add("@EnglishName", SqlDbType.NVarChar, 255).Value = CountriesObj.EnglishName;
				myCommand.Parameters.Add("@ArabicName", SqlDbType.NVarChar, 255).Value = CountriesObj.ArabicName;
				myCommand.Parameters.Add("@TIMEZONE_H", SqlDbType.Int, 4).Value = CountriesObj.TIMEZONE_H;
				myCommand.Parameters.Add("@TIMEZONE_M", SqlDbType.Int, 4).Value = CountriesObj.TIMEZONE_M;
				myCommand.Parameters.Add("@Reg_ID", SqlDbType.Int, 4).Value = CountriesObj.Reg_ID;
				myCommand.Parameters.Add("@SimpleArName", SqlDbType.NVarChar, 255).Value = CountriesObj.SimpleArName;
				myCommand.Parameters.Add("@SimpleEnName", SqlDbType.NVarChar, 255).Value = CountriesObj.SimpleEnName;

                //----------------------------------------------------------------------------------------------
                // Execute the command
                //----------------------------------------------------------------------------------------------
                myConnection.Open();
                if (myCommand.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
                myConnection.Close();
                return result;
                //----------------------------------------------------------------------------------------------
            }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region /*--------- Delete ---------*\
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Deletes single object .
        /// </summary>
        /// <param name="CountryID">The model id.</param>
        /// <returns>The result of delete query.</returns>
        //--------------------------------------------------------------------
        public bool Delete(int CountryID)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[Countries_Delete]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                if(CountryID>0)myCommand.Parameters.Add("@CountryID", SqlDbType.Int, 4).Value = CountryID;

                //----------------------------------------------------------------------------------------------
                // Execute the command
                //----------------------------------------------------------------------------------------------
                myConnection.Open();
                if (myCommand.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
                myConnection.Close();
                return result;
                //----------------------------------------------------------------------------------------------
            }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region /*--------- DeleteGroupofObjects ---------*\
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Deletes multible objects .
        /// </summary>
        /// <param name="ids">The array of ids.</param>
        //--------------------------------------------------------------------
        public void DeleteGroupofObjects(int[] ids)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[Countries_Delete]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                myCommand.Parameters.Add("@CountryID", SqlDbType.Int, 4);

                //----------------------------------------------------------------------------------------------
                // Execute the command
                //----------------------------------------------------------------------------------------------
                myConnection.Open();
                foreach (int id in ids)
                {
                    myCommand.Parameters["@CountryID"].Value = id;
                    myCommand.ExecuteNonQuery();
                }
                myConnection.Close();
                //----------------------------------------------------------------------------------------------
            }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region /*--------- GetPageByPage ---------*\
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets the spesific Records.
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns>The result of query.</returns>
        //--------------------------------------------------------------------
        public List<CountriesModel> GetPageByPage(int pageIndex, int pageSize, string sortname, string sortorder, string qtype, string query, out int totalRecords)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                //----------------------------------------------------------------------------------------------
                List<CountriesModel> CountriesList = new List<CountriesModel>();
                CountriesModel CountriesObj;
                //----------------------------------------------------------------------------------------------
                SqlCommand myCommand = new SqlCommand("[dbo].[Countries_GetPageByPage]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                myCommand.Parameters.Add("@PageIndex", SqlDbType.Int, 4).Value = pageIndex;
                myCommand.Parameters.Add("@PageSize", SqlDbType.Int, 4).Value = pageSize;
                if (!string.IsNullOrEmpty(sortname)) { myCommand.Parameters.Add("@SortName", SqlDbType.NVarChar,128).Value = sortname; }
                if (!string.IsNullOrEmpty(sortorder)) { myCommand.Parameters.Add("@SortOrder", SqlDbType.NVarChar, 128).Value = sortorder; }
                if (!string.IsNullOrEmpty(qtype)) { myCommand.Parameters.Add("@QType", SqlDbType.NVarChar, 128).Value = qtype; }
                if (!string.IsNullOrEmpty(query)) { myCommand.Parameters.Add("@Query", SqlDbType.NVarChar, 128).Value = query; }
                myCommand.Parameters.Add("@TotalRecords", SqlDbType.Int).Direction = ParameterDirection.Output;
                //----------------------------------------------------------------------------------------------
                // Execute the command
                //----------------------------------------------------------------------------------------------
                SqlDataReader dr;
                myConnection.Open();
                dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    CountriesObj = PopulateModelFromIDataReader(dr);
                    CountriesList.Add(CountriesObj);
                }
                dr.Close();
                totalRecords = (int)myCommand.Parameters["@TotalRecords"].Value;
                myConnection.Close();
                //----------------------------------------------------------------------------------------------
                return CountriesList;
                //----------------------------------------------------------------------------------------------
            }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region /*--------- Get ---------*\
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets the spesific Records.
        /// </summary>
        /// <param name="CountryID">The CountriesObj id.</param>
        /// <returns>Model object.</returns>
        //--------------------------------------------------------------------
        public List<CountriesModel> Get(int CountryID)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                //----------------------------------------------------------------------------------------------
                List<CountriesModel> CountriesList = new List<CountriesModel>();
                CountriesModel CountriesObj;
                //----------------------------------------------------------------------------------------------
                SqlCommand myCommand = new SqlCommand("[dbo].[Countries_Get]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                if(CountryID>0)myCommand.Parameters.Add("@CountryID", SqlDbType.Int, 4).Value = CountryID;

                 // Execute the command
                //----------------------------------------------------------------------------------------------
                SqlDataReader dr;
                myConnection.Open();
                dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    CountriesObj = PopulateModelFromIDataReader(dr);
                    CountriesList.Add(CountriesObj);
                }
                dr.Close();
                myConnection.Close();
                //----------------------------------------------------------------------------------------------
                return CountriesList;
                //----------------------------------------------------------------------------------------------
            }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region /*--------- PopulateModelFromIDataReader ---------*\
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Populates model from IDataReader .
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>Model object.</returns>
        //--------------------------------------------------------------------
        private CountriesModel PopulateModelFromIDataReader(IDataReader reader)
        {
            //Create a new object
            CountriesModel CountriesObj = new CountriesModel();
            //Fill the object with data

			//------------------------------------------------
			//[CountryID]
			//------------------------------------------------
			if (reader["CountryID"] != DBNull.Value)
			    CountriesObj.CountryID = (int)reader["CountryID"];
			//------------------------------------------------
			//------------------------------------------------
			//[Code]
			//------------------------------------------------
			if (reader["Code"] != DBNull.Value)
			    CountriesObj.Code = (string)reader["Code"];
			//------------------------------------------------
			//------------------------------------------------
			//[EnglishName]
			//------------------------------------------------
			if (reader["EnglishName"] != DBNull.Value)
			    CountriesObj.EnglishName = (string)reader["EnglishName"];
			//------------------------------------------------
			//------------------------------------------------
			//[ArabicName]
			//------------------------------------------------
			if (reader["ArabicName"] != DBNull.Value)
			    CountriesObj.ArabicName = (string)reader["ArabicName"];
			//------------------------------------------------
			//------------------------------------------------
			//[TIMEZONE_H]
			//------------------------------------------------
			if (reader["TIMEZONE_H"] != DBNull.Value)
			    CountriesObj.TIMEZONE_H = (int)reader["TIMEZONE_H"];
			//------------------------------------------------
			//------------------------------------------------
			//[TIMEZONE_M]
			//------------------------------------------------
			if (reader["TIMEZONE_M"] != DBNull.Value)
			    CountriesObj.TIMEZONE_M = (int)reader["TIMEZONE_M"];
			//------------------------------------------------
			//------------------------------------------------
			//[Reg_ID]
			//------------------------------------------------
			if (reader["Reg_ID"] != DBNull.Value)
			    CountriesObj.Reg_ID = (int)reader["Reg_ID"];
			//------------------------------------------------
			//------------------------------------------------
			//[SimpleArName]
			//------------------------------------------------
			if (reader["SimpleArName"] != DBNull.Value)
			    CountriesObj.SimpleArName = (string)reader["SimpleArName"];
			//------------------------------------------------
			//------------------------------------------------
			//[SimpleEnName]
			//------------------------------------------------
			if (reader["SimpleEnName"] != DBNull.Value)
			    CountriesObj.SimpleEnName = (string)reader["SimpleEnName"];
			//------------------------------------------------
            //Return the populated object
            return CountriesObj;
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
    }
}