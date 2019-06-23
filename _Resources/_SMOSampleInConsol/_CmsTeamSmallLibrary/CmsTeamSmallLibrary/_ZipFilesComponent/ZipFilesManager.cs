using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ionic.Zip;
using System.IO;

namespace CmsTeamSmallLibrary
{
    public class ZipFilesManager
    {
        //--------------------------------------------------------------------------------------
        public static ZipFile CreateZipFile(string fileName, string filePath)
        {
            ZipFile zipFile = new ZipFile();
            return CreateZipFile( zipFile,  fileName,  filePath);
        }
        //--------------------------------------------------------------------------------------
        public static ZipFile CreateZipFile(ZipFile zipFile, string fileName, string filePath)
        {
            using (zipFile)
            {
                FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                zipFile.AddEntry(fileName, fileStream);
            }
            return zipFile;
        }
        //--------------------------------------------------------------------------------------
        public static void SaveZipFile(ZipFile zipFile, string zipFilePath)
        {
            zipFile.Save(zipFilePath);
        }
        //--------------------------------------------------------------------------------------
        public static void CreateZipFileAndSaveIt(string fileName, string filePath, string zipFilePath)
        {
            ZipFile zipFile = new ZipFile();
            CreateZipFileAndSaveIt(zipFile, fileName, filePath, zipFilePath);
        }
        //--------------------------------------------------------------------------------------
        public static void CreateZipFileAndSaveIt(ZipFile zipFile, string fileName, string filePath, string zipFilePath)
        {
            using (zipFile)
            {
                FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                zipFile.AddEntry(fileName, fileStream);
            }
            SaveZipFile(zipFile, zipFilePath);
        }
        //--------------------------------------------------------------------------------------
    }
}
