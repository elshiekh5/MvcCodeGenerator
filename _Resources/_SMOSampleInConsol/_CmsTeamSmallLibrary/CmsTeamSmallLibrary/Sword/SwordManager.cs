using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using CmsTeamSmallLibrary.XML;
using Hindawi.OnlinePlatform.BLL.FromDb;
using Ionic.Zip;

namespace CmsTeamSmallLibrary.Sword
{
    public class SwordManager
    {
        #region --------------SendFilesBySword--------------
        //------------------------------------------------------------------
        //SendFilesBySword
        //------------------------------------------------------------------
        public static void SendFilesBySword(List<Article> articlesList, string OwnerName, string SentPackagesFolder, string OriginalDirectoryPath, string OriginalFilePathPatren, string targetNameSpace, string schemaUri, string ErrorLogFile)
        {
            if (!Directory.Exists(SentPackagesFolder)) { Directory.CreateDirectory(SentPackagesFolder); }
            if (File.Exists(SentPackagesFolder)) { File.Delete(ErrorLogFile); }
            //------------------------------------------------------------------
            //Open remote connection with local server
            //------------------------------------------------------------------
            RemoteConnectionManager.OpenImpersonation(OriginalDirectoryPath);
            //------------------------------------------------------------------
            string DistenationFileName = "";
            string DistenationPdfFileName = "";
            string DistenationXmlMetaDataFileName = "";
            string DistenationZipPackagePath = "";
            //------------------------------------------------------------------
            string DistenationMetsXmlFileName = "";
            string DistenationMetsXmlFileContents = "";
            //------------------------------------------------------------------
            string SourceFilePath = "";
            string SourcePdfFilPath = "";
            string SourceXmlMetaDataFilePath = "";
            //------------------------------------------------------------------
            foreach (Article currentArticle in articlesList)
            {
                try
                {
                    if (currentArticle.ArticleId != "197514")
                    {
                        continue;
                    }
                    //------------------------------------------------------------------
                    DistenationFileName = BuildFileName(currentArticle);
                    DistenationPdfFileName = DistenationFileName + ".pdf";
                    DistenationXmlMetaDataFileName = DistenationFileName + ".xml";
                    DistenationMetsXmlFileName = "mets.xml";
                    DistenationZipPackagePath = DistenationFileName + ".zip";
                    //------------------------------------------------------------------
                    SourceFilePath = OriginalDirectoryPath + string.Format(OriginalFilePathPatren, currentArticle.VolumeNumber.ToString(), currentArticle.JournalCode, currentArticle.Pii.ToString(), currentArticle.PublicationYear);
                    SourcePdfFilPath = SourceFilePath + ".pdf";
                    SourceXmlMetaDataFilePath = SourceFilePath + ".xml";
                    //------------------------------------------------------------------
                    //Create Mets.xml contents
                    DistenationMetsXmlFileContents = SwordXmlManager.BuildMetaDataXmlContents(currentArticle, OwnerName, DistenationPdfFileName);
                    //------------------------------------------------------------------
                   // if (true)
                    if (XmlManager.CheckXMLContentValidation(DistenationMetsXmlFileContents, targetNameSpace, schemaUri))
                    {
                        //------------------------------------------------------------------------------------------
                        //Create zip package
                        //------------------------------------------------------------------------------------------
                        //Add pdf file
                        ZipFile zipFile = ZipFilesManager.CreateZipFile(DistenationPdfFileName, SourcePdfFilPath);
                        //Add xml file
                        zipFile = ZipFilesManager.CreateZipFile(zipFile, DistenationXmlMetaDataFileName, SourceXmlMetaDataFilePath);
                        //Add mets file
                        zipFile.AddEntry(DistenationMetsXmlFileName, DistenationMetsXmlFileContents);
                        //Save package file
                        ZipFilesManager.SaveZipFile(zipFile, DistenationZipPackagePath);
                        //------------------------------------------------------------------------------------------
                        //string RsponseFromServer = WebRequetsManger.CreateWebRequest(AppSettings.RepositoryAddress, DistenationZipPackagePath);
                        File.Move(DistenationZipPackagePath, "SentPackages/" + DistenationZipPackagePath);
                        //------------------------------------------------------------------------------------------
                    }
                    else
                    {
                        throw new Exception("Invalid XML");
                    }
                    //------------------------------------------------------------------
                }
                catch (Exception ex)
                {
                    LogError(ErrorLogFile, currentArticle.JournalCode.ToString() + " _ " + currentArticle.PublicationYear.ToString() + " _ " + currentArticle.Pii.ToString(), ex.Message);
                }
            }
            //---------RemoteConnectionManager---------------------------------------------------------
            //close remote connection with local server
            //------------------------------------------------------------------
            RemoteConnectionManager.CloseImpersonation();
            //------------------------------------------------------------------
        }

        //------------------------------------------------------------------
        #endregion

        #region --------------BuildFileName--------------
        //------------------------------------------------------------------
        //BuildFileName
        //------------------------------------------------------------------
        private static string BuildFileName(Article articles)
        {
            return articles.JournalCode + "." + articles.PublicationYear + "." + articles.Pii;
        }
        //------------------------------------------------------------------
        #endregion

        #region --------------LogError--------------
        //------------------------------------------------------------------
        //LogError
        //------------------------------------------------------------------
        public static void LogError(string ErrorLogFile, string source, string ErrMessage)
        {
            TextWriter tr = new StreamWriter(ErrorLogFile, true, Encoding.UTF8);
            tr.WriteLine(source + "\n");
            tr.WriteLine(ErrMessage + "\n");
            tr.WriteLine("--------------------------------------------------------------------------------------------------------------------\n");
            tr.Dispose();
        }
        //------------------------------------------------------------------
        #endregion
    }
}