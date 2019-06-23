using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Configuration;
using CmsTeamSmallLibrary;
using System.Windows.Forms;

namespace CmsTeamSmallLibrary.Log
{

    public class LogManager
    {
        public static string ErrorLogFile = Application.StartupPath + "\\Logs\\" + "ErrorLog.txt";
        public static string DonelogFile = Application.StartupPath + "\\Logs\\" + "DoneFiles.txt";

        public static void LogError(string Message, string StackTrace, string FileName, string JournalCode, int VolumeNumber, string Pii, string ToolName)
        {
            //try
            //{
            string FileType = FileName.Substring(FileName.LastIndexOf("."));
            //GetErrors.MissedArticles_ErrorLog(Message, StackTrace, FileName, JournalCode, VolumeNumber, Pii, FileType, ToolName);
            //}
            //catch
            //{
            TextWriter tr = new StreamWriter(ErrorLogFile, true, Encoding.UTF8);
            tr.WriteLine(JournalCode + "\t" + Pii + "\t" + VolumeNumber + "\t" + Message + "\t" + FileName + "\t\n");
            tr.Dispose();
            //}        
        }
        public static void LogStatement(string statement)
        {

            TextWriter tr = new StreamWriter(DonelogFile, true, Encoding.UTF8);
            tr.WriteLine(statement);
            tr.Dispose();
        }
        public static void LogError(string source, string msg)
        {

            TextWriter tr = new StreamWriter(ErrorLogFile, true, Encoding.UTF8);
            tr.WriteLine(source + ":\t" + msg + "\t");
            tr.Dispose();
        }
        public static void Writerecord(string statement)
        {

            TextWriter tr = new StreamWriter("Logs\\" + "records" + ConfigurationManager.AppSettings["volume"] + "_" + ".txt", true, Encoding.UTF8);
            tr.Write(statement);
            tr.Dispose();
        }

    }
}
