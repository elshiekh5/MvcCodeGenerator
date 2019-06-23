using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Test.Models;

namespace Test
{   
    /// <summary>
    /// The factory class of SchoolsModel.
    /// </summary>
    public class SchoolsFactor
    {

        #region --------------Create--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Creates model object by calling News data provider create method.
        /// </summary>
        /// <param name="SchoolsObj">The model object.</param>
        /// <returns>The result of create operation.</returns>
        //--------------------------------------------------------------------
        public static bool Create(SchoolsModel SchoolsObj)
        {
            return SchoolsSqlDataPrvider.Instance.Create(SchoolsObj);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Update--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Updates model object by calling sql data provider update method.
        /// </summary>
        /// <param name="SchoolsObj">The model object.</param>
        /// <returns>The result of update operation.</returns>
        //--------------------------------------------------------------------
        public static bool Update(SchoolsModel SchoolsObj)
        {
            return SchoolsSqlDataPrvider.Instance.Update(SchoolsObj);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Delete--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Deletes single model object .
        /// </summary>
        /// <param name="SchoolID">The model id.</param>
        /// <returns>The result of delete operation.</returns>
        //--------------------------------------------------------------------
        public static bool Delete(int SchoolID)
        {
            return SchoolsSqlDataPrvider.Instance.Delete(SchoolID);
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
            SchoolsSqlDataPrvider.Instance.DeleteGroupofObjects(ids);
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
        public static List<SchoolsModel> Get()
        {
            return SchoolsSqlDataPrvider.Instance.Get(-1);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------GetPageByPage--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets the spesific records.
        /// </summary>
        //--------------------------------------------------------------------
        public static List<SchoolsModel> GetPageByPage(int pageIndex, int pageSize,string sortname, string sortorder, string qtype, string query, out int totalRecords)
        {
            return SchoolsSqlDataPrvider.Instance.GetPageByPage(pageIndex, pageSize, sortname,  sortorder,  qtype,  query, out totalRecords);
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
        public static SchoolsModel GetObject(int SchoolID)
        {
            SchoolsModel SchoolsObj = null;
            List<SchoolsModel> list = SchoolsSqlDataPrvider.Instance.Get(SchoolID);
            if (list != null && list.Count > 0)
            {
                SchoolsObj = list[0];
            }
            return SchoolsObj;
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
    }
}