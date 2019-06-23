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

        #region --------------Updat--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Updates model object by calling sql data provider update method.
        /// </summary>
        /// <param name="ItemsObj">The model object.</param>
        /// <returns>The result of update operation.</returns>
        //--------------------------------------------------------------------
        public static bool Updat(ItemsModel ItemsObj)
        {
            return ItemsSqlDataPrvider.Instance.Updat(ItemsObj);
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
        public static List<ItemsModel> GetPageByPage(int pageIndex, int pageSize, out int totalRecords)
        {
            return ItemsSqlDataPrvider.Instance.GetPageByPage(pageIndex, pageSize, out totalRecords);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Get--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets the spesific records.
        /// </summary>
        /// <param name="ItemID">The model id.</param>
        /// <returns>Model object.</returns>
        //--------------------------------------------------------------------
        public static List<ItemsModel> Get(int ItemID)
        {
            return ItemsSqlDataPrvider.Instance.Get(ItemID);
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
        public static ItemsModel GetObject(int ID)
        {
            ItemsModel itemsObj = null;
            List<ItemsModel> itemsList = ItemsSqlDataPrvider.Instance.Get(ID);
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