using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Portfolio.Models;

namespace Portfolio
{   
    /// <summary>
    /// The factory class of BusinessSectionsModel.
    /// </summary>
    public class BusinessSectionsFactor
    {

        #region --------------Create--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Creates model object by calling News data provider create method.
        /// </summary>
        /// <param name="BusinessSectionsObj">The model object.</param>
        /// <returns>The result of create operation.</returns>
        //--------------------------------------------------------------------
        public static bool Create(BusinessSectionsModel BusinessSectionsObj)
        {
            return BusinessSectionsSqlDataPrvider.Instance.Create(BusinessSectionsObj);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Update--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Updates model object by calling sql data provider update method.
        /// </summary>
        /// <param name="BusinessSectionsObj">The model object.</param>
        /// <returns>The result of update operation.</returns>
        //--------------------------------------------------------------------
        public static bool Update(BusinessSectionsModel BusinessSectionsObj)
        {
            return BusinessSectionsSqlDataPrvider.Instance.Update(BusinessSectionsObj);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------Delete--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Deletes single model object .
        /// </summary>
        /// <param name="BusinessSectionID">The model id.</param>
        /// <returns>The result of delete operation.</returns>
        //--------------------------------------------------------------------
        public static bool Delete(int BusinessSectionID)
        {
            return BusinessSectionsSqlDataPrvider.Instance.Delete(BusinessSectionID);
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
            BusinessSectionsSqlDataPrvider.Instance.DeleteGroupofObjects(ids);
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
        public static List<BusinessSectionsModel> Get()
        {
            return BusinessSectionsSqlDataPrvider.Instance.Get(-1);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------GetPageByPage--------------
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets the spesific records.
        /// </summary>
        //--------------------------------------------------------------------
        public static List<BusinessSectionsModel> GetPageByPage(int pageIndex, int pageSize,string sortname, string sortorder, string qtype, string query, out int totalRecords)
        {
            return BusinessSectionsSqlDataPrvider.Instance.GetPageByPage(pageIndex, pageSize, sortname,  sortorder,  qtype,  query, out totalRecords);
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
        public static BusinessSectionsModel GetObject(int BusinessSectionID)
        {
            BusinessSectionsModel BusinessSectionsObj = null;
            List<BusinessSectionsModel> list = BusinessSectionsSqlDataPrvider.Instance.Get(BusinessSectionID);
            if (list != null && list.Count > 0)
            {
                BusinessSectionsObj = list[0];
            }
            return BusinessSectionsObj;
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
    }
}