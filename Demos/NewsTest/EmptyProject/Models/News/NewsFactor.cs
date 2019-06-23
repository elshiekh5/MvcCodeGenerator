using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Test.Models;

namespace Test
{   
    /// <summary>
    /// The factory class of NewsModel.
    /// </summary>
    public class NewsFactor
    {

        #region --------------Create--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Creates model object by calling News data provider create method.
        /// </summary>
        /// <param name="NewsObj">The model object.</param>
        /// <returns>The result of create operation.</returns>
        //--------------------------------------------------------------------
        public static bool Create(NewsModel NewsObj)
        {
            return NewsSqlDataPrvider.Instance.Create(NewsObj);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Update--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Updates model object by calling sql data provider update method.
        /// </summary>
        /// <param name="NewsObj">The model object.</param>
        /// <returns>The result of update operation.</returns>
        //--------------------------------------------------------------------
        public static bool Update(NewsModel NewsObj)
        {
            return NewsSqlDataPrvider.Instance.Update(NewsObj);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Delete--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Deletes single model object .
        /// </summary>
        /// <param name="NewsID">The model id.</param>
        /// <returns>The result of delete operation.</returns>
        //--------------------------------------------------------------------
        public static bool Delete(int NewsID)
        {
            return NewsSqlDataPrvider.Instance.Delete(NewsID);
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
            NewsSqlDataPrvider.Instance.DeleteGroupofObjects(ids);
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
        public static List<NewsModel> Get()
        {
            return NewsSqlDataPrvider.Instance.Get(-1);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------GetPageByPage--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets the spesific records.
        /// </summary>
        //--------------------------------------------------------------------
        public static List<NewsModel> GetPageByPage(int pageIndex, int pageSize,string sortname, string sortorder, string qtype, string query, out int totalRecords)
        {
            return NewsSqlDataPrvider.Instance.GetPageByPage(pageIndex, pageSize, sortname,  sortorder,  qtype,  query, out totalRecords);
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
        public static NewsModel GetObject(int NewsID)
        {
            NewsModel NewsObj = null;
            List<NewsModel> list = NewsSqlDataPrvider.Instance.Get(NewsID);
            if (list != null && list.Count > 0)
            {
                NewsObj = list[0];
            }
            return NewsObj;
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
    }
}