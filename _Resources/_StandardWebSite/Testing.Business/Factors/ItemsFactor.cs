using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using News.DAL;
using News.Models;

namespace News.Business.Factors
{   
    /// <summary>
    /// The factory class of ItemsModel.
    /// </summary>
    public class ItemsFactor
    {
        #region --------------Save--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Save model data.
        /// </summary>
        /// <param name="ItemsObj">The model object.</param>
        /// <returns>The result of Saving operation.</returns>
        //--------------------------------------------------------------------
        public static bool Save(ItemsModel ItemsObj)
        {
            if (ItemsObj.ItemID > 0)
            {
                return ItemsSqlDataPrvider.Instance.Update(ItemsObj);
            }
            else
            {
                return ItemsSqlDataPrvider.Instance.Create(ItemsObj);
            }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Create--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Creates model object by calling News data provider create method.
        /// </summary>
        /// <param name="ItemsObj">The model object.</param>
        /// <returns>The result of create operation.</returns>
        //--------------------------------------------------------------------
        public static bool Create(ItemsModel ItemsObj)
        {
            return ItemsSqlDataPrvider.Instance.Create(ItemsObj);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Update--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Updates model object by calling sql data provider update method.
        /// </summary>
        /// <param name="ItemsObj">The model object.</param>
        /// <returns>The result of update operation.</returns>
        //--------------------------------------------------------------------
        public static bool Update(ItemsModel ItemsObj)
        {
            return ItemsSqlDataPrvider.Instance.Update(ItemsObj);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Delete--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Deletes single model object .
        /// </summary>
        /// <param name="ItemID">The model id.</param>
        /// <returns>The result of delete operation.</returns>
        //--------------------------------------------------------------------
        public static bool Delete(int ItemID)
        {
            return ItemsSqlDataPrvider.Instance.Delete(ItemID);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------DeleteGroupofobjects--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Deletes multible objects .
        /// </summary>
        /// <param name="ids">The array of ids.</param>
        /// <returns>The result of delete operation.</returns>
        //--------------------------------------------------------------------
        public static void DeleteGroupofobjects(int[] ids)
        {
             ItemsSqlDataPrvider.Instance.DeleteGroupofobjects(ids);
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
        public static List<ItemsModel> Get()
        {
            return ItemsSqlDataPrvider.Instance.Get(-1);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------GetPageByPage--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets the spesific records.
        /// </summary>
        /// <param name="pageIndex">current page</param>
        /// <param name="pageSize">numbere of items per page</param>
        /// <param name="totalRecords">total records found into database</param>
        /// <returns></returns>
        //--------------------------------------------------------------------
        public static List<ItemsModel> GetPageByPage(int pageIndex, int pageSize,string sortname, string sortorder, string qtype, string query, out int totalRecords)
        {
            return ItemsSqlDataPrvider.Instance.GetPageByPage(pageIndex, pageSize, sortname,  sortorder,  qtype,  query, out totalRecords);
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
        public static ItemsModel GetObject(int id)
        {
            ItemsModel itemsObj = null;
            List<ItemsModel> itemsList = ItemsSqlDataPrvider.Instance.Get(id);
            if (itemsList != null && itemsList.Count > 0)
            {
                itemsObj = itemsList[0];
            }
            return itemsObj;
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
    }
}