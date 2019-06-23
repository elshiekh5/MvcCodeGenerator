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
    /// News SQL data provider which represents the data access layer of Journals.
    /// </summary>
    public class JournalsSqlDataPrvider
    {

        #region --------------Instance--------------
        //------------------------------------------------------------------------------------------------------
        private static JournalsSqlDataPrvider _Instance;
        //--------------------------------------------------------------------
        /// <summary>
        /// Gets instance of JournalsSqlDataPrvider calss.
        /// <example>JournalsSqlDataPrvider dp=JournalsSqlDataPrvider.Instance.</example>
        /// </summary>
        //--------------------------------------------------------------------
        public static JournalsSqlDataPrvider Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new JournalsSqlDataPrvider();
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
        /// <param name="JournalsObj">Model object.</param>
        /// <returns>The result of create query.</returns>
        //--------------------------------------------------------------------
        public bool Create(JournalsModel JournalsObj)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[Journals_Create]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
				myCommand.Parameters.Add("@JournalCode", SqlDbType.VarChar).Value = JournalsObj.JournalCode;
				myCommand.Parameters.Add("@FullTitle", SqlDbType.VarChar).Value = JournalsObj.FullTitle;
				myCommand.Parameters.Add("@ShortTitle", SqlDbType.VarChar).Value = JournalsObj.ShortTitle;
				myCommand.Parameters.Add("@JournalSubCode", SqlDbType.VarChar).Value = JournalsObj.JournalSubCode;
				myCommand.Parameters.Add("@JournalOwner", SqlDbType.Int).Value = JournalsObj.JournalOwner;
				myCommand.Parameters.Add("@Note", SqlDbType.VarChar).Value = JournalsObj.Note;

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
        /// <param name="JournalsObj">Model object.</param>
        /// <returns>The result of update query.</returns>
        //--------------------------------------------------------------------
        public bool Update(JournalsModel JournalsObj)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[Journals_Update]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                
				myCommand.Parameters.Add("@JournalCode", SqlDbType.VarChar).Value = JournalsObj.JournalCode;
				myCommand.Parameters.Add("@FullTitle", SqlDbType.VarChar).Value = JournalsObj.FullTitle;
				myCommand.Parameters.Add("@ShortTitle", SqlDbType.VarChar).Value = JournalsObj.ShortTitle;
				myCommand.Parameters.Add("@JournalSubCode", SqlDbType.VarChar).Value = JournalsObj.JournalSubCode;
				myCommand.Parameters.Add("@JournalOwner", SqlDbType.Int).Value = JournalsObj.JournalOwner;
				myCommand.Parameters.Add("@Note", SqlDbType.VarChar).Value = JournalsObj.Note;

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
        /// <param name="JournalCode">The model id.</param>
        /// <returns>The result of delete query.</returns>
        //--------------------------------------------------------------------
        public bool Delete(string JournalCode)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[Journals_Delete]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                if(!string.IsNullOrEmpty(JournalCode))myCommand.Parameters.Add("@JournalCode", SqlDbType.VarChar).Value = JournalCode;

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
        public void DeleteGroupofObjects(string[] ids)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[Journals_Delete]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                myCommand.Parameters.Add("@JournalCode", SqlDbType.VarChar);

                //----------------------------------------------------------------------------------------------
                // Execute the command
                //----------------------------------------------------------------------------------------------
                myConnection.Open();
                foreach (string id in ids)
                {
                    myCommand.Parameters["@JournalCode"].Value = id;
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
        public List<JournalsModel> GetPageByPage(int pageIndex, int pageSize, string sortname, string sortorder, string qtype, string query, out int totalRecords)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                //----------------------------------------------------------------------------------------------
                List<JournalsModel> JournalsList = new List<JournalsModel>();
                JournalsModel JournalsObj;
                //----------------------------------------------------------------------------------------------
                SqlCommand myCommand = new SqlCommand("[dbo].[Journals_GetPageByPage]", myConnection);
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
                    JournalsObj = PopulateModelFromIDataReader(dr);
                    JournalsList.Add(JournalsObj);
                }
                dr.Close();
                totalRecords = (int)myCommand.Parameters["@TotalRecords"].Value;
                myConnection.Close();
                //----------------------------------------------------------------------------------------------
                return JournalsList;
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
        /// <param name="JournalCode">The JournalsObj id.</param>
        /// <returns>Model object.</returns>
        //--------------------------------------------------------------------
        public List<JournalsModel> Get(string JournalCode)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                //----------------------------------------------------------------------------------------------
                List<JournalsModel> JournalsList = new List<JournalsModel>();
                JournalsModel JournalsObj;
                //----------------------------------------------------------------------------------------------
                SqlCommand myCommand = new SqlCommand("[dbo].[Journals_Get]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                if(!string.IsNullOrEmpty(JournalCode))myCommand.Parameters.Add("@JournalCode", SqlDbType.VarChar).Value = JournalCode;

                 // Execute the command
                //----------------------------------------------------------------------------------------------
                SqlDataReader dr;
                myConnection.Open();
                dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    JournalsObj = PopulateModelFromIDataReader(dr);
                    JournalsList.Add(JournalsObj);
                }
                dr.Close();
                myConnection.Close();
                //----------------------------------------------------------------------------------------------
                return JournalsList;
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
        private JournalsModel PopulateModelFromIDataReader(IDataReader reader)
        {
            //Create a new object
            JournalsModel JournalsObj = new JournalsModel();
            //Fill the object with data

			//------------------------------------------------
			//[JournalCode]
			//------------------------------------------------
			if (reader["JournalCode"] != DBNull.Value)
			    JournalsObj.JournalCode = (string)reader["JournalCode"];
			//------------------------------------------------
			//------------------------------------------------
			//[FullTitle]
			//------------------------------------------------
			if (reader["FullTitle"] != DBNull.Value)
			    JournalsObj.FullTitle = (string)reader["FullTitle"];
			//------------------------------------------------
			//------------------------------------------------
			//[ShortTitle]
			//------------------------------------------------
			if (reader["ShortTitle"] != DBNull.Value)
			    JournalsObj.ShortTitle = (string)reader["ShortTitle"];
			//------------------------------------------------
			//------------------------------------------------
			//[JournalSubCode]
			//------------------------------------------------
			if (reader["JournalSubCode"] != DBNull.Value)
			    JournalsObj.JournalSubCode = (string)reader["JournalSubCode"];
			//------------------------------------------------
			//------------------------------------------------
			//[JournalOwner]
			//------------------------------------------------
			if (reader["JournalOwner"] != DBNull.Value)
			    JournalsObj.JournalOwner = (int)reader["JournalOwner"];
			//------------------------------------------------
			//------------------------------------------------
			//[Note]
			//------------------------------------------------
			if (reader["Note"] != DBNull.Value)
			    JournalsObj.Note = (string)reader["Note"];
			//------------------------------------------------
            //Return the populated object
            return JournalsObj;
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
    }
}