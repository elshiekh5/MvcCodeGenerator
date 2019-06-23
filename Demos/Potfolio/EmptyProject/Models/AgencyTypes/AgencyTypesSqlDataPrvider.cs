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
    /// News SQL data provider which represents the data access layer of AgencyTypes.
    /// </summary>
    public class AgencyTypesSqlDataPrvider
    {

        #region --------------Instance--------------
        //------------------------------------------------------------------------------------------------------
        private static AgencyTypesSqlDataPrvider _Instance;
        //--------------------------------------------------------------------
        /// <summary>
        /// Gets instance of AgencyTypesSqlDataPrvider calss.
        /// <example>AgencyTypesSqlDataPrvider dp=AgencyTypesSqlDataPrvider.Instance.</example>
        /// </summary>
        //--------------------------------------------------------------------
        public static AgencyTypesSqlDataPrvider Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new AgencyTypesSqlDataPrvider();
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
        /// <param name="AgencyTypesObj">Model object.</param>
        /// <returns>The result of create query.</returns>
        //--------------------------------------------------------------------
        public bool Create(AgencyTypesModel AgencyTypesObj)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[AgencyTypes_Create]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
				myCommand.Parameters.Add("@AgencyTypeID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
				myCommand.Parameters.Add("@Title", SqlDbType.NVarChar, 128).Value = AgencyTypesObj.Title;

                //----------------------------------------------------------------------------------------------
                // Execute the command
                //----------------------------------------------------------------------------------------------
                myConnection.Open();
                if (myCommand.ExecuteNonQuery() > 0)
                {
                    result = true;
                    //Get ID value from database and set it in object
				AgencyTypesObj.AgencyTypeID = (int)myCommand.Parameters["@AgencyTypeID"].Value;

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
        /// <param name="AgencyTypesObj">Model object.</param>
        /// <returns>The result of update query.</returns>
        //--------------------------------------------------------------------
        public bool Update(AgencyTypesModel AgencyTypesObj)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[AgencyTypes_Update]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                
				myCommand.Parameters.Add("@AgencyTypeID", SqlDbType.Int, 4).Value = AgencyTypesObj.AgencyTypeID;
				myCommand.Parameters.Add("@Title", SqlDbType.NVarChar, 128).Value = AgencyTypesObj.Title;

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
        /// <param name="AgencyTypeID">The model id.</param>
        /// <returns>The result of delete query.</returns>
        //--------------------------------------------------------------------
        public bool Delete(int AgencyTypeID)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[AgencyTypes_Delete]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                if(AgencyTypeID>0)myCommand.Parameters.Add("@AgencyTypeID", SqlDbType.Int, 4).Value = AgencyTypeID;

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
                SqlCommand myCommand = new SqlCommand("[dbo].[AgencyTypes_Delete]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                myCommand.Parameters.Add("@AgencyTypeID", SqlDbType.Int, 4);

                //----------------------------------------------------------------------------------------------
                // Execute the command
                //----------------------------------------------------------------------------------------------
                myConnection.Open();
                foreach (int id in ids)
                {
                    myCommand.Parameters["@AgencyTypeID"].Value = id;
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
        public List<AgencyTypesModel> GetPageByPage(int pageIndex, int pageSize, string sortname, string sortorder, string qtype, string query, out int totalRecords)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                //----------------------------------------------------------------------------------------------
                List<AgencyTypesModel> AgencyTypesList = new List<AgencyTypesModel>();
                AgencyTypesModel AgencyTypesObj;
                //----------------------------------------------------------------------------------------------
                SqlCommand myCommand = new SqlCommand("[dbo].[AgencyTypes_GetPageByPage]", myConnection);
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
                    AgencyTypesObj = PopulateModelFromIDataReader(dr);
                    AgencyTypesList.Add(AgencyTypesObj);
                }
                dr.Close();
                totalRecords = (int)myCommand.Parameters["@TotalRecords"].Value;
                myConnection.Close();
                //----------------------------------------------------------------------------------------------
                return AgencyTypesList;
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
        /// <param name="AgencyTypeID">The AgencyTypesObj id.</param>
        /// <returns>Model object.</returns>
        //--------------------------------------------------------------------
        public List<AgencyTypesModel> Get(int AgencyTypeID)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                //----------------------------------------------------------------------------------------------
                List<AgencyTypesModel> AgencyTypesList = new List<AgencyTypesModel>();
                AgencyTypesModel AgencyTypesObj;
                //----------------------------------------------------------------------------------------------
                SqlCommand myCommand = new SqlCommand("[dbo].[AgencyTypes_Get]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                if(AgencyTypeID>0)myCommand.Parameters.Add("@AgencyTypeID", SqlDbType.Int, 4).Value = AgencyTypeID;

                 // Execute the command
                //----------------------------------------------------------------------------------------------
                SqlDataReader dr;
                myConnection.Open();
                dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    AgencyTypesObj = PopulateModelFromIDataReader(dr);
                    AgencyTypesList.Add(AgencyTypesObj);
                }
                dr.Close();
                myConnection.Close();
                //----------------------------------------------------------------------------------------------
                return AgencyTypesList;
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
        private AgencyTypesModel PopulateModelFromIDataReader(IDataReader reader)
        {
            //Create a new object
            AgencyTypesModel AgencyTypesObj = new AgencyTypesModel();
            //Fill the object with data

			//------------------------------------------------
			//[AgencyTypeID]
			//------------------------------------------------
			if (reader["AgencyTypeID"] != DBNull.Value)
			    AgencyTypesObj.AgencyTypeID = (int)reader["AgencyTypeID"];
			//------------------------------------------------
			//------------------------------------------------
			//[Title]
			//------------------------------------------------
			if (reader["Title"] != DBNull.Value)
			    AgencyTypesObj.Title = (string)reader["Title"];
			//------------------------------------------------
            //Return the populated object
            return AgencyTypesObj;
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
    }
}