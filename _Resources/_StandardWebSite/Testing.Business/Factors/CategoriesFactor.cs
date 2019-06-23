using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using News.DAL;
using News.Models;

namespace News.Business.Factors
{   
    /// <summary>
    /// The factory class of CategoriesModel.
    /// </summary>
    public class CategoriesFactor
    {

        #region --------------Create--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Creates model object by calling News data provider create method.
        /// </summary>
        /// <param name="CategoriesObj">The model object.</param>
        /// <returns>The result of create operation.</returns>
        //--------------------------------------------------------------------
        public static bool Create(CategoriesModel CategoriesObj)
        {
            return CategoriesSqlDataPrvider.Instance.Create(CategoriesObj);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Update--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Updates model object by calling sql data provider update method.
        /// </summary>
        /// <param name="CategoriesObj">The model object.</param>
        /// <returns>The result of update operation.</returns>
        //--------------------------------------------------------------------
        public static bool Update(CategoriesModel CategoriesObj)
        {
            return CategoriesSqlDataPrvider.Instance.Update(CategoriesObj);
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
            return CategoriesSqlDataPrvider.Instance.Delete(CategoryID);
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
        public static List<CategoriesModel> GetPageByPage(int pageIndex, int pageSize, out int totalRecords)
        {
            return CategoriesSqlDataPrvider.Instance.GetPageByPage(pageIndex, pageSize, out totalRecords);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Get--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets the spesific records.
        /// </summary>
        /// <param name="CategoryID">The model id.</param>
        /// <returns>Model object.</returns>
        //--------------------------------------------------------------------
        public static List<CategoriesModel> Get()
        {
            return CategoriesSqlDataPrvider.Instance.Get(-1);
        }
        public static List<CategoriesModel> Get(int CategoryID)
        {
            return CategoriesSqlDataPrvider.Instance.Get(CategoryID);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
    }
}