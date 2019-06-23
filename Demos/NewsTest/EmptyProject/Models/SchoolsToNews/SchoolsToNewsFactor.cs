using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Test.Models;

namespace Test
{   
    /// <summary>
    /// The factory class of SchoolsToNewsModel.
    /// </summary>
    public class SchoolsToNewsFactor
    {

        #region --------------Create--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Creates model object by calling News data provider create method.
        /// </summary>
        /// <param name="SchoolsToNewsObj">The model object.</param>
        /// <returns>The result of create operation.</returns>
        //--------------------------------------------------------------------
        public static bool Create(SchoolsToNewsModel SchoolsToNewsObj)
        {
            return SchoolsToNewsSqlDataPrvider.Instance.Create(SchoolsToNewsObj);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Update--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Updates model object by calling sql data provider update method.
        /// </summary>
        /// <param name="SchoolsToNewsObj">The model object.</param>
        /// <returns>The result of update operation.</returns>
        //--------------------------------------------------------------------
        public static bool Update(SchoolsToNewsModel SchoolsToNewsObj)
        {
            return SchoolsToNewsSqlDataPrvider.Instance.Update(SchoolsToNewsObj);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Delete--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Deletes single model object .
        /// </summary>
        /// <param name="ShoolsToNewsID">The model id.</param>
        /// <returns>The result of delete operation.</returns>
        //--------------------------------------------------------------------
        public static bool Delete(int ShoolsToNewsID)
        {
            return SchoolsToNewsSqlDataPrvider.Instance.Delete(ShoolsToNewsID);
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
            SchoolsToNewsSqlDataPrvider.Instance.DeleteGroupofObjects(ids);
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
        public static List<SchoolsToNewsModel> Get()
        {
            return SchoolsToNewsSqlDataPrvider.Instance.Get(-1);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------GetPageByPage--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets the spesific records.
        /// </summary>
        //--------------------------------------------------------------------
        public static List<SchoolsToNewsModel> GetPageByPage(int pageIndex, int pageSize,string sortname, string sortorder, string qtype, string query, out int totalRecords)
        {
            return SchoolsToNewsSqlDataPrvider.Instance.GetPageByPage(pageIndex, pageSize, sortname,  sortorder,  qtype,  query, out totalRecords);
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
        public static SchoolsToNewsModel GetObject(int ShoolsToNewsID)
        {
            SchoolsToNewsModel SchoolsToNewsObj = null;
            List<SchoolsToNewsModel> list = SchoolsToNewsSqlDataPrvider.Instance.Get(ShoolsToNewsID);
            if (list != null && list.Count > 0)
            {
                SchoolsToNewsObj = list[0];
            }
            return SchoolsToNewsObj;
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
    }
}