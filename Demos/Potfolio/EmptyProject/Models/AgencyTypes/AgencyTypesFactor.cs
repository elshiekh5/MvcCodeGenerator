using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Portfolio.Models;

namespace Portfolio
{   
    /// <summary>
    /// The factory class of AgencyTypesModel.
    /// </summary>
    public class AgencyTypesFactor
    {

        #region --------------Create--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Creates model object by calling News data provider create method.
        /// </summary>
        /// <param name="AgencyTypesObj">The model object.</param>
        /// <returns>The result of create operation.</returns>
        //--------------------------------------------------------------------
        public static bool Create(AgencyTypesModel AgencyTypesObj)
        {
            return AgencyTypesSqlDataPrvider.Instance.Create(AgencyTypesObj);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Update--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Updates model object by calling sql data provider update method.
        /// </summary>
        /// <param name="AgencyTypesObj">The model object.</param>
        /// <returns>The result of update operation.</returns>
        //--------------------------------------------------------------------
        public static bool Update(AgencyTypesModel AgencyTypesObj)
        {
            return AgencyTypesSqlDataPrvider.Instance.Update(AgencyTypesObj);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Delete--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Deletes single model object .
        /// </summary>
        /// <param name="AgencyTypeID">The model id.</param>
        /// <returns>The result of delete operation.</returns>
        //--------------------------------------------------------------------
        public static bool Delete(int AgencyTypeID)
        {
            return AgencyTypesSqlDataPrvider.Instance.Delete(AgencyTypeID);
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
            AgencyTypesSqlDataPrvider.Instance.DeleteGroupofObjects(ids);
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
        public static List<AgencyTypesModel> Get()
        {
            return AgencyTypesSqlDataPrvider.Instance.Get(-1);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------GetPageByPage--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets the spesific records.
        /// </summary>
        //--------------------------------------------------------------------
        public static List<AgencyTypesModel> GetPageByPage(int pageIndex, int pageSize,string sortname, string sortorder, string qtype, string query, out int totalRecords)
        {
            return AgencyTypesSqlDataPrvider.Instance.GetPageByPage(pageIndex, pageSize, sortname,  sortorder,  qtype,  query, out totalRecords);
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
        public static AgencyTypesModel GetObject(int AgencyTypeID)
        {
            AgencyTypesModel AgencyTypesObj = null;
            List<AgencyTypesModel> list = AgencyTypesSqlDataPrvider.Instance.Get(AgencyTypeID);
            if (list != null && list.Count > 0)
            {
                AgencyTypesObj = list[0];
            }
            return AgencyTypesObj;
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
    }
}