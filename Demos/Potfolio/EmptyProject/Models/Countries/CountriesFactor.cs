using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Portfolio.Models;

namespace Portfolio
{   
    /// <summary>
    /// The factory class of CountriesModel.
    /// </summary>
    public class CountriesFactor
    {

        #region --------------Create--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Creates model object by calling News data provider create method.
        /// </summary>
        /// <param name="CountriesObj">The model object.</param>
        /// <returns>The result of create operation.</returns>
        //--------------------------------------------------------------------
        public static bool Create(CountriesModel CountriesObj)
        {
            return CountriesSqlDataPrvider.Instance.Create(CountriesObj);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Update--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Updates model object by calling sql data provider update method.
        /// </summary>
        /// <param name="CountriesObj">The model object.</param>
        /// <returns>The result of update operation.</returns>
        //--------------------------------------------------------------------
        public static bool Update(CountriesModel CountriesObj)
        {
            return CountriesSqlDataPrvider.Instance.Update(CountriesObj);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Delete--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Deletes single model object .
        /// </summary>
        /// <param name="CountryID">The model id.</param>
        /// <returns>The result of delete operation.</returns>
        //--------------------------------------------------------------------
        public static bool Delete(int CountryID)
        {
            return CountriesSqlDataPrvider.Instance.Delete(CountryID);
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
            CountriesSqlDataPrvider.Instance.DeleteGroupofObjects(ids);
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
        public static List<CountriesModel> Get()
        {
            return CountriesSqlDataPrvider.Instance.Get(-1);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------GetPageByPage--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets the spesific records.
        /// </summary>
        //--------------------------------------------------------------------
        public static List<CountriesModel> GetPageByPage(int pageIndex, int pageSize,string sortname, string sortorder, string qtype, string query, out int totalRecords)
        {
            return CountriesSqlDataPrvider.Instance.GetPageByPage(pageIndex, pageSize, sortname,  sortorder,  qtype,  query, out totalRecords);
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
        public static CountriesModel GetObject(int CountryID)
        {
            CountriesModel CountriesObj = null;
            List<CountriesModel> list = CountriesSqlDataPrvider.Instance.Get(CountryID);
            if (list != null && list.Count > 0)
            {
                CountriesObj = list[0];
            }
            return CountriesObj;
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
    }
}