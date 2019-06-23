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
    /// News SQL data provider which represents the data access layer of Images.
    /// </summary>
    public class ImagesSqlDataPrvider
    {

        #region --------------Instance--------------
        //------------------------------------------------------------------------------------------------------
        private static ImagesSqlDataPrvider _Instance;
        //--------------------------------------------------------------------
        /// <summary>
        /// Gets instance of ImagesSqlDataPrvider calss.
        /// <example>ImagesSqlDataPrvider dp=ImagesSqlDataPrvider.Instance.</example>
        /// </summary>
        //--------------------------------------------------------------------
        public static ImagesSqlDataPrvider Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ImagesSqlDataPrvider();
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
        /// <param name="ImagesObj">Model object.</param>
        /// <returns>The result of create query.</returns>
        //--------------------------------------------------------------------
        public bool Create(ImagesModel ImagesObj)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[Images_Create]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
				myCommand.Parameters.Add("@ImageID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
				myCommand.Parameters.Add("@CategoryID", SqlDbType.Int, 4).Value = ImagesObj.CategoryID;
				myCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 128).Value = ImagesObj.Name;
				myCommand.Parameters.Add("@Path", SqlDbType.NVarChar, 512).Value = ImagesObj.Path;
				myCommand.Parameters.Add("@ImageWidth", SqlDbType.Int, 4).Value = ImagesObj.ImageWidth;
				myCommand.Parameters.Add("@ImageHeight", SqlDbType.Int, 4).Value = ImagesObj.ImageHeight;
				myCommand.Parameters.Add("@ImageSize", SqlDbType.Int, 4).Value = ImagesObj.ImageSize;
				myCommand.Parameters.Add("@ImageExtension", SqlDbType.VarChar, 5).Value = ImagesObj.ImageExtension;
				myCommand.Parameters.Add("@Data", SqlDbType.Image).Value = ImagesObj.Data;
				myCommand.Parameters.Add("@AppearingCount", SqlDbType.Int, 4).Value = ImagesObj.AppearingCount;
				myCommand.Parameters.Add("@Identifire", SqlDbType.VarChar, 128).Value = ImagesObj.Identifire;
				myCommand.Parameters.Add("@BlocKeys", SqlDbType.VarChar, 128).Value = ImagesObj.BlocKeys;

                //----------------------------------------------------------------------------------------------
                // Execute the command
                //----------------------------------------------------------------------------------------------
                myConnection.Open();
                if (myCommand.ExecuteNonQuery() > 0)
                {
                    result = true;
                    //Get ID value from database and set it in object
				ImagesObj.ImageID = (int)myCommand.Parameters["@ImageID"].Value;

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
        /// <param name="ImagesObj">Model object.</param>
        /// <returns>The result of update query.</returns>
        //--------------------------------------------------------------------
        public bool Update(ImagesModel ImagesObj)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[Images_Update]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                
				myCommand.Parameters.Add("@ImageID", SqlDbType.Int, 4).Value = ImagesObj.ImageID;
				myCommand.Parameters.Add("@CategoryID", SqlDbType.Int, 4).Value = ImagesObj.CategoryID;
				myCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 128).Value = ImagesObj.Name;
				myCommand.Parameters.Add("@Path", SqlDbType.NVarChar, 512).Value = ImagesObj.Path;
				myCommand.Parameters.Add("@ImageWidth", SqlDbType.Int, 4).Value = ImagesObj.ImageWidth;
				myCommand.Parameters.Add("@ImageHeight", SqlDbType.Int, 4).Value = ImagesObj.ImageHeight;
				myCommand.Parameters.Add("@ImageSize", SqlDbType.Int, 4).Value = ImagesObj.ImageSize;
				myCommand.Parameters.Add("@ImageExtension", SqlDbType.VarChar, 5).Value = ImagesObj.ImageExtension;
				myCommand.Parameters.Add("@Data", SqlDbType.Image).Value = ImagesObj.Data;
				myCommand.Parameters.Add("@AppearingCount", SqlDbType.Int, 4).Value = ImagesObj.AppearingCount;
				myCommand.Parameters.Add("@Identifire", SqlDbType.VarChar, 128).Value = ImagesObj.Identifire;
				myCommand.Parameters.Add("@BlocKeys", SqlDbType.VarChar, 128).Value = ImagesObj.BlocKeys;

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
        /// <param name="ImageID">The model id.</param>
        /// <returns>The result of delete query.</returns>
        //--------------------------------------------------------------------
        public bool Delete(int ImageID)
        {
            bool result = false;
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand("[dbo].[Images_Delete]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                if(ImageID>0)myCommand.Parameters.Add("@ImageID", SqlDbType.Int, 4).Value = ImageID;

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
                SqlCommand myCommand = new SqlCommand("[dbo].[Images_Delete]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                myCommand.Parameters.Add("@ImageID", SqlDbType.Int, 4);

                //----------------------------------------------------------------------------------------------
                // Execute the command
                //----------------------------------------------------------------------------------------------
                myConnection.Open();
                foreach (int id in ids)
                {
                    myCommand.Parameters["@ImageID"].Value = id;
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
        public List<ImagesModel> GetPageByPage(int pageIndex, int pageSize, string sortname, string sortorder, string qtype, string query, out int totalRecords)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                //----------------------------------------------------------------------------------------------
                List<ImagesModel> ImagesList = new List<ImagesModel>();
                ImagesModel ImagesObj;
                //----------------------------------------------------------------------------------------------
                SqlCommand myCommand = new SqlCommand("[dbo].[Images_GetPageByPage]", myConnection);
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
                    ImagesObj = PopulateModelFromIDataReader(dr);
                    ImagesList.Add(ImagesObj);
                }
                dr.Close();
                totalRecords = (int)myCommand.Parameters["@TotalRecords"].Value;
                myConnection.Close();
                //----------------------------------------------------------------------------------------------
                return ImagesList;
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
        /// <param name="ImageID">The ImagesObj id.</param>
        /// <returns>Model object.</returns>
        //--------------------------------------------------------------------
        public List<ImagesModel> Get(int ImageID)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                //----------------------------------------------------------------------------------------------
                List<ImagesModel> ImagesList = new List<ImagesModel>();
                ImagesModel ImagesObj;
                //----------------------------------------------------------------------------------------------
                SqlCommand myCommand = new SqlCommand("[dbo].[Images_Get]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                if(ImageID>0)myCommand.Parameters.Add("@ImageID", SqlDbType.Int, 4).Value = ImageID;

                 // Execute the command
                //----------------------------------------------------------------------------------------------
                SqlDataReader dr;
                myConnection.Open();
                dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    ImagesObj = PopulateModelFromIDataReader(dr);
                    ImagesList.Add(ImagesObj);
                }
                dr.Close();
                myConnection.Close();
                //----------------------------------------------------------------------------------------------
                return ImagesList;
                //----------------------------------------------------------------------------------------------
            }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region /*--------- GetRandomImage ---------*\
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets the spesific Records.
        /// </summary>
        /// <param name="ImageID">The ImagesObj id.</param>
        /// <returns>Model object.</returns>
        //--------------------------------------------------------------------
        public ImagesModel GetRandomImage(int requestID, int categoryID, int width, int height)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                //----------------------------------------------------------------------------------------------
                ImagesModel ImagesObj = null;
                //----------------------------------------------------------------------------------------------
                SqlCommand myCommand = new SqlCommand("[dbo].[Images_GetRandomImage]", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //----------------------------------------------------------------------------------------------
                // Set the parameters
                //----------------------------------------------------------------------------------------------
                myCommand.Parameters.Add("@RequestID", SqlDbType.Int, 4).Value = requestID;
                myCommand.Parameters.Add("@CategoryID", SqlDbType.Int, 4).Value = categoryID;
                myCommand.Parameters.Add("@Width", SqlDbType.Decimal).Value = width;
                myCommand.Parameters.Add("@Height", SqlDbType.Decimal).Value = height;

                // Execute the command
                //----------------------------------------------------------------------------------------------
                SqlDataReader dr;
                myConnection.Open();
                dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    ImagesObj = PopulateModelFromIDataReader(dr);
                }
                dr.Close();
                myConnection.Close();
                //----------------------------------------------------------------------------------------------
                return ImagesObj;
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
        private ImagesModel PopulateModelFromIDataReader(IDataReader reader)
        {
            //Create a new object
            ImagesModel ImagesObj = new ImagesModel();
            //Fill the object with data

			//------------------------------------------------
			//[ImageID]
			//------------------------------------------------
			if (reader["ImageID"] != DBNull.Value)
			    ImagesObj.ImageID = (int)reader["ImageID"];
			//------------------------------------------------
			//------------------------------------------------
			//[CategoryID]
			//------------------------------------------------
			if (reader["CategoryID"] != DBNull.Value)
			    ImagesObj.CategoryID = (int)reader["CategoryID"];
			//------------------------------------------------
			//------------------------------------------------
			//[Name]
			//------------------------------------------------
			if (reader["Name"] != DBNull.Value)
			    ImagesObj.Name = (string)reader["Name"];
			//------------------------------------------------
			//------------------------------------------------
			//[Path]
			//------------------------------------------------
			if (reader["Path"] != DBNull.Value)
			    ImagesObj.Path = (string)reader["Path"];
			//------------------------------------------------
			//------------------------------------------------
			//[ImageWidth]
			//------------------------------------------------
			if (reader["ImageWidth"] != DBNull.Value)
			    ImagesObj.ImageWidth = (int)reader["ImageWidth"];
			//------------------------------------------------
			//------------------------------------------------
			//[ImageHeight]
			//------------------------------------------------
			if (reader["ImageHeight"] != DBNull.Value)
			    ImagesObj.ImageHeight = (int)reader["ImageHeight"];
			//------------------------------------------------
			//------------------------------------------------
			//[ImageSize]
			//------------------------------------------------
			if (reader["ImageSize"] != DBNull.Value)
			    ImagesObj.ImageSize = (int)reader["ImageSize"];
			//------------------------------------------------
			//------------------------------------------------
			//[ImageExtension]
			//------------------------------------------------
			if (reader["ImageExtension"] != DBNull.Value)
			    ImagesObj.ImageExtension = (string)reader["ImageExtension"];
			//------------------------------------------------
			//------------------------------------------------
			//[Data]
			//------------------------------------------------
			if (reader["Data"] != DBNull.Value)
			    ImagesObj.Data = (byte[])reader["Data"];
			//------------------------------------------------
			//------------------------------------------------
			//[AppearingCount]
			//------------------------------------------------
			if (reader["AppearingCount"] != DBNull.Value)
			    ImagesObj.AppearingCount = (int)reader["AppearingCount"];
			//------------------------------------------------
			//------------------------------------------------
			//[Identifire]
			//------------------------------------------------
			if (reader["Identifire"] != DBNull.Value)
			    ImagesObj.Identifire = (string)reader["Identifire"];
			//------------------------------------------------
			//------------------------------------------------
			//[BlocKeys]
			//------------------------------------------------
			if (reader["BlocKeys"] != DBNull.Value)
			    ImagesObj.BlocKeys = (string)reader["BlocKeys"];
			//------------------------------------------------
            //Return the populated object
            return ImagesObj;
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
    }
}