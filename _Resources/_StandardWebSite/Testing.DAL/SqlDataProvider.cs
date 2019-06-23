using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;
using Testing.Models;

namespace Testing.DAL
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
            return new SqlConnection(ConfigurationSettings.AppSettings["Connectionstring"].ToString());
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
                myCommand.Parameters.Add("@ID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
				myCommand.Parameters.Add("@Title", SqlDbType.NChar, 100).Value = NewsObj.Title;
				myCommand.Parameters.Add("@Brief", SqlDbType.NChar, 500).Value = NewsObj.Brief;
				myCommand.Parameters.Add("@Details", SqlDbType.NVarChar, -1).Value = NewsObj.Details;
				//myCommand.Parameters.Add("@Date", SqlDbType.DateTime2, 8).Value = NewsObj.Date;

                //----------------------------------------------------------------------------------------------
                // Execute the command
                //----------------------------------------------------------------------------------------------
                myConnection.Open();
                if (myCommand.ExecuteNonQuery() > 0)
                {
                    result = true;
                    //Get ID value from database and set it in object
                    NewsObj.ID = (int)myCommand.Parameters["@ID"].Value;
                }
                myConnection.Close();
                return result;
                //----------------------------------------------------------------------------------------------
            }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region /*--------- Updat ---------*\
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Converts the object properties to SQL paramters and executes the update procedure.
        /// </summary>
        /// <param name="NewsObj">Model object.</param>
        /// <returns>The result of update query.</returns>
        //--------------------------------------------------------------------
        public bool Updat(NewsModel NewsObj)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[News_Update]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                myCommand.Parameters.Add("@ID", SqlDbType.Int, 4).Value = NewsObj.ID;
				myCommand.Parameters.Add("@Title", SqlDbType.NChar, 100).Value = NewsObj.Title;
				myCommand.Parameters.Add("@Brief", SqlDbType.NChar, 500).Value = NewsObj.Brief;
				myCommand.Parameters.Add("@Details", SqlDbType.NVarChar, -1).Value = NewsObj.Details;
				//myCommand.Parameters.Add("@Date", SqlDbType.DateTime2, 8).Value = NewsObj.Date;

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
                SqlCommand myCommand = new SqlCommand("[dbo].[News_Delete]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                myCommand.Parameters.Add("@ID", SqlDbType.Int, 4).Value = ID;
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
        public List<NewsModel> GetPageByPage(int pageIndex, int pageSize, out int totalRecords)
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
                myCommand.Parameters.Add("@pageIndex", SqlDbType.Int, 4).Value = pageIndex;
                myCommand.Parameters.Add("@pageSize", SqlDbType.Int, 4).Value = pageSize;
                myCommand.Parameters.Add("@TotalRecords", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
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
        /// <param name="ID">The NewsObj id.</param>
        /// <returns>Model object.</returns>
        //--------------------------------------------------------------------
        public List<NewsModel> Get(int ID)
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
                if(ID>0)
                    myCommand.Parameters.Add("@ID", SqlDbType.Int, 4).Value = ID;
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
            //ID
            //------------------------------------------------
            if (reader["ID"] != DBNull.Value)
                NewsObj.ID = (int)reader["ID"];
            //------------------------------------------------

			//------------------------------------------------
			//[Title]
			//------------------------------------------------
			if (reader["Title"] != DBNull.Value)
			    NewsObj.Title = (string)reader["Title"];
			//------------------------------------------------
			//------------------------------------------------
			//[Brief]
			//------------------------------------------------
			if (reader["Brief"] != DBNull.Value)
			    NewsObj.Brief = (string)reader["Brief"];
			//------------------------------------------------
			//------------------------------------------------
			//[Details]
			//------------------------------------------------
			if (reader["Details"] != DBNull.Value)
			    NewsObj.Details = (string)reader["Details"];
			//------------------------------------------------
			//------------------------------------------------
			//[Date]
			//------------------------------------------------
            //if (reader["Date"] != DBNull.Value)
            //    NewsObj.Date = (DateTime)reader["Date"];
			//------------------------------------------------
            //Return the populated object
            return NewsObj;
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
    }
}