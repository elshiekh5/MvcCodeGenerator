using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;


namespace AppCore
{   
    /// <summary>
    /// The factory class of ImagesCategoriesModel.
    /// </summary>
    public class ImagesCategoriesFactor
    {

        #region --------------Create--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Creates model object by calling News data provider create method.
        /// </summary>
        /// <param name="ImagesCategoriesObj">The model object.</param>
        /// <returns>The result of create operation.</returns>
        //--------------------------------------------------------------------
        public static bool Create(ImagesCategoriesModel ImagesCategoriesObj)
        {
            return ImagesCategoriesSqlDataPrvider.Instance.Create(ImagesCategoriesObj);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Update--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Updates model object by calling sql data provider update method.
        /// </summary>
        /// <param name="ImagesCategoriesObj">The model object.</param>
        /// <returns>The result of update operation.</returns>
        //--------------------------------------------------------------------
        public static bool Update(ImagesCategoriesModel ImagesCategoriesObj)
        {
            return ImagesCategoriesSqlDataPrvider.Instance.Update(ImagesCategoriesObj);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Delete--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Deletes single model object .
        /// </summary>
        /// <param name="CategoryID">The model id.</param>
        /// <returns>The result of delete operation.</returns>
        //--------------------------------------------------------------------
        public static bool Delete(int CategoryID)
        {
            return ImagesCategoriesSqlDataPrvider.Instance.Delete(CategoryID);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------DeleteGroupofObjects--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Deletes multible objects .
        /// </summary>
        /// <param name="ids">The array of ids.</param>
        /// <returns>The result of delete operation.</returns>
        //--------------------------------------------------------------------
        public static void DeleteGroupofObjects(int[] ids)
        {
            ImagesCategoriesSqlDataPrvider.Instance.DeleteGroupofObjects(ids);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Get--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of objects.</returns>
        //--------------------------------------------------------------------
        public static List<ImagesCategoriesModel> Get()
        {
            return ImagesCategoriesSqlDataPrvider.Instance.Get(-1);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------GetPageByPage--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets the spesific records.
        /// </summary>
        //--------------------------------------------------------------------
        public static List<ImagesCategoriesModel> GetPageByPage(int pageIndex, int pageSize,string sortname, string sortorder, string qtype, string query, out int totalRecords)
        {
            return ImagesCategoriesSqlDataPrvider.Instance.GetPageByPage(pageIndex, pageSize, sortname,  sortorder,  qtype,  query, out totalRecords);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------GetObject--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets the spesific record.
        /// </summary>
        /// <param name="ID">The model id.</param>
        /// <returns>Model object.</returns>
        //--------------------------------------------------------------------
        public static ImagesCategoriesModel GetObject(int CategoryID)
        {
            ImagesCategoriesModel ImagesCategoriesObj = null;
            List<ImagesCategoriesModel> list = ImagesCategoriesSqlDataPrvider.Instance.Get(CategoryID);
            if (list != null && list.Count > 0)
            {
                ImagesCategoriesObj = list[0];
            }
            return ImagesCategoriesObj;
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
    }
}