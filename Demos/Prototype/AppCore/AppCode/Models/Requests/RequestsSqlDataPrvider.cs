using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;


namespace AppCore
{
    /// <summary>
    /// News SQL data provider which represents the data access layer of Requests.
    /// </summary>
    public class RequestsSqlDataPrvider
    {

        #region --------------Instance--------------
        //------------------------------------------------------------------------------------------------------
        private static RequestsSqlDataPrvider _Instance;
        //--------------------------------------------------------------------
        /// <summary>
        /// Gets instance of RequestsSqlDataPrvider calss.
        /// <example>RequestsSqlDataPrvider dp=RequestsSqlDataPrvider.Instance.</example>
        /// </summary>
        //--------------------------------------------------------------------
        public static RequestsSqlDataPrvider Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new RequestsSqlDataPrvider();
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
        /// <param name="RequestsObj">Model object.</param>
        /// <returns>The result of create query.</returns>
        //--------------------------------------------------------------------
        public bool Create(RequestsModel RequestsObj)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[Requests_Create]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
				myCommand.Parameters.Add("@RequestID", SqlDbType.Int, 4).Value = RequestsObj.RequestID;
				myCommand.Parameters.Add("@RequestTime", SqlDbType.DateTime2, 8).Value = RequestsObj.RequestTime;

                //----------------------------------------------------------------------------------------------
                // Execute the command
                //----------------------------------------------------------------------------------------------
                myConnection.Open();
                if (myCommand.ExecuteNonQuery() > 0)
                {
                    result = true;
                    //Get ID value from database and set it in object

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
        /// <param name="RequestsObj">Model object.</param>
        /// <returns>The result of update query.</returns>
        //--------------------------------------------------------------------
        public bool Update(RequestsModel RequestsObj)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[Requests_Update]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                
				myCommand.Parameters.Add("@RequestID", SqlDbType.Int, 4).Value = RequestsObj.RequestID;
				myCommand.Parameters.Add("@RequestTime", SqlDbType.DateTime2, 8).Value = RequestsObj.RequestTime;

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
        /// <param name="RequestID">The model id.</param>
        /// <returns>The result of delete query.</returns>
        //--------------------------------------------------------------------
        public bool Delete(int RequestID)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[Requests_Delete]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                if(RequestID>0)myCommand.Parameters.Add("@RequestID", SqlDbType.Int, 4).Value = RequestID;

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
                SqlCommand myCommand = new SqlCommand("[dbo].[Requests_Delete]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                myCommand.Parameters.Add("@RequestID", SqlDbType.Int, 4);

                //----------------------------------------------------------------------------------------------
                // Execute the command
                //----------------------------------------------------------------------------------------------
                myConnection.Open();
                foreach (int id in ids)
                {
                    myCommand.Parameters["@RequestID"].Value = id;
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
        public List<RequestsModel> GetPageByPage(int pageIndex, int pageSize, string sortname, string sortorder, string qtype, string query, out int totalRecords)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                //----------------------------------------------------------------------------------------------
                List<RequestsModel> RequestsList = new List<RequestsModel>();
                RequestsModel RequestsObj;
                //----------------------------------------------------------------------------------------------
                SqlCommand myCommand = new SqlCommand("[dbo].[Requests_GetPageByPage]", myConnection);
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
                    RequestsObj = PopulateModelFromIDataReader(dr);
                    RequestsList.Add(RequestsObj);
                }
                dr.Close();
                totalRecords = (int)myCommand.Parameters["@TotalRecords"].Value;
                myConnection.Close();
                //----------------------------------------------------------------------------------------------
                return RequestsList;
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
        /// <param name="RequestID">The RequestsObj id.</param>
        /// <returns>Model object.</returns>
        //--------------------------------------------------------------------
        public List<RequestsModel> Get(int RequestID)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                //----------------------------------------------------------------------------------------------
                List<RequestsModel> RequestsList = new List<RequestsModel>();
                RequestsModel RequestsObj;
                //----------------------------------------------------------------------------------------------
                SqlCommand myCommand = new SqlCommand("[dbo].[Requests_Get]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                if(RequestID>0)myCommand.Parameters.Add("@RequestID", SqlDbType.Int, 4).Value = RequestID;

                 // Execute the command
                //----------------------------------------------------------------------------------------------
                SqlDataReader dr;
                myConnection.Open();
                dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    RequestsObj = PopulateModelFromIDataReader(dr);
                    RequestsList.Add(RequestsObj);
                }
                dr.Close();
                myConnection.Close();
                //----------------------------------------------------------------------------------------------
                return RequestsList;
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
        private RequestsModel PopulateModelFromIDataReader(IDataReader reader)
        {
            //Create a new object
            RequestsModel RequestsObj = new RequestsModel();
            //Fill the object with data

			//------------------------------------------------
			//[RequestID]
			//------------------------------------------------
			if (reader["RequestID"] != DBNull.Value)
			    RequestsObj.RequestID = (int)reader["RequestID"];
			//------------------------------------------------
			//------------------------------------------------
			//[RequestTime]
			//------------------------------------------------
			if (reader["RequestTime"] != DBNull.Value)
			    RequestsObj.RequestTime = (DateTime)reader["RequestTime"];
			//------------------------------------------------
            //Return the populated object
            return RequestsObj;
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
    }
}