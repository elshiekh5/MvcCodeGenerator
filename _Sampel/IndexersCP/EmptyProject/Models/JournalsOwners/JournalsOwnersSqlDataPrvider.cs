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
    /// News SQL data provider which represents the data access layer of JournalsOwners.
    /// </summary>
    public class JournalsOwnersSqlDataPrvider
    {

        #region --------------Instance--------------
        //------------------------------------------------------------------------------------------------------
        private static JournalsOwnersSqlDataPrvider _Instance;
        //--------------------------------------------------------------------
        /// <summary>
        /// Gets instance of JournalsOwnersSqlDataPrvider calss.
        /// <example>JournalsOwnersSqlDataPrvider dp=JournalsOwnersSqlDataPrvider.Instance.</example>
        /// </summary>
        //--------------------------------------------------------------------
        public static JournalsOwnersSqlDataPrvider Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new JournalsOwnersSqlDataPrvider();
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
        /// <param name="JournalsOwnersObj">Model object.</param>
        /// <returns>The result of create query.</returns>
        //--------------------------------------------------------------------
        public bool Create(JournalsOwnersModel JournalsOwnersObj)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[JournalsOwners_Create]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
				myCommand.Parameters.Add("@JournalOwner", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
				myCommand.Parameters.Add("@OwnerName", SqlDbType.VarChar, 64).Value = JournalsOwnersObj.OwnerName;

                //----------------------------------------------------------------------------------------------
                // Execute the command
                //----------------------------------------------------------------------------------------------
                myConnection.Open();
                if (myCommand.ExecuteNonQuery() > 0)
                {
                    result = true;
                    //Get ID value from database and set it in object
				JournalsOwnersObj.JournalOwner = (int)myCommand.Parameters["@JournalOwner"].Value;

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
        /// <param name="JournalsOwnersObj">Model object.</param>
        /// <returns>The result of update query.</returns>
        //--------------------------------------------------------------------
        public bool Update(JournalsOwnersModel JournalsOwnersObj)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[JournalsOwners_Update]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                
				myCommand.Parameters.Add("@JournalOwner", SqlDbType.Int, 4).Value = JournalsOwnersObj.JournalOwner;
				myCommand.Parameters.Add("@OwnerName", SqlDbType.VarChar, 64).Value = JournalsOwnersObj.OwnerName;

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
        /// <param name="JournalOwner">The model id.</param>
        /// <returns>The result of delete query.</returns>
        //--------------------------------------------------------------------
        public bool Delete(int JournalOwner)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[JournalsOwners_Delete]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                if(JournalOwner>0)myCommand.Parameters.Add("@JournalOwner", SqlDbType.Int, 4).Value = JournalOwner;

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
                SqlCommand myCommand = new SqlCommand("[dbo].[JournalsOwners_Delete]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                myCommand.Parameters.Add("@JournalOwner", SqlDbType.Int, 4);

                //----------------------------------------------------------------------------------------------
                // Execute the command
                //----------------------------------------------------------------------------------------------
                myConnection.Open();
                foreach (int id in ids)
                {
                    myCommand.Parameters["@JournalOwner"].Value = id;
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
        public List<JournalsOwnersModel> GetPageByPage(int pageIndex, int pageSize, string sortname, string sortorder, string qtype, string query, out int totalRecords)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                //----------------------------------------------------------------------------------------------
                List<JournalsOwnersModel> JournalsOwnersList = new List<JournalsOwnersModel>();
                JournalsOwnersModel JournalsOwnersObj;
                //----------------------------------------------------------------------------------------------
                SqlCommand myCommand = new SqlCommand("[dbo].[JournalsOwners_GetPageByPage]", myConnection);
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
                    JournalsOwnersObj = PopulateModelFromIDataReader(dr);
                    JournalsOwnersList.Add(JournalsOwnersObj);
                }
                dr.Close();
                totalRecords = (int)myCommand.Parameters["@TotalRecords"].Value;
                myConnection.Close();
                //----------------------------------------------------------------------------------------------
                return JournalsOwnersList;
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
        /// <param name="JournalOwner">The JournalsOwnersObj id.</param>
        /// <returns>Model object.</returns>
        //--------------------------------------------------------------------
        public List<JournalsOwnersModel> Get(int JournalOwner)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                //----------------------------------------------------------------------------------------------
                List<JournalsOwnersModel> JournalsOwnersList = new List<JournalsOwnersModel>();
                JournalsOwnersModel JournalsOwnersObj;
                //----------------------------------------------------------------------------------------------
                SqlCommand myCommand = new SqlCommand("[dbo].[JournalsOwners_Get]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                if(JournalOwner>0)myCommand.Parameters.Add("@JournalOwner", SqlDbType.Int, 4).Value = JournalOwner;

                 // Execute the command
                //----------------------------------------------------------------------------------------------
                SqlDataReader dr;
                myConnection.Open();
                dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    JournalsOwnersObj = PopulateModelFromIDataReader(dr);
                    JournalsOwnersList.Add(JournalsOwnersObj);
                }
                dr.Close();
                myConnection.Close();
                //----------------------------------------------------------------------------------------------
                return JournalsOwnersList;
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
        private JournalsOwnersModel PopulateModelFromIDataReader(IDataReader reader)
        {
            //Create a new object
            JournalsOwnersModel JournalsOwnersObj = new JournalsOwnersModel();
            //Fill the object with data

			//------------------------------------------------
			//[JournalOwner]
			//------------------------------------------------
			if (reader["JournalOwner"] != DBNull.Value)
			    JournalsOwnersObj.JournalOwner = (int)reader["JournalOwner"];
			//------------------------------------------------
			//------------------------------------------------
			//[OwnerName]
			//------------------------------------------------
			if (reader["OwnerName"] != DBNull.Value)
			    JournalsOwnersObj.OwnerName = (string)reader["OwnerName"];
			//------------------------------------------------
            //Return the populated object
            return JournalsOwnersObj;
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
    }
}