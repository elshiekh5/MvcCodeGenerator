using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;


namespace AppCore
{   
    /// <summary>
    /// The factory class of RequestsModel.
    /// </summary>
    public class RequestsFactor
    {

        #region --------------Create--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Creates model object by calling News data provider create method.
        /// </summary>
        /// <param name="RequestsObj">The model object.</param>
        /// <returns>The result of create operation.</returns>
        //--------------------------------------------------------------------
        public static bool Create(RequestsModel RequestsObj)
        {
            return RequestsSqlDataPrvider.Instance.Create(RequestsObj);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Update--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Updates model object by calling sql data provider update method.
        /// </summary>
        /// <param name="RequestsObj">The model object.</param>
        /// <returns>The result of update operation.</returns>
        //--------------------------------------------------------------------
        public static bool Update(RequestsModel RequestsObj)
        {
            return RequestsSqlDataPrvider.Instance.Update(RequestsObj);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Delete--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Deletes single model object .
        /// </summary>
        /// <param name="RequestID">The model id.</param>
        /// <returns>The result of delete operation.</returns>
        //--------------------------------------------------------------------
        public static bool Delete(int RequestID)
        {
            return RequestsSqlDataPrvider.Instance.Delete(RequestID);
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
            RequestsSqlDataPrvider.Instance.DeleteGroupofObjects(ids);
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
        public static List<RequestsModel> Get()
        {
            return RequestsSqlDataPrvider.Instance.Get(-1);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------GetPageByPage--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets the spesific records.
        /// </summary>
        //--------------------------------------------------------------------
        public static List<RequestsModel> GetPageByPage(int pageIndex, int pageSize,string sortname, string sortorder, string qtype, string query, out int totalRecords)
        {
            return RequestsSqlDataPrvider.Instance.GetPageByPage(pageIndex, pageSize, sortname,  sortorder,  qtype,  query, out totalRecords);
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
        public static RequestsModel GetObject(int RequestID)
        {
            RequestsModel RequestsObj = null;
            List<RequestsModel> list = RequestsSqlDataPrvider.Instance.Get(RequestID);
            if (list != null && list.Count > 0)
            {
                RequestsObj = list[0];
            }
            return RequestsObj;
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
    }
}