using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;
using News.Models;

namespace News.DAL
{
    /// <summary>
    /// News SQL data provider which represents the data access layer of Items.
    /// </summary>
    public class ItemsSqlDataPrvider
    {

        #region --------------Instance--------------
        //------------------------------------------------------------------------------------------------------
        private static ItemsSqlDataPrvider _Instance;
        //--------------------------------------------------------------------
        /// <summary>
        /// Gets instance of ItemsSqlDataPrvider calss.
        /// <example>ItemsSqlDataPrvider dp=ItemsSqlDataPrvider.Instance.</example>
        /// </summary>
        //--------------------------------------------------------------------
        public static ItemsSqlDataPrvider Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ItemsSqlDataPrvider();
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
        /// <param name="ItemsObj">Model object.</param>
        /// <returns>The result of create query.</returns>
        //--------------------------------------------------------------------
        public bool Create(ItemsModel ItemsObj)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[Items_Create]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                myCommand.Parameters.Add("@ItemID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
				myCommand.Parameters.Add("@CategoryID", SqlDbType.Int, 4).Value = ItemsObj.CategoryID;
				myCommand.Parameters.Add("@PhotoFile", SqlDbType.VarChar, 5).Value = ItemsObj.PhotoFile;
				myCommand.Parameters.Add("@Url", SqlDbType.NVarChar, 128).Value = ItemsObj.Url;
				myCommand.Parameters.Add("@Type", SqlDbType.Int, 4).Value = ItemsObj.Type;
				myCommand.Parameters.Add("@IsMain", SqlDbType.Bit, 1).Value = ItemsObj.IsMain;
				myCommand.Parameters.Add("@YoutubeCode", SqlDbType.VarChar, 16).Value = ItemsObj.YoutubeCode;
				myCommand.Parameters.Add("@Title", SqlDbType.NVarChar, 128).Value = ItemsObj.Title;
				myCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = ItemsObj.Description;
				//myCommand.Parameters.Add("@DateInserted", SqlDbType.DateTime, 8).Value = ItemsObj.DateInserted;

                //----------------------------------------------------------------------------------------------
                // Execute the command
                //----------------------------------------------------------------------------------------------
                myConnection.Open();
                if (myCommand.ExecuteNonQuery() > 0)
                {
                    result = true;
                    //Get ID value from database and set it in object
                    ItemsObj.ItemID = (int)myCommand.Parameters["@ItemID"].Value;
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
        /// <param name="ItemsObj">Model object.</param>
        /// <returns>The result of update query.</returns>
        //--------------------------------------------------------------------
        public bool Update(ItemsModel ItemsObj)
        {

            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[Items_Update]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                myCommand.Parameters.Add("@ItemID", SqlDbType.Int, 4).Value = ItemsObj.ItemID;
				myCommand.Parameters.Add("@CategoryID", SqlDbType.Int, 4).Value = ItemsObj.CategoryID;
				myCommand.Parameters.Add("@PhotoFile", SqlDbType.VarChar, 5).Value = ItemsObj.PhotoFile;
				myCommand.Parameters.Add("@Url", SqlDbType.NVarChar, 128).Value = ItemsObj.Url;
				myCommand.Parameters.Add("@Type", SqlDbType.Int, 4).Value = ItemsObj.Type;
				myCommand.Parameters.Add("@IsMain", SqlDbType.Bit, 1).Value = ItemsObj.IsMain;
				myCommand.Parameters.Add("@YoutubeCode", SqlDbType.VarChar, 16).Value = ItemsObj.YoutubeCode;
				myCommand.Parameters.Add("@Title", SqlDbType.NVarChar, 128).Value = ItemsObj.Title;
				myCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = ItemsObj.Description;
				//myCommand.Parameters.Add("@DateInserted", SqlDbType.DateTime, 8).Value = ItemsObj.DateInserted;

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
        /// <param name="ItemID">The model id.</param>
        /// <returns>The result of delete query.</returns>
        //--------------------------------------------------------------------
        public bool Delete(int ItemID)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[Items_Delete]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                myCommand.Parameters.Add("@ItemID", SqlDbType.Int, 4).Value = ItemID;
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
        #region /*--------- DeleteGroupofobjects ---------*\
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Deletes multible objects .
        /// </summary>
        /// <param name="ids">The array of ids.</param>
        //--------------------------------------------------------------------
        public void DeleteGroupofobjects(int[] ids)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[Items_Delete]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                myCommand.Parameters.Add("@ItemID", SqlDbType.Int, 4);
                //----------------------------------------------------------------------------------------------
                // Execute the command
                //----------------------------------------------------------------------------------------------
                myConnection.Open();
                foreach (int id in ids)
                {
                    myCommand.Parameters["@ItemID"].Value = id;
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
        public List<ItemsModel> GetPageByPage(int pageIndex, int pageSize, string sortname, string sortorder, string qtype, string query, out int totalRecords)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                //----------------------------------------------------------------------------------------------
                List<ItemsModel> ItemsList = new List<ItemsModel>();
                ItemsModel ItemsObj;
                //----------------------------------------------------------------------------------------------
                SqlCommand myCommand = new SqlCommand("[dbo].[Items_GetPageByPage]", myConnection);
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
                    ItemsObj = PopulateModelFromIDataReader(dr);
                    ItemsList.Add(ItemsObj);
                }
                dr.Close();
                totalRecords = (int)myCommand.Parameters["@TotalRecords"].Value;
                myConnection.Close();
                //----------------------------------------------------------------------------------------------
                return ItemsList;
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
        /// <param name="ItemID">The ItemsObj id.</param>
        /// <returns>Model object.</returns>
        //--------------------------------------------------------------------
        public List<ItemsModel> Get(int ItemID)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                //----------------------------------------------------------------------------------------------
                List<ItemsModel> ItemsList = new List<ItemsModel>();
                ItemsModel ItemsObj;
                //----------------------------------------------------------------------------------------------
                SqlCommand myCommand = new SqlCommand("[dbo].[Items_Get]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                if(ItemID>0)
                    myCommand.Parameters.Add("@ItemID", SqlDbType.Int, 4).Value = ItemID;
                 // Execute the command
                //----------------------------------------------------------------------------------------------
                SqlDataReader dr;
                myConnection.Open();
                dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    ItemsObj = PopulateModelFromIDataReader(dr);
                    ItemsList.Add(ItemsObj);
                }
                dr.Close();
                myConnection.Close();
                //----------------------------------------------------------------------------------------------
                return ItemsList;
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
        private ItemsModel PopulateModelFromIDataReader(IDataReader reader)
        {
            //Create a new object
            ItemsModel ItemsObj = new ItemsModel();
            //Fill the object with data
            //------------------------------------------------
            //ItemID
            //------------------------------------------------
            if (reader["ItemID"] != DBNull.Value)
                ItemsObj.ItemID = (int)reader["ItemID"];
            //------------------------------------------------

			//------------------------------------------------
			//[CategoryID]
			//------------------------------------------------
			if (reader["CategoryID"] != DBNull.Value)
			    ItemsObj.CategoryID = (int)reader["CategoryID"];
			//------------------------------------------------
			//------------------------------------------------
			//[PhotoFile]
			//------------------------------------------------
			if (reader["PhotoFile"] != DBNull.Value)
			    ItemsObj.PhotoFile = (string)reader["PhotoFile"];
			//------------------------------------------------
			//------------------------------------------------
			//[Url]
			//------------------------------------------------
			if (reader["Url"] != DBNull.Value)
			    ItemsObj.Url = (string)reader["Url"];
			//------------------------------------------------
			//------------------------------------------------
			//[Type]
			//------------------------------------------------
			if (reader["Type"] != DBNull.Value)
			    ItemsObj.Type = (int)reader["Type"];
			//------------------------------------------------
			//------------------------------------------------
			//[IsMain]
			//------------------------------------------------
			if (reader["IsMain"] != DBNull.Value)
			    ItemsObj.IsMain = (bool)reader["IsMain"];
			//------------------------------------------------
			//------------------------------------------------
			//[YoutubeCode]
			//------------------------------------------------
			if (reader["YoutubeCode"] != DBNull.Value)
			    ItemsObj.YoutubeCode = (string)reader["YoutubeCode"];
			//------------------------------------------------
			//------------------------------------------------
			//[Title]
			//------------------------------------------------
			if (reader["Title"] != DBNull.Value)
			    ItemsObj.Title = (string)reader["Title"];
			//------------------------------------------------
			//------------------------------------------------
			//[Description]
			//------------------------------------------------
			if (reader["Description"] != DBNull.Value)
			    ItemsObj.Description = (string)reader["Description"];
			//------------------------------------------------
			//------------------------------------------------
			//[DateInserted]
			//------------------------------------------------
			if (reader["DateInserted"] != DBNull.Value)
			    ItemsObj.DateInserted = (DateTime)reader["DateInserted"];
			//------------------------------------------------
            //Return the populated object
            return ItemsObj;
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
    }
}