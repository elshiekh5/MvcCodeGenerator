using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using IndexersCP4.Models;

namespace IndexersCP4
{   
    /// <summary>
    /// The factory class of IndexersWithJournalsModel.
    /// </summary>
    public class IndexersWithJournalsFactor
    {

        #region --------------Create--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Creates model object by calling News data provider create method.
        /// </summary>
        /// <param name="IndexersWithJournalsObj">The model object.</param>
        /// <returns>The result of create operation.</returns>
        //--------------------------------------------------------------------
        public static bool Create(IndexersWithJournalsModel IndexersWithJournalsObj)
        {
            return IndexersWithJournalsSqlDataPrvider.Instance.Create(IndexersWithJournalsObj);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Update--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Updates model object by calling sql data provider update method.
        /// </summary>
        /// <param name="IndexersWithJournalsObj">The model object.</param>
        /// <returns>The result of update operation.</returns>
        //--------------------------------------------------------------------
        public static bool Update(IndexersWithJournalsModel IndexersWithJournalsObj)
        {
            return IndexersWithJournalsSqlDataPrvider.Instance.Update(IndexersWithJournalsObj);
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
            return IndexersWithJournalsSqlDataPrvider.Instance.Delete(ID);
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
            IndexersWithJournalsSqlDataPrvider.Instance.DeleteGroupofObjects(ids);
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
        public static List<IndexersWithJournalsModel> Get()
        {
            return IndexersWithJournalsSqlDataPrvider.Instance.Get(-1);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------GetPageByPage--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets the spesific records.
        /// </summary>
        //--------------------------------------------------------------------
        public static List<IndexersWithJournalsModel> GetPageByPage(int pageIndex, int pageSize,string sortname, string sortorder, string qtype, string query, out int totalRecords)
        {
            return IndexersWithJournalsSqlDataPrvider.Instance.GetPageByPage(pageIndex, pageSize, sortname,  sortorder,  qtype,  query, out totalRecords);
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
        public static IndexersWithJournalsModel GetObject(int ID)
        {
            IndexersWithJournalsModel IndexersWithJournalsObj = null;
            List<IndexersWithJournalsModel> list = IndexersWithJournalsSqlDataPrvider.Instance.Get(ID);
            if (list != null && list.Count > 0)
            {
                IndexersWithJournalsObj = list[0];
            }
            return IndexersWithJournalsObj;
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
    }
}