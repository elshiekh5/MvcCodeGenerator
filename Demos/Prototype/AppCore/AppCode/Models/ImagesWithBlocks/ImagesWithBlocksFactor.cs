using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;


namespace AppCore
{   
    /// <summary>
    /// The factory class of ImagesWithBlocksModel.
    /// </summary>
    public class ImagesWithBlocksFactor
    {

        #region --------------Create--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Creates model object by calling News data provider create method.
        /// </summary>
        /// <param name="ImagesWithBlocksObj">The model object.</param>
        /// <returns>The result of create operation.</returns>
        //--------------------------------------------------------------------
        public static bool Create(ImagesWithBlocksModel ImagesWithBlocksObj)
        {
            return ImagesWithBlocksSqlDataPrvider.Instance.Create(ImagesWithBlocksObj);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Update--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Updates model object by calling sql data provider update method.
        /// </summary>
        /// <param name="ImagesWithBlocksObj">The model object.</param>
        /// <returns>The result of update operation.</returns>
        //--------------------------------------------------------------------
        public static bool Update(ImagesWithBlocksModel ImagesWithBlocksObj)
        {
            return ImagesWithBlocksSqlDataPrvider.Instance.Update(ImagesWithBlocksObj);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Delete--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Deletes single model object .
        /// </summary>
        /// <param name="ID">The model id.</param>
        /// <returns>The result of delete operation.</returns>
        //--------------------------------------------------------------------
        public static bool Delete(int ID)
        {
            return ImagesWithBlocksSqlDataPrvider.Instance.Delete(ID);
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
            ImagesWithBlocksSqlDataPrvider.Instance.DeleteGroupofObjects(ids);
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
        public static List<ImagesWithBlocksModel> Get()
        {
            return ImagesWithBlocksSqlDataPrvider.Instance.Get(-1);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------GetPageByPage--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets the spesific records.
        /// </summary>
        //--------------------------------------------------------------------
        public static List<ImagesWithBlocksModel> GetPageByPage(int pageIndex, int pageSize,string sortname, string sortorder, string qtype, string query, out int totalRecords)
        {
            return ImagesWithBlocksSqlDataPrvider.Instance.GetPageByPage(pageIndex, pageSize, sortname,  sortorder,  qtype,  query, out totalRecords);
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
        public static ImagesWithBlocksModel GetObject(int ID)
        {
            ImagesWithBlocksModel ImagesWithBlocksObj = null;
            List<ImagesWithBlocksModel> list = ImagesWithBlocksSqlDataPrvider.Instance.Get(ID);
            if (list != null && list.Count > 0)
            {
                ImagesWithBlocksObj = list[0];
            }
            return ImagesWithBlocksObj;
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
    }
}