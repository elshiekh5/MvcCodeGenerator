using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.OleDb;
using System.Data;

namespace CmsTeamSmallLibrary.Excel
{
    public class ExcelFactor
    {
        #region ---------------CreateExcelFile---------------
        //-----------------------------------------------
        //CreateExcelFile
        //-----------------------------------------------
        public static void CreateExcelFile(string excelFilePath, ArrayList ArticlesNos)
        {
            Microsoft.Office.Interop.Excel.Application xlApp;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Cells[1, 1] = "Article ID";
            for (int i = 0; i < ArticlesNos.Count; i++)
            {
                xlWorkSheet.Cells[i + 2, 1] = ArticlesNos[i];
            }
            xlWorkBook.SaveAs(excelFilePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            ReleaseObject(xlWorkSheet);
            ReleaseObject(xlWorkBook);
            ReleaseObject(xlApp);
        }
        //-----------------------------------------
        #endregion

        #region ---------------ReleaseObject---------------
        //-----------------------------------------------
        //ReleaseObject
        //-----------------------------------------------
        private static void ReleaseObject(object obj)
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

        #region ---------------LoadRowsFromExcellFile---------------
        //-----------------------------------------------
        //LoadRowsFromExcellFile
        //-----------------------------------------------
        public static ArrayList LoadRowsFromExcellFile(string filePath, int columnsCount, List<string> errorList)
        {
            List<string> excelRow = null;
            ArrayList allRows = new ArrayList();
            try
            {
                String sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + filePath + ";" + "Extended Properties=Excel 8.0;";

                // Create connection object by using the preceding connection string.
                OleDbConnection objConn = new OleDbConnection(sConnectionString);

                // Open connection with the database.
                objConn.Open();

                // The code to follow uses a SQL SELECT command to display the data from the worksheet.

                // Create new OleDbCommand to return data from worksheet.
                OleDbCommand objCmdSelect = new OleDbCommand("SELECT * FROM [Sheet1$]", objConn);

                // Create new OleDbDataAdapter that is used to build a DataSet
                // based on the preceding SQL SELECT statement.
                OleDbDataAdapter objAdapter1 = new OleDbDataAdapter();

                // Pass the Select command to the adapter.
                objAdapter1.SelectCommand = objCmdSelect;

                // Create new DataSet to hold information from the worksheet.
                DataSet objDataset1 = new DataSet();

                // Fill the DataSet with the information from the worksheet.
                objAdapter1.Fill(objDataset1, "XLData");
                //---------------------------------------------------------------------------------------------
                for (int i = 0; i < objDataset1.Tables[0].Rows.Count; i++)
                {
                    try
                    {
                        excelRow = new List<string>();
                        for (int b = 0; b < columnsCount; b++)
                        {
                            excelRow.Add(objDataset1.Tables[0].Rows[i].ItemArray[b].ToString());
                        }
                        allRows.Add(excelRow);
                    }
                    catch (Exception ex2)
                    {
                        errorList.Add(objDataset1.Tables[0].Rows[i].ItemArray[0].ToString());
                    }
                }
                objConn.Close();
                //---------------------------------------------------------------------------------------------
                //---------------------------------------------------------------------------------------------
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return allRows;
        }
        //-----------------------------------------
        #endregion

        #region ---------------LoadArticleListFromExcellFile---------------
        //-----------------------------------------------
        //LoadArticleListFromExcellFile
        //-----------------------------------------------
        protected void LoadArticleListFromExcellFile(string filePath, List<int> articlesList, List<string> errorList)
        {
            try
            {
                String sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + filePath + ";" + "Extended Properties=Excel 8.0;";

                // Create connection object by using the preceding connection string.
                OleDbConnection objConn = new OleDbConnection(sConnectionString);

                // Open connection with the database.
                objConn.Open();

                // The code to follow uses a SQL SELECT command to display the data from the worksheet.

                // Create new OleDbCommand to return data from worksheet.
                OleDbCommand objCmdSelect = new OleDbCommand("SELECT * FROM [Sheet1$]", objConn);

                // Create new OleDbDataAdapter that is used to build a DataSet
                // based on the preceding SQL SELECT statement.
                OleDbDataAdapter objAdapter1 = new OleDbDataAdapter();

                // Pass the Select command to the adapter.
                objAdapter1.SelectCommand = objCmdSelect;

                // Create new DataSet to hold information from the worksheet.
                DataSet objDataset1 = new DataSet();

                // Fill the DataSet with the information from the worksheet.
                objAdapter1.Fill(objDataset1, "XLData");
                //---------------------------------------------------------------------
                int articleID;
                  //---------------------------------------------------------------------
                for (int i = 0; i < objDataset1.Tables[0].Rows.Count; i++)
                {
                    try
                    {
                        articleID = Convert.ToInt32(objDataset1.Tables[0].Rows[i].ItemArray[0]);
                        articlesList.Add(articleID);
                        
                    }
                    catch (Exception ex2)
                    {
                        errorList.Add(objDataset1.Tables[0].Rows[i].ItemArray[0].ToString());
                    }


                }
                objConn.Close();
                //---------------------------------------------------------------------------------------------
                //---------------------------------------------------------------------------------------------
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        //-----------------------------------------
        #endregion

        #region ---------------CreateExcelFileFromTextFile---------------
        //-----------------------------------------------
        //LoadRowsFromExcellFile
        //-----------------------------------------------
        public static void CreateExcelFileFromTextFile(string textFilePath,string excelFilePath)
        {
            try
            {
                ExcelFile excelFile = new ExcelFile();
                /* ExcelLogFile.WorkSheet.Cells[1, 1] = "Items";
                 ExcelLogFile.WorkSheet.Cells[1, 2] = "JournalCode";
                 ExcelLogFile.WorkSheet.Cells[1, 3] = "Volume";
                 ExcelLogFile.WorkSheet.Cells[1, 4] = "Message";
                 return ExcelLogFile;
                   ++IndexOfRow;
             ExcelLogFile.WorkSheet.Cells[IndexOfRow, 1] = pii;
             ExcelLogFile.WorkSheet.Cells[IndexOfRow, 2] = JournalCode;
             ExcelLogFile.WorkSheet.Cells[IndexOfRow, 3] = volume;
             ExcelLogFile.WorkSheet.Cells[IndexOfRow, 4] = message;
                 */
                string fileContents = CmsTeamSmallLibrary.IO.FileManager.ReadingTextFile(textFilePath);
                string[] lines=fileContents.Split(new char[] { '\n' });
                int lineNo = 0;
                foreach (string line in lines)
                {
                   excelFile.WorkSheet.Cells[++lineNo, 1] = line;
                }
                excelFile.SaveAsAndClose(excelFilePath);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        //-----------------------------------------
        #endregion

        #region /*--------- InitializeExcelLogFile ---------*\
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //--------------------------------------------------------------------
        public static ExcelFile CreateFileAndInitializeHeaders(List<string> headers)
        {
            ExcelFile ExcelLogFile = new ExcelFile();
            for (int i = 0; i < headers.Count; i++)
            {
                ExcelLogFile.WorkSheet.Cells[1, i + 1] = headers[i];
                
            }
            return ExcelLogFile;
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region --------------LogInSuccessededExcelFile--------------
        //------------------------------------------------------------------------------------------------------
        //LogError
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        ///
        /// </summary>
        //------------------------------------------------------------------------------------------------------
        private static void LogInExcelFile(ExcelFile ExcelLogFile, ref int IndexOfRow, List<string> cellsValues)
        {

            ++IndexOfRow;
            for (int i = 0; i < cellsValues.Count; i++)
            {
                ExcelLogFile.WorkSheet.Cells[IndexOfRow, i + 1] = cellsValues[i];

            }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
    }
}
