using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;
using IndexersCP4.Models;

namespace IndexersCP4
{
    /// <summary>
    /// News SQL data provider which represents the data access layer of RoundsLog.
    /// </summary>
    public class RoundsLogSqlDataPrvider
    {

        #region --------------Instance--------------
        //------------------------------------------------------------------------------------------------------
        private static RoundsLogSqlDataPrvider _Instance;
        //--------------------------------------------------------------------
        /// <summary>
        /// Gets instance of RoundsLogSqlDataPrvider calss.
        /// <example>RoundsLogSqlDataPrvider dp=RoundsLogSqlDataPrvider.Instance.</example>
        /// </summary>
        //--------------------------------------------------------------------
        public static RoundsLogSqlDataPrvider Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new RoundsLogSqlDataPrvider();
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
        /// <param name="RoundsLogObj">Model object.</param>
        /// <returns>The result of create query.</returns>
        //--------------------------------------------------------------------
        public bool Create(RoundsLogModel RoundsLogObj)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[RoundsLog_Create]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
				myCommand.Parameters.Add("@RoundID", SqlDbType.Int).Direction = ParameterDirection.Output;
				myCommand.Parameters.Add("@IndexerID", SqlDbType.Int).Value = RoundsLogObj.IndexerID;
				myCommand.Parameters.Add("@JournalCode", SqlDbType.VarChar).Value = RoundsLogObj.JournalCode;
				myCommand.Parameters.Add("@RoundDate", SqlDbType.Date).Value = RoundsLogObj.RoundDate;
				myCommand.Parameters.Add("@StatusID", SqlDbType.Int).Value = RoundsLogObj.StatusID;
				myCommand.Parameters.Add("@NextEvaRound", SqlDbType.NVarChar).Value = RoundsLogObj.NextEvaRound;
				myCommand.Parameters.Add("@Notes", SqlDbType.NVarChar).Value = RoundsLogObj.Notes;

                //----------------------------------------------------------------------------------------------
                // Execute the command
                //----------------------------------------------------------------------------------------------
                myConnection.Open();
                if (myCommand.ExecuteNonQuery() > 0)
                {
                    result = true;
                    //Get ID value from database and set it in object
				RoundsLogObj.RoundID = (int)myCommand.Parameters["@RoundID"].Value;

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
        /// <param name="RoundsLogObj">Model object.</param>
        /// <returns>The result of update query.</returns>
        //--------------------------------------------------------------------
        public bool Update(RoundsLogModel RoundsLogObj)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[RoundsLog_Update]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                
				myCommand.Parameters.Add("@RoundID", SqlDbType.Int).Value = RoundsLogObj.RoundID;
				myCommand.Parameters.Add("@IndexerID", SqlDbType.Int).Value = RoundsLogObj.IndexerID;
				myCommand.Parameters.Add("@JournalCode", SqlDbType.VarChar).Value = RoundsLogObj.JournalCode;
				myCommand.Parameters.Add("@RoundDate", SqlDbType.Date).Value = RoundsLogObj.RoundDate;
				myCommand.Parameters.Add("@StatusID", SqlDbType.Int).Value = RoundsLogObj.StatusID;
				myCommand.Parameters.Add("@NextEvaRound", SqlDbType.NVarChar).Value = RoundsLogObj.NextEvaRound;
				myCommand.Parameters.Add("@Notes", SqlDbType.NVarChar).Value = RoundsLogObj.Notes;

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
        /// <param name="RoundID">The model id.</param>
        /// <returns>The result of delete query.</returns>
        //--------------------------------------------------------------------
        public bool Delete(int RoundID)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[RoundsLog_Delete]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                if(RoundID>0)myCommand.Parameters.Add("@RoundID", SqlDbType.Int).Value = RoundID;

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
                SqlCommand myCommand = new SqlCommand("[dbo].[RoundsLog_Delete]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                myCommand.Parameters.Add("@RoundID", SqlDbType.Int);

                //----------------------------------------------------------------------------------------------
                // Execute the command
                //----------------------------------------------------------------------------------------------
                myConnection.Open();
                foreach (int id in ids)
                {
                    myCommand.Parameters["@RoundID"].Value = id;
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
        public List<RoundsLogModel> GetPageByPage(int pageIndex, int pageSize, string sortname, string sortorder, string qtype, string query, out int totalRecords)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                //----------------------------------------------------------------------------------------------
                List<RoundsLogModel> RoundsLogList = new List<RoundsLogModel>();
                RoundsLogModel RoundsLogObj;
                //----------------------------------------------------------------------------------------------
                SqlCommand myCommand = new SqlCommand("[dbo].[RoundsLog_GetPageByPage]", myConnection);
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
                    RoundsLogObj = PopulateModelFromIDataReader(dr);
                    RoundsLogList.Add(RoundsLogObj);
                }
                dr.Close();
                totalRecords = (int)myCommand.Parameters["@TotalRecords"].Value;
                myConnection.Close();
                //----------------------------------------------------------------------------------------------
                return RoundsLogList;
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
        /// <param name="RoundID">The RoundsLogObj id.</param>
        /// <returns>Model object.</returns>
        //--------------------------------------------------------------------
        public List<RoundsLogModel> Get(int RoundID)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                //----------------------------------------------------------------------------------------------
                List<RoundsLogModel> RoundsLogList = new List<RoundsLogModel>();
                RoundsLogModel RoundsLogObj;
                //----------------------------------------------------------------------------------------------
                SqlCommand myCommand = new SqlCommand("[dbo].[RoundsLog_Get]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                if(RoundID>0)myCommand.Parameters.Add("@RoundID", SqlDbType.Int).Value = RoundID;

                 // Execute the command
                //----------------------------------------------------------------------------------------------
                SqlDataReader dr;
                myConnection.Open();
                dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    RoundsLogObj = PopulateModelFromIDataReader(dr);
                    RoundsLogList.Add(RoundsLogObj);
                }
                dr.Close();
                myConnection.Close();
                //----------------------------------------------------------------------------------------------
                return RoundsLogList;
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
        private RoundsLogModel PopulateModelFromIDataReader(IDataReader reader)
        {
            //Create a new object
            RoundsLogModel RoundsLogObj = new RoundsLogModel();
            //Fill the object with data

			//------------------------------------------------
			//[RoundID]
			//------------------------------------------------
			if (reader["RoundID"] != DBNull.Value)
			    RoundsLogObj.RoundID = (int)reader["RoundID"];
			//------------------------------------------------
			//------------------------------------------------
			//[IndexerID]
			//------------------------------------------------
			if (reader["IndexerID"] != DBNull.Value)
			    RoundsLogObj.IndexerID = (int)reader["IndexerID"];
			//------------------------------------------------
			//------------------------------------------------
			//[JournalCode]
			//------------------------------------------------
			if (reader["JournalCode"] != DBNull.Value)
			    RoundsLogObj.JournalCode = (string)reader["JournalCode"];
			//------------------------------------------------
			//------------------------------------------------
			//[RoundDate]
			//------------------------------------------------
			if (reader["RoundDate"] != DBNull.Value)
			    RoundsLogObj.RoundDate = (DateTime)reader["RoundDate"];
			//------------------------------------------------
			//------------------------------------------------
			//[StatusID]
			//------------------------------------------------
			if (reader["StatusID"] != DBNull.Value)
			    RoundsLogObj.StatusID = (int)reader["StatusID"];
			//------------------------------------------------
			//------------------------------------------------
			//[NextEvaRound]
			//------------------------------------------------
			if (reader["NextEvaRound"] != DBNull.Value)
			    RoundsLogObj.NextEvaRound = (string)reader["NextEvaRound"];
			//------------------------------------------------
			//------------------------------------------------
			//[Notes]
			//------------------------------------------------
			if (reader["Notes"] != DBNull.Value)
			    RoundsLogObj.Notes = (string)reader["Notes"];
			//------------------------------------------------
            //Return the populated object
            return RoundsLogObj;
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
    }
}