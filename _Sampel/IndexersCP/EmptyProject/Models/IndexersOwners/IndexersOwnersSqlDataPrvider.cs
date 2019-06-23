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
    /// News SQL data provider which represents the data access layer of IndexersOwners.
    /// </summary>
    public class IndexersOwnersSqlDataPrvider
    {

        #region --------------Instance--------------
        //------------------------------------------------------------------------------------------------------
        private static IndexersOwnersSqlDataPrvider _Instance;
        //--------------------------------------------------------------------
        /// <summary>
        /// Gets instance of IndexersOwnersSqlDataPrvider calss.
        /// <example>IndexersOwnersSqlDataPrvider dp=IndexersOwnersSqlDataPrvider.Instance.</example>
        /// </summary>
        //--------------------------------------------------------------------
        public static IndexersOwnersSqlDataPrvider Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new IndexersOwnersSqlDataPrvider();
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
        /// <param name="IndexersOwnersObj">Model object.</param>
        /// <returns>The result of create query.</returns>
        //--------------------------------------------------------------------
        public bool Create(IndexersOwnersModel IndexersOwnersObj)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[IndexersOwners_Create]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
				myCommand.Parameters.Add("@OwnerID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
				myCommand.Parameters.Add("@Name", SqlDbType.VarChar, 64).Value = IndexersOwnersObj.Name;

                //----------------------------------------------------------------------------------------------
                // Execute the command
                //----------------------------------------------------------------------------------------------
                myConnection.Open();
                if (myCommand.ExecuteNonQuery() > 0)
                {
                    result = true;
                    //Get ID value from database and set it in object
				IndexersOwnersObj.OwnerID = (int)myCommand.Parameters["@OwnerID"].Value;

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
        /// <param name="IndexersOwnersObj">Model object.</param>
        /// <returns>The result of update query.</returns>
        //--------------------------------------------------------------------
        public bool Update(IndexersOwnersModel IndexersOwnersObj)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[IndexersOwners_Update]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                
				myCommand.Parameters.Add("@OwnerID", SqlDbType.Int, 4).Value = IndexersOwnersObj.OwnerID;
				myCommand.Parameters.Add("@Name", SqlDbType.VarChar, 64).Value = IndexersOwnersObj.Name;

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
        /// <param name="OwnerID">The model id.</param>
        /// <returns>The result of delete query.</returns>
        //--------------------------------------------------------------------
        public bool Delete(int OwnerID)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[IndexersOwners_Delete]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                if(OwnerID>0)myCommand.Parameters.Add("@OwnerID", SqlDbType.Int, 4).Value = OwnerID;

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
                SqlCommand myCommand = new SqlCommand("[dbo].[IndexersOwners_Delete]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                myCommand.Parameters.Add("@OwnerID", SqlDbType.Int, 4);

                //----------------------------------------------------------------------------------------------
                // Execute the command
                //----------------------------------------------------------------------------------------------
                myConnection.Open();
                foreach (int id in ids)
                {
                    myCommand.Parameters["@OwnerID"].Value = id;
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
        public List<IndexersOwnersModel> GetPageByPage(int pageIndex, int pageSize, string sortname, string sortorder, string qtype, string query, out int totalRecords)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                //----------------------------------------------------------------------------------------------
                List<IndexersOwnersModel> IndexersOwnersList = new List<IndexersOwnersModel>();
                IndexersOwnersModel IndexersOwnersObj;
                //----------------------------------------------------------------------------------------------
                SqlCommand myCommand = new SqlCommand("[dbo].[IndexersOwners_GetPageByPage]", myConnection);
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
                    IndexersOwnersObj = PopulateModelFromIDataReader(dr);
                    IndexersOwnersList.Add(IndexersOwnersObj);
                }
                dr.Close();
                totalRecords = (int)myCommand.Parameters["@TotalRecords"].Value;
                myConnection.Close();
                //----------------------------------------------------------------------------------------------
                return IndexersOwnersList;
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
        /// <param name="OwnerID">The IndexersOwnersObj id.</param>
        /// <returns>Model object.</returns>
        //--------------------------------------------------------------------
        public List<IndexersOwnersModel> Get(int OwnerID)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                //----------------------------------------------------------------------------------------------
                List<IndexersOwnersModel> IndexersOwnersList = new List<IndexersOwnersModel>();
                IndexersOwnersModel IndexersOwnersObj;
                //----------------------------------------------------------------------------------------------
                SqlCommand myCommand = new SqlCommand("[dbo].[IndexersOwners_Get]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                if(OwnerID>0)myCommand.Parameters.Add("@OwnerID", SqlDbType.Int, 4).Value = OwnerID;

                 // Execute the command
                //----------------------------------------------------------------------------------------------
                SqlDataReader dr;
                myConnection.Open();
                dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    IndexersOwnersObj = PopulateModelFromIDataReader(dr);
                    IndexersOwnersList.Add(IndexersOwnersObj);
                }
                dr.Close();
                myConnection.Close();
                //----------------------------------------------------------------------------------------------
                return IndexersOwnersList;
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
        private IndexersOwnersModel PopulateModelFromIDataReader(IDataReader reader)
        {
            //Create a new object
            IndexersOwnersModel IndexersOwnersObj = new IndexersOwnersModel();
            //Fill the object with data

			//------------------------------------------------
			//[OwnerID]
			//------------------------------------------------
			if (reader["OwnerID"] != DBNull.Value)
			    IndexersOwnersObj.OwnerID = (int)reader["OwnerID"];
			//------------------------------------------------
			//------------------------------------------------
			//[Name]
			//------------------------------------------------
			if (reader["Name"] != DBNull.Value)
			    IndexersOwnersObj.Name = (string)reader["Name"];
			//------------------------------------------------
            //Return the populated object
            return IndexersOwnersObj;
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
    }
}