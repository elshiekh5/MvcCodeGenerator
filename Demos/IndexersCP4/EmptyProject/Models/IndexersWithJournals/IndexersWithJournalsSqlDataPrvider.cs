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
    /// News SQL data provider which represents the data access layer of IndexersWithJournals.
    /// </summary>
    public class IndexersWithJournalsSqlDataPrvider
    {

        #region --------------Instance--------------
        //------------------------------------------------------------------------------------------------------
        private static IndexersWithJournalsSqlDataPrvider _Instance;
        //--------------------------------------------------------------------
        /// <summary>
        /// Gets instance of IndexersWithJournalsSqlDataPrvider calss.
        /// <example>IndexersWithJournalsSqlDataPrvider dp=IndexersWithJournalsSqlDataPrvider.Instance.</example>
        /// </summary>
        //--------------------------------------------------------------------
        public static IndexersWithJournalsSqlDataPrvider Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new IndexersWithJournalsSqlDataPrvider();
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
        /// <param name="IndexersWithJournalsObj">Model object.</param>
        /// <returns>The result of create query.</returns>
        //--------------------------------------------------------------------
        public bool Create(IndexersWithJournalsModel IndexersWithJournalsObj)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[IndexersWithJournals_Create]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
				myCommand.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;
				myCommand.Parameters.Add("@IndexerID", SqlDbType.Int).Value = IndexersWithJournalsObj.IndexerID;
				myCommand.Parameters.Add("@JournalCode", SqlDbType.VarChar).Value = IndexersWithJournalsObj.JournalCode;
				myCommand.Parameters.Add("@SubmissionDate", SqlDbType.Date).Value = IndexersWithJournalsObj.SubmissionDate;
				myCommand.Parameters.Add("@AcceptanceDate", SqlDbType.Date).Value = IndexersWithJournalsObj.AcceptanceDate;
				myCommand.Parameters.Add("@RejectionDate", SqlDbType.Date).Value = IndexersWithJournalsObj.RejectionDate;
				myCommand.Parameters.Add("@NextEvaRound", SqlDbType.NVarChar).Value = IndexersWithJournalsObj.NextEvaRound;
				myCommand.Parameters.Add("@NoofEvaRound", SqlDbType.Int).Value = IndexersWithJournalsObj.NoofEvaRound;
				myCommand.Parameters.Add("@Notes", SqlDbType.NVarChar).Value = IndexersWithJournalsObj.Notes;
				myCommand.Parameters.Add("@StatusID", SqlDbType.Int).Value = IndexersWithJournalsObj.StatusID;

                //----------------------------------------------------------------------------------------------
                // Execute the command
                //----------------------------------------------------------------------------------------------
                myConnection.Open();
                if (myCommand.ExecuteNonQuery() > 0)
                {
                    result = true;
                    //Get ID value from database and set it in object
				IndexersWithJournalsObj.ID = (int)myCommand.Parameters["@ID"].Value;

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
        /// <param name="IndexersWithJournalsObj">Model object.</param>
        /// <returns>The result of update query.</returns>
        //--------------------------------------------------------------------
        public bool Update(IndexersWithJournalsModel IndexersWithJournalsObj)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[IndexersWithJournals_Update]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                
				myCommand.Parameters.Add("@ID", SqlDbType.Int).Value = IndexersWithJournalsObj.ID;
				myCommand.Parameters.Add("@IndexerID", SqlDbType.Int).Value = IndexersWithJournalsObj.IndexerID;
				myCommand.Parameters.Add("@JournalCode", SqlDbType.VarChar).Value = IndexersWithJournalsObj.JournalCode;
				myCommand.Parameters.Add("@SubmissionDate", SqlDbType.Date).Value = IndexersWithJournalsObj.SubmissionDate;
				myCommand.Parameters.Add("@AcceptanceDate", SqlDbType.Date).Value = IndexersWithJournalsObj.AcceptanceDate;
				myCommand.Parameters.Add("@RejectionDate", SqlDbType.Date).Value = IndexersWithJournalsObj.RejectionDate;
				myCommand.Parameters.Add("@NextEvaRound", SqlDbType.NVarChar).Value = IndexersWithJournalsObj.NextEvaRound;
				myCommand.Parameters.Add("@NoofEvaRound", SqlDbType.Int).Value = IndexersWithJournalsObj.NoofEvaRound;
				myCommand.Parameters.Add("@Notes", SqlDbType.NVarChar).Value = IndexersWithJournalsObj.Notes;
				myCommand.Parameters.Add("@StatusID", SqlDbType.Int).Value = IndexersWithJournalsObj.StatusID;

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
        /// <param name="ID">The model id.</param>
        /// <returns>The result of delete query.</returns>
        //--------------------------------------------------------------------
        public bool Delete(int ID)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[IndexersWithJournals_Delete]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                if(ID>0)myCommand.Parameters.Add("@ID", SqlDbType.Int).Value = ID;

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
                SqlCommand myCommand = new SqlCommand("[dbo].[IndexersWithJournals_Delete]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                myCommand.Parameters.Add("@ID", SqlDbType.Int);

                //----------------------------------------------------------------------------------------------
                // Execute the command
                //----------------------------------------------------------------------------------------------
                myConnection.Open();
                foreach (int id in ids)
                {
                    myCommand.Parameters["@ID"].Value = id;
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
        public List<IndexersWithJournalsModel> GetPageByPage(int pageIndex, int pageSize, string sortname, string sortorder, string qtype, string query, out int totalRecords)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                //----------------------------------------------------------------------------------------------
                List<IndexersWithJournalsModel> IndexersWithJournalsList = new List<IndexersWithJournalsModel>();
                IndexersWithJournalsModel IndexersWithJournalsObj;
                //----------------------------------------------------------------------------------------------
                SqlCommand myCommand = new SqlCommand("[dbo].[IndexersWithJournals_GetPageByPage]", myConnection);
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
                    IndexersWithJournalsObj = PopulateModelFromIDataReader(dr);
                    IndexersWithJournalsList.Add(IndexersWithJournalsObj);
                }
                dr.Close();
                totalRecords = (int)myCommand.Parameters["@TotalRecords"].Value;
                myConnection.Close();
                //----------------------------------------------------------------------------------------------
                return IndexersWithJournalsList;
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
        /// <param name="ID">The IndexersWithJournalsObj id.</param>
        /// <returns>Model object.</returns>
        //--------------------------------------------------------------------
        public List<IndexersWithJournalsModel> Get(int ID)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                //----------------------------------------------------------------------------------------------
                List<IndexersWithJournalsModel> IndexersWithJournalsList = new List<IndexersWithJournalsModel>();
                IndexersWithJournalsModel IndexersWithJournalsObj;
                //----------------------------------------------------------------------------------------------
                SqlCommand myCommand = new SqlCommand("[dbo].[IndexersWithJournals_Get]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                if(ID>0)myCommand.Parameters.Add("@ID", SqlDbType.Int).Value = ID;

                 // Execute the command
                //----------------------------------------------------------------------------------------------
                SqlDataReader dr;
                myConnection.Open();
                dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    IndexersWithJournalsObj = PopulateModelFromIDataReader(dr);
                    IndexersWithJournalsList.Add(IndexersWithJournalsObj);
                }
                dr.Close();
                myConnection.Close();
                //----------------------------------------------------------------------------------------------
                return IndexersWithJournalsList;
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
        private IndexersWithJournalsModel PopulateModelFromIDataReader(IDataReader reader)
        {
            //Create a new object
            IndexersWithJournalsModel IndexersWithJournalsObj = new IndexersWithJournalsModel();
            //Fill the object with data

			//------------------------------------------------
			//[ID]
			//------------------------------------------------
			if (reader["ID"] != DBNull.Value)
			    IndexersWithJournalsObj.ID = (int)reader["ID"];
			//------------------------------------------------
			//------------------------------------------------
			//[IndexerID]
			//------------------------------------------------
			if (reader["IndexerID"] != DBNull.Value)
			    IndexersWithJournalsObj.IndexerID = (int)reader["IndexerID"];
			//------------------------------------------------
			//------------------------------------------------
			//[JournalCode]
			//------------------------------------------------
			if (reader["JournalCode"] != DBNull.Value)
			    IndexersWithJournalsObj.JournalCode = (string)reader["JournalCode"];
			//------------------------------------------------
			//------------------------------------------------
			//[SubmissionDate]
			//------------------------------------------------
			if (reader["SubmissionDate"] != DBNull.Value)
			    IndexersWithJournalsObj.SubmissionDate = (DateTime)reader["SubmissionDate"];
			//------------------------------------------------
			//------------------------------------------------
			//[AcceptanceDate]
			//------------------------------------------------
			if (reader["AcceptanceDate"] != DBNull.Value)
			    IndexersWithJournalsObj.AcceptanceDate = (DateTime)reader["AcceptanceDate"];
			//------------------------------------------------
			//------------------------------------------------
			//[RejectionDate]
			//------------------------------------------------
			if (reader["RejectionDate"] != DBNull.Value)
			    IndexersWithJournalsObj.RejectionDate = (DateTime)reader["RejectionDate"];
			//------------------------------------------------
			//------------------------------------------------
			//[NextEvaRound]
			//------------------------------------------------
			if (reader["NextEvaRound"] != DBNull.Value)
			    IndexersWithJournalsObj.NextEvaRound = (string)reader["NextEvaRound"];
			//------------------------------------------------
			//------------------------------------------------
			//[NoofEvaRound]
			//------------------------------------------------
			if (reader["NoofEvaRound"] != DBNull.Value)
			    IndexersWithJournalsObj.NoofEvaRound = (int)reader["NoofEvaRound"];
			//------------------------------------------------
			//------------------------------------------------
			//[Notes]
			//------------------------------------------------
			if (reader["Notes"] != DBNull.Value)
			    IndexersWithJournalsObj.Notes = (string)reader["Notes"];
			//------------------------------------------------
			//------------------------------------------------
			//[StatusID]
			//------------------------------------------------
			if (reader["StatusID"] != DBNull.Value)
			    IndexersWithJournalsObj.StatusID = (int)reader["StatusID"];
			//------------------------------------------------
            //Return the populated object
            return IndexersWithJournalsObj;
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
    }
}