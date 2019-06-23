using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;
using Test.Models;

namespace Test
{
    /// <summary>
    /// News SQL data provider which represents the data access layer of News.
    /// </summary>
    public class NewsSqlDataPrvider
    {

        #region --------------Instance--------------
        //------------------------------------------------------------------------------------------------------
        private static NewsSqlDataPrvider _Instance;
        //--------------------------------------------------------------------
        /// <summary>
        /// Gets instance of NewsSqlDataPrvider calss.
        /// <example>NewsSqlDataPrvider dp=NewsSqlDataPrvider.Instance.</example>
        /// </summary>
        //--------------------------------------------------------------------
        public static NewsSqlDataPrvider Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new NewsSqlDataPrvider();
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
        /// <param name="NewsObj">Model object.</param>
        /// <returns>The result of create query.</returns>
        //--------------------------------------------------------------------
        public bool Create(NewsModel NewsObj)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[News_Create]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
				myCommand.Parameters.Add("@NewsID", SqlDbType.Int).Direction = ParameterDirection.Output;
				myCommand.Parameters.Add("@LangID", SqlDbType.Int).Value = NewsObj.LangID;
				myCommand.Parameters.Add("@Type", SqlDbType.Int).Value = NewsObj.Type;
				myCommand.Parameters.Add("@Title", SqlDbType.NVarChar).Value = NewsObj.Title;
				myCommand.Parameters.Add("@Details", SqlDbType.NVarChar).Value = NewsObj.Details;
				myCommand.Parameters.Add("@PhotoFile", SqlDbType.VarChar).Value = NewsObj.PhotoFile;
				myCommand.Parameters.Add("@PhotoName", SqlDbType.NVarChar).Value = NewsObj.PhotoName;
				myCommand.Parameters.Add("@AttachFile", SqlDbType.VarChar).Value = NewsObj.AttachFile;
				myCommand.Parameters.Add("@AttachName", SqlDbType.NVarChar).Value = NewsObj.AttachName;
				myCommand.Parameters.Add("@EndDate", SqlDbType.DateTime2).Value = NewsObj.EndDate;
				myCommand.Parameters.Add("@InsertDate", SqlDbType.DateTime2).Value = NewsObj.InsertDate;
				myCommand.Parameters.Add("@LastModfiedDate", SqlDbType.DateTime2).Value = NewsObj.LastModfiedDate;

                //----------------------------------------------------------------------------------------------
                // Execute the command
                //----------------------------------------------------------------------------------------------
                myConnection.Open();
                if (myCommand.ExecuteNonQuery() > 0)
                {
                    result = true;
                    //Get ID value from database and set it in object
				NewsObj.NewsID = (int)myCommand.Parameters["@NewsID"].Value;

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
        /// <param name="NewsObj">Model object.</param>
        /// <returns>The result of update query.</returns>
        //--------------------------------------------------------------------
        public bool Update(NewsModel NewsObj)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[News_Update]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                
				myCommand.Parameters.Add("@NewsID", SqlDbType.Int).Value = NewsObj.NewsID;
				myCommand.Parameters.Add("@LangID", SqlDbType.Int).Value = NewsObj.LangID;
				myCommand.Parameters.Add("@Type", SqlDbType.Int).Value = NewsObj.Type;
				myCommand.Parameters.Add("@Title", SqlDbType.NVarChar).Value = NewsObj.Title;
				myCommand.Parameters.Add("@Details", SqlDbType.NVarChar).Value = NewsObj.Details;
				myCommand.Parameters.Add("@PhotoFile", SqlDbType.VarChar).Value = NewsObj.PhotoFile;
				myCommand.Parameters.Add("@PhotoName", SqlDbType.NVarChar).Value = NewsObj.PhotoName;
				myCommand.Parameters.Add("@AttachFile", SqlDbType.VarChar).Value = NewsObj.AttachFile;
				myCommand.Parameters.Add("@AttachName", SqlDbType.NVarChar).Value = NewsObj.AttachName;
				myCommand.Parameters.Add("@EndDate", SqlDbType.DateTime2).Value = NewsObj.EndDate;
				myCommand.Parameters.Add("@InsertDate", SqlDbType.DateTime2).Value = NewsObj.InsertDate;
				myCommand.Parameters.Add("@LastModfiedDate", SqlDbType.DateTime2).Value = NewsObj.LastModfiedDate;

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
        /// <param name="NewsID">The model id.</param>
        /// <returns>The result of delete query.</returns>
        //--------------------------------------------------------------------
        public bool Delete(int NewsID)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[News_Delete]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                if(NewsID>0)myCommand.Parameters.Add("@NewsID", SqlDbType.Int).Value = NewsID;

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
                SqlCommand myCommand = new SqlCommand("[dbo].[News_Delete]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                myCommand.Parameters.Add("@NewsID", SqlDbType.Int);

                //----------------------------------------------------------------------------------------------
                // Execute the command
                //----------------------------------------------------------------------------------------------
                myConnection.Open();
                foreach (int id in ids)
                {
                    myCommand.Parameters["@NewsID"].Value = id;
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
        public List<NewsModel> GetPageByPage(int pageIndex, int pageSize, string sortname, string sortorder, string qtype, string query, out int totalRecords)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                //----------------------------------------------------------------------------------------------
                List<NewsModel> NewsList = new List<NewsModel>();
                NewsModel NewsObj;
                //----------------------------------------------------------------------------------------------
                SqlCommand myCommand = new SqlCommand("[dbo].[News_GetPageByPage]", myConnection);
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
                    NewsObj = PopulateModelFromIDataReader(dr);
                    NewsList.Add(NewsObj);
                }
                dr.Close();
                totalRecords = (int)myCommand.Parameters["@TotalRecords"].Value;
                myConnection.Close();
                //----------------------------------------------------------------------------------------------
                return NewsList;
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
        /// <param name="NewsID">The NewsObj id.</param>
        /// <returns>Model object.</returns>
        //--------------------------------------------------------------------
        public List<NewsModel> Get(int NewsID)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                //----------------------------------------------------------------------------------------------
                List<NewsModel> NewsList = new List<NewsModel>();
                NewsModel NewsObj;
                //----------------------------------------------------------------------------------------------
                SqlCommand myCommand = new SqlCommand("[dbo].[News_Get]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                if(NewsID>0)myCommand.Parameters.Add("@NewsID", SqlDbType.Int).Value = NewsID;

                 // Execute the command
                //----------------------------------------------------------------------------------------------
                SqlDataReader dr;
                myConnection.Open();
                dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    NewsObj = PopulateModelFromIDataReader(dr);
                    NewsList.Add(NewsObj);
                }
                dr.Close();
                myConnection.Close();
                //----------------------------------------------------------------------------------------------
                return NewsList;
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
        private NewsModel PopulateModelFromIDataReader(IDataReader reader)
        {
            //Create a new object
            NewsModel NewsObj = new NewsModel();
            //Fill the object with data

			//------------------------------------------------
			//[NewsID]
			//------------------------------------------------
			if (reader["NewsID"] != DBNull.Value)
			    NewsObj.NewsID = (int)reader["NewsID"];
			//------------------------------------------------
			//------------------------------------------------
			//[LangID]
			//------------------------------------------------
			if (reader["LangID"] != DBNull.Value)
			    NewsObj.LangID = (int)reader["LangID"];
			//------------------------------------------------
			//------------------------------------------------
			//[Type]
			//------------------------------------------------
			if (reader["Type"] != DBNull.Value)
			    NewsObj.Type = (int)reader["Type"];
			//------------------------------------------------
			//------------------------------------------------
			//[Title]
			//------------------------------------------------
			if (reader["Title"] != DBNull.Value)
			    NewsObj.Title = (string)reader["Title"];
			//------------------------------------------------
			//------------------------------------------------
			//[Details]
			//------------------------------------------------
			if (reader["Details"] != DBNull.Value)
			    NewsObj.Details = (string)reader["Details"];
			//------------------------------------------------
			//------------------------------------------------
			//[PhotoFile]
			//------------------------------------------------
			if (reader["PhotoFile"] != DBNull.Value)
			    NewsObj.PhotoFile = (string)reader["PhotoFile"];
			//------------------------------------------------
			//------------------------------------------------
			//[PhotoName]
			//------------------------------------------------
			if (reader["PhotoName"] != DBNull.Value)
			    NewsObj.PhotoName = (string)reader["PhotoName"];
			//------------------------------------------------
			//------------------------------------------------
			//[AttachFile]
			//------------------------------------------------
			if (reader["AttachFile"] != DBNull.Value)
			    NewsObj.AttachFile = (string)reader["AttachFile"];
			//------------------------------------------------
			//------------------------------------------------
			//[AttachName]
			//------------------------------------------------
			if (reader["AttachName"] != DBNull.Value)
			    NewsObj.AttachName = (string)reader["AttachName"];
			//------------------------------------------------
			//------------------------------------------------
			//[EndDate]
			//------------------------------------------------
			if (reader["EndDate"] != DBNull.Value)
			    NewsObj.EndDate = (DateTime)reader["EndDate"];
			//------------------------------------------------
			//------------------------------------------------
			//[InsertDate]
			//------------------------------------------------
			if (reader["InsertDate"] != DBNull.Value)
			    NewsObj.InsertDate = (DateTime)reader["InsertDate"];
			//------------------------------------------------
			//------------------------------------------------
			//[LastModfiedDate]
			//------------------------------------------------
			if (reader["LastModfiedDate"] != DBNull.Value)
			    NewsObj.LastModfiedDate = (DateTime)reader["LastModfiedDate"];
			//------------------------------------------------
            //Return the populated object
            return NewsObj;
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
    }
}