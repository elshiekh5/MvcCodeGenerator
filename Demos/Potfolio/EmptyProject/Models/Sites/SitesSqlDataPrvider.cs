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
    /// News SQL data provider which represents the data access layer of Sites.
    /// </summary>
    public class SitesSqlDataPrvider
    {

        #region --------------Instance--------------
        //------------------------------------------------------------------------------------------------------
        private static SitesSqlDataPrvider _Instance;
        //--------------------------------------------------------------------
        /// <summary>
        /// Gets instance of SitesSqlDataPrvider calss.
        /// <example>SitesSqlDataPrvider dp=SitesSqlDataPrvider.Instance.</example>
        /// </summary>
        //--------------------------------------------------------------------
        public static SitesSqlDataPrvider Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new SitesSqlDataPrvider();
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
        /// <param name="SitesObj">Model object.</param>
        /// <returns>The result of create query.</returns>
        //--------------------------------------------------------------------
        public bool Create(SitesModel SitesObj)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[Sites_Create]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
				myCommand.Parameters.Add("@SiteID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
				myCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 128).Value = SitesObj.Name;
				myCommand.Parameters.Add("@Url", SqlDbType.NVarChar, 128).Value = SitesObj.Url;
				myCommand.Parameters.Add("@PhotoFile", SqlDbType.NVarChar, 128).Value = SitesObj.PhotoFile;
				myCommand.Parameters.Add("@LogoFile", SqlDbType.NVarChar, 128).Value = SitesObj.LogoFile;
				myCommand.Parameters.Add("@Brief", SqlDbType.NVarChar, 512).Value = SitesObj.Brief;
				myCommand.Parameters.Add("@Address", SqlDbType.NVarChar, 128).Value = SitesObj.Address;
				myCommand.Parameters.Add("@AgencyTypeID", SqlDbType.Int, 4).Value = SitesObj.AgencyTypeID;
				myCommand.Parameters.Add("@BusinessSectionID", SqlDbType.Int, 4).Value = SitesObj.BusinessSectionID;
				myCommand.Parameters.Add("@CountryID", SqlDbType.Int, 4).Value = SitesObj.CountryID;

                //----------------------------------------------------------------------------------------------
                // Execute the command
                //----------------------------------------------------------------------------------------------
                myConnection.Open();
                if (myCommand.ExecuteNonQuery() > 0)
                {
                    result = true;
                    //Get ID value from database and set it in object
				SitesObj.SiteID = (int)myCommand.Parameters["@SiteID"].Value;

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
        /// <param name="SitesObj">Model object.</param>
        /// <returns>The result of update query.</returns>
        //--------------------------------------------------------------------
        public bool Update(SitesModel SitesObj)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[Sites_Update]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                
				myCommand.Parameters.Add("@SiteID", SqlDbType.Int, 4).Value = SitesObj.SiteID;
				myCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 128).Value = SitesObj.Name;
				myCommand.Parameters.Add("@Url", SqlDbType.NVarChar, 128).Value = SitesObj.Url;
				myCommand.Parameters.Add("@PhotoFile", SqlDbType.NVarChar, 128).Value = SitesObj.PhotoFile;
				myCommand.Parameters.Add("@LogoFile", SqlDbType.NVarChar, 128).Value = SitesObj.LogoFile;
				myCommand.Parameters.Add("@Brief", SqlDbType.NVarChar, 512).Value = SitesObj.Brief;
				myCommand.Parameters.Add("@Address", SqlDbType.NVarChar, 128).Value = SitesObj.Address;
				myCommand.Parameters.Add("@AgencyTypeID", SqlDbType.Int, 4).Value = SitesObj.AgencyTypeID;
				myCommand.Parameters.Add("@BusinessSectionID", SqlDbType.Int, 4).Value = SitesObj.BusinessSectionID;
				myCommand.Parameters.Add("@CountryID", SqlDbType.Int, 4).Value = SitesObj.CountryID;

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
        /// <param name="SiteID">The model id.</param>
        /// <returns>The result of delete query.</returns>
        //--------------------------------------------------------------------
        public bool Delete(int SiteID)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[Sites_Delete]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                if(SiteID>0)myCommand.Parameters.Add("@SiteID", SqlDbType.Int, 4).Value = SiteID;

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
                SqlCommand myCommand = new SqlCommand("[dbo].[Sites_Delete]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                myCommand.Parameters.Add("@SiteID", SqlDbType.Int, 4);

                //----------------------------------------------------------------------------------------------
                // Execute the command
                //----------------------------------------------------------------------------------------------
                myConnection.Open();
                foreach (int id in ids)
                {
                    myCommand.Parameters["@SiteID"].Value = id;
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
        public List<SitesModel> GetPageByPage(int pageIndex, int pageSize, string sortname, string sortorder, string qtype, string query, out int totalRecords)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                //----------------------------------------------------------------------------------------------
                List<SitesModel> SitesList = new List<SitesModel>();
                SitesModel SitesObj;
                //----------------------------------------------------------------------------------------------
                SqlCommand myCommand = new SqlCommand("[dbo].[Sites_GetPageByPage]", myConnection);
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
                    SitesObj = PopulateModelFromIDataReader(dr);
                    SitesList.Add(SitesObj);
                }
                dr.Close();
                totalRecords = (int)myCommand.Parameters["@TotalRecords"].Value;
                myConnection.Close();
                //----------------------------------------------------------------------------------------------
                return SitesList;
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
        /// <param name="SiteID">The SitesObj id.</param>
        /// <returns>Model object.</returns>
        //--------------------------------------------------------------------
        public List<SitesModel> Get(int SiteID)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                //----------------------------------------------------------------------------------------------
                List<SitesModel> SitesList = new List<SitesModel>();
                SitesModel SitesObj;
                //----------------------------------------------------------------------------------------------
                SqlCommand myCommand = new SqlCommand("[dbo].[Sites_Get]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                if(SiteID>0)myCommand.Parameters.Add("@SiteID", SqlDbType.Int, 4).Value = SiteID;

                 // Execute the command
                //----------------------------------------------------------------------------------------------
                SqlDataReader dr;
                myConnection.Open();
                dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    SitesObj = PopulateModelFromIDataReader(dr);
                    SitesList.Add(SitesObj);
                }
                dr.Close();
                myConnection.Close();
                //----------------------------------------------------------------------------------------------
                return SitesList;
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
        private SitesModel PopulateModelFromIDataReader(IDataReader reader)
        {
            //Create a new object
            SitesModel SitesObj = new SitesModel();
            //Fill the object with data

			//------------------------------------------------
			//[SiteID]
			//------------------------------------------------
			if (reader["SiteID"] != DBNull.Value)
			    SitesObj.SiteID = (int)reader["SiteID"];
			//------------------------------------------------
			//------------------------------------------------
			//[Name]
			//------------------------------------------------
			if (reader["Name"] != DBNull.Value)
			    SitesObj.Name = (string)reader["Name"];
			//------------------------------------------------
			//------------------------------------------------
			//[Url]
			//------------------------------------------------
			if (reader["Url"] != DBNull.Value)
			    SitesObj.Url = (string)reader["Url"];
			//------------------------------------------------
			//------------------------------------------------
			//[PhotoFile]
			//------------------------------------------------
			if (reader["PhotoFile"] != DBNull.Value)
			    SitesObj.PhotoFile = (string)reader["PhotoFile"];
			//------------------------------------------------
			//------------------------------------------------
			//[LogoFile]
			//------------------------------------------------
			if (reader["LogoFile"] != DBNull.Value)
			    SitesObj.LogoFile = (string)reader["LogoFile"];
			//------------------------------------------------
			//------------------------------------------------
			//[Brief]
			//------------------------------------------------
			if (reader["Brief"] != DBNull.Value)
			    SitesObj.Brief = (string)reader["Brief"];
			//------------------------------------------------
			//------------------------------------------------
			//[Address]
			//------------------------------------------------
			if (reader["Address"] != DBNull.Value)
			    SitesObj.Address = (string)reader["Address"];
			//------------------------------------------------
			//------------------------------------------------
			//[AgencyTypeID]
			//------------------------------------------------
			if (reader["AgencyTypeID"] != DBNull.Value)
			    SitesObj.AgencyTypeID = (int)reader["AgencyTypeID"];
			//------------------------------------------------
			//------------------------------------------------
			//[BusinessSectionID]
			//------------------------------------------------
			if (reader["BusinessSectionID"] != DBNull.Value)
			    SitesObj.BusinessSectionID = (int)reader["BusinessSectionID"];
			//------------------------------------------------
			//------------------------------------------------
			//[CountryID]
			//------------------------------------------------
			if (reader["CountryID"] != DBNull.Value)
			    SitesObj.CountryID = (int)reader["CountryID"];
			//------------------------------------------------
            //Return the populated object
            return SitesObj;
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
    }
}