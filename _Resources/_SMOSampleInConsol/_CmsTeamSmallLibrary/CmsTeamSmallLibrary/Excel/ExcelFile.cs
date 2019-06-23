using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;

namespace CmsTeamSmallLibrary.Excel
{
    public class ExcelFile
    {
        #region --------------Appilcation--------------
        //------------------------------------------------------------------------------------------------------
        //xlAppilcation
        //--------------------------------------------------------------------
        private Microsoft.Office.Interop.Excel.Application _Appilcation;
        /// <summary>
        /// Gets or sets entity xlAppilcation. 
        /// </summary>
        public Microsoft.Office.Interop.Excel.Application Appilcation
        {
            get { return _Appilcation; }
            set { _Appilcation = value; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------xlAppilcation--------------
        //------------------------------------------------------------------------------------------------------
        //xlAppilcation
        //--------------------------------------------------------------------
        private Microsoft.Office.Interop.Excel.Workbook _WorkBook;
        /// <summary>
        /// Gets or sets entity xlAppilcation. 
        /// </summary>
        public Microsoft.Office.Interop.Excel.Workbook WorkBook
        {
            get { return _WorkBook; }
            set { _WorkBook = value; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------WorkSheet--------------
        //------------------------------------------------------------------------------------------------------
        //WorkSheet
        //--------------------------------------------------------------------
        private Microsoft.Office.Interop.Excel.Worksheet _WorkSheet;
        /// <summary>
        /// Gets or sets entity WorkSheet. 
        /// </summary>
        public Microsoft.Office.Interop.Excel.Worksheet WorkSheet
        {
            get { return _WorkSheet; }
            set { _WorkSheet = value; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------MisValue--------------
        //------------------------------------------------------------------------------------------------------
        //MisValue
        //--------------------------------------------------------------------
        private object _MisValue;
        /// <summary>
        /// Gets or sets entity MisValue. 
        /// </summary>
        public object MisValue
        {
            get { return _MisValue; }
            set { _MisValue = value; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion


        #region /*--------- ExcelFile ---------*\
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //--------------------------------------------------------------------
        public ExcelFile()
        {
            Appilcation = new Microsoft.Office.Interop.Excel.ApplicationClass();
            MisValue = System.Reflection.Missing.Value;
            WorkBook = Appilcation.Workbooks.Add(MisValue);
            WorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)WorkBook.Worksheets.get_Item(1);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
        #region /*--------- SaveAs ---------*\
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //--------------------------------------------------------------------
        public void SaveAs(string excelFilePath)
        {
           // excelFilePath = "c:\\loglog.xls";
            WorkBook.SaveAs(excelFilePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, MisValue, MisValue, MisValue, MisValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, MisValue, MisValue, MisValue, MisValue, MisValue);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
        #region /*--------- SaveAsAndClose ---------*\
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //--------------------------------------------------------------------
        public void SaveAsAndClose(string excelFilePath)
        {
            SaveAs(excelFilePath);
            Close();
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
        #region /*--------- Close ---------*\
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //--------------------------------------------------------------------
        public void Close()
        {
            WorkBook.Close(true, MisValue, MisValue);
            Appilcation.Quit();
            ReleaseObject(WorkSheet);
            ReleaseObject(WorkBook);
            ReleaseObject(Appilcation);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
        #region ---------------ReleaseObject---------------
        //-----------------------------------------------
        //ReleaseObject
        //-----------------------------------------------
        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }
        //-----------------------------------------
        #endregion
    }
}
