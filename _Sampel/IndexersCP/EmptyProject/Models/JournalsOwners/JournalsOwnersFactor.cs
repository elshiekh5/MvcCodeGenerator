using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using IndexersCP4.Models;

namespace IndexersCP4
{   
    /// <summary>
    /// The factory class of JournalsOwnersModel.
    /// </summary>
    public class JournalsOwnersFactor
    {

        #region --------------Create--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Creates model object by calling News data provider create method.
        /// </summary>
        /// <param name="JournalsOwnersObj">The model object.</param>
        /// <returns>The result of create operation.</returns>
        //--------------------------------------------------------------------
        public static bool Create(JournalsOwnersModel JournalsOwnersObj)
        {
            return JournalsOwnersSqlDataPrvider.Instance.Create(JournalsOwnersObj);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Update--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Updates model object by calling sql data provider update method.
        /// </summary>
        /// <param name="JournalsOwnersObj">The model object.</param>
        /// <returns>The result of update operation.</returns>
        //--------------------------------------------------------------------
        public static bool Update(JournalsOwnersModel JournalsOwnersObj)
        {
            return JournalsOwnersSqlDataPrvider.Instance.Update(JournalsOwnersObj);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Delete--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Deletes single model object .
        /// </summary>
        /// <param name="JournalOwner">The model id.</param>
        /// <returns>The result of delete operation.</returns>
        //--------------------------------------------------------------------
        public static bool Delete(int JournalOwner)
        {
            return JournalsOwnersSqlDataPrvider.Instance.Delete(JournalOwner);
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
            JournalsOwnersSqlDataPrvider.Instance.DeleteGroupofObjects(ids);
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
        public static List<JournalsOwnersModel> Get()
        {
            return JournalsOwnersSqlDataPrvider.Instance.Get(-1);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------GetPageByPage--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets the spesific records.
        /// </summary>
        //--------------------------------------------------------------------
        public static List<JournalsOwnersModel> GetPageByPage(int pageIndex, int pageSize,string sortname, string sortorder, string qtype, string query, out int totalRecords)
        {
            return JournalsOwnersSqlDataPrvider.Instance.GetPageByPage(pageIndex, pageSize, sortname,  sortorder,  qtype,  query, out totalRecords);
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
        public static JournalsOwnersModel GetObject(int JournalOwner)
        {
            JournalsOwnersModel JournalsOwnersObj = null;
            List<JournalsOwnersModel> list = JournalsOwnersSqlDataPrvider.Instance.Get(JournalOwner);
            if (list != null && list.Count > 0)
            {
                JournalsOwnersObj = list[0];
            }
            return JournalsOwnersObj;
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
    }
}