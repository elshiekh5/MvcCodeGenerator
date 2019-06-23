using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Testing.Models;
using Testing.DAL;

namespace Testing.Business.Factors
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

        #region --------------Updat--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Updates model object by calling sql data provider update method.
        /// </summary>
        /// <param name="NewsObj">The model object.</param>
        /// <returns>The result of update operation.</returns>
        //--------------------------------------------------------------------
        public static bool Updat(NewsModel NewsObj)
        {
            return NewsSqlDataPrvider.Instance.Updat(NewsObj);
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
            return NewsSqlDataPrvider.Instance.Delete(ID);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------GetPageByPage--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets the spesific records.
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        //--------------------------------------------------------------------
        public static List<NewsModel> GetPageByPage(int pageIndex, int pageSize, out int totalRecords)
        {
            return NewsSqlDataPrvider.Instance.GetPageByPage(pageIndex, pageSize, out totalRecords);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Get--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets the spesific records.
        /// </summary>
        /// <returns>Model object.</returns>
        //--------------------------------------------------------------------
        public static List<NewsModel> Get()
        {
            return NewsSqlDataPrvider.Instance.Get(-1);
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
        public static NewsModel GetObject(int ID)
        {
            NewsModel NewsObj = null;
            List<NewsModel> NewsList = NewsSqlDataPrvider.Instance.Get(ID);
             if (NewsList != null && NewsList.Count > 0)
             {
                 NewsObj = NewsList[0];
             }
             return NewsObj;
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
    }
}