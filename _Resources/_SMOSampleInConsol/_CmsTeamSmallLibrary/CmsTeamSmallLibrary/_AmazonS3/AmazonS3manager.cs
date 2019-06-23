using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Configuration;
using Amazon.S3;
using Amazon;
using Amazon.S3.Model;
using System.IO;

namespace CmsTeamSmallLibrary.AmazonS3NameSpace
{

   public class AmazonS3Manager
    {
        #region --------------S3Client--------------
        private AmazonS3 _S3Client = null;
        public AmazonS3 S3Client
        {
            get { return _S3Client; }
            set { _S3Client = value; }
        }
        //------------------------------------------
        #endregion

        public AmazonS3Manager()
        {
            string AWSAccessKey = AppConfig.AppSettings["AWSAccessKey"];
            string AWSSecretKey = AppConfig.AppSettings["AWSSecretKey"];
            GetS3Client(AWSAccessKey, AWSSecretKey);
        }


        public AmazonS3Manager(string AWSAccessKey, string AWSSecretKey)
        {
            GetS3Client(AWSAccessKey, AWSSecretKey);
        }



        #region --------------GetS3Client--------------
        /// <summary>
        /// Create AmazonS3 from key in Application Config.
        /// </summary>
        /// <returns S3Client></returns>
        public void GetS3Client(string AWSAccessKey, string AWSSecretKey)
        {
            _S3Client = AWSClientFactory.CreateAmazonS3Client(AWSAccessKey, AWSSecretKey);
        }
        //------------------------------------------
        #endregion

        #region --------------BucketMethods--------------

        #region --------------CreateBucket--------------
        /// <summary>
        /// Creates bucket if it is not exists.
        /// </summary>
        /// <param name="S3Client"></param>
        private void CreateBucket(string BucketName)
        {
            ListBucketsResponse response = S3Client.ListBuckets();
            bool found = false;
            foreach (S3Bucket bucket in response.Buckets)
            {
                if (bucket.BucketName == BucketName)
                {
                    found = true;
                    break;
                }
            }
            if (found == false)
            {
                S3Client.PutBucket(new PutBucketRequest().WithBucketName(BucketName));
            }
        }
        //------------------------------------------
        #endregion

        #region --------------GetAllBucket--------------
        private void GetAllBucket()
        {
            ListBucketsResponse response = S3Client.ListBuckets();
        }
        //------------------------------------------
        #endregion
        #endregion

        #region --------------FolderMethods--------------

        #region --------------DoesFolderExist--------------
        public bool DoesFolderExist(string bucketName, string folderName)
        {
            ListObjectsRequest request = new ListObjectsRequest();
            request.BucketName = bucketName;
            request.WithPrefix(folderName + "/");
            request.MaxKeys = 1;

            using (ListObjectsResponse response = S3Client.ListObjects(request))
            {
                return (response.S3Objects.Count > 0);
            }
        }
        #endregion

        #region --------------CreateNewFolder--------------
        public void CreateNewFolder(string BucketName, string folderFullPath)
        {
            PutObjectRequest request = new PutObjectRequest();
            request.WithBucketName(BucketName);
            request.WithKey(folderFullPath);
            request.WithContentBody("");
            S3Client.PutObject(request);
        }
        //------------------------------------------
        #endregion
        #endregion

        #region --------------FileMethods--------------


        #region --------------DoesFileExist--------------
        public bool DoesFileExist(string bucketName, string filefullPath)
        {
            ListObjectsRequest request = new ListObjectsRequest();
            request.BucketName = bucketName;
            request.WithPrefix(filefullPath);
            request.MaxKeys = 1;
            using (ListObjectsResponse response = S3Client.ListObjects(request))
            {
                return (response.S3Objects.Count > 0);
            }
        }
        #endregion
        /*
        #region --------------CreateNewFileInFolder--------------
        public void CreateNewFileInFolder(string filefullPath, string fileContent)
        {
           // String S3_KEY = "Els/" + "Demo Create File.txt";
            PutObjectRequest request = new PutObjectRequest();
            request.WithBucketName(BucketName);
            request.WithKey(filefullPath);
            request.WithContentBody(fileContent);
            S3Client.PutObject(request);
        }
        //------------------------------------------
        #endregion
        */

        #region --------------CreateNewFile--------------
        public void CreateNewFile(string BucketName, string filefullPath, string fileContent)
        {
            PutObjectRequest request = new PutObjectRequest();
            request.WithBucketName(BucketName);
            request.WithKey(filefullPath);
            if (!string.IsNullOrEmpty(fileContent))
            { request.WithContentBody(fileContent); }
            S3Client.PutObject(request);
        }
        //------------------------------------------
        #endregion

        #region --------------UploadFile--------------
        public void UploadFile(string BucketName, string filefullPath)
        {
            //S3_KEY is name of file we want upload
            PutObjectRequest request = new PutObjectRequest();
            request.WithBucketName(BucketName);
            request.WithKey(filefullPath);
            //request.WithInputStream(MemoryStream);
            request.WithFilePath("");
            S3Client.PutObject(request);
        }
        //------------------------------------------
        #endregion

        #region --------------GetFile--------------
        public MemoryStream GetFile(string BucketName, string filefullPath)
        {
            using (S3Client)
            {
                MemoryStream file = new MemoryStream();
                try
                {
                    GetObjectResponse r = S3Client.GetObject(new GetObjectRequest()
                    {
                        BucketName = BucketName,
                        Key = filefullPath
                    });
                    try
                    {
                        long transferred = 0L;
                        BufferedStream stream2 = new BufferedStream(r.ResponseStream);
                        byte[] buffer = new byte[0x2000];
                        int count = 0;
                        while ((count = stream2.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            file.Write(buffer, 0, count);
                        }
                    }
                    finally
                    {
                    }
                    return file;
                }
                catch (AmazonS3Exception)
                {
                    //Show exception
                }
            }
            return null;
        }
        //------------------------------------------
        #endregion

        #region --------------DeleteFile--------------
        public void DeleteFile(string BucketName, string filefullPath)
        {
            DeleteObjectRequest request = new DeleteObjectRequest()
            {
                BucketName = BucketName,
                Key = filefullPath
            };
            S3Response response = S3Client.DeleteObject(request);
        }
        //------------------------------------------
        #endregion

        #region --------------CopyFile--------------
        public void CopyFile(string SourceBucket, String sourcePath, string DestinationBucket, String destinationPath)
        {
            // String destinationPath = "Els2/temp.txt";
            //SourceKey = "Els/" + "Demo Create File.txt",
            CopyObjectRequest request = new CopyObjectRequest()
            {
                SourceBucket = SourceBucket,
                SourceKey = sourcePath,
                DestinationBucket = DestinationBucket,
                DestinationKey = destinationPath
            };
            CopyObjectResponse response = S3Client.CopyObject(request);
        }
        //------------------------------------------
        #endregion

        #region --------------ShareFile--------------
        public void ShareFile(string BucketName, string filefullPath)
        {
            S3Response response1 = S3Client.SetACL(new SetACLRequest()
            {
                CannedACL = S3CannedACL.PublicRead,
                BucketName = BucketName,
                Key = filefullPath
            });
        }
        //------------------------------------------
        #endregion

        #region --------------MakeUrl--------------
        public String MakeUrl(string BucketName, string filefullPath)
        {
            string preSignedURL = S3Client.GetPreSignedURL(new GetPreSignedUrlRequest()
            {
                BucketName = BucketName,
                Key = filefullPath,
                Expires = System.DateTime.Now.AddMinutes(30)

            });

            return preSignedURL;
        }
        //------------------------------------------
        #endregion

        #region --------------ExistsOldCode--------------
        /*
        public bool Exists(string filefullPath, string bucketName)
        {
            try
            {
                S3Response response = S3Client.GetObjectMetadata(new GetObjectMetadataRequest()
                   .WithBucketName(bucketName)
                   .WithKey(filefullPath));

                return true;
            }

            catch (Amazon.S3.AmazonS3Exception ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return false;
                //status wasn't not found, so throw the exception
                throw;
            }
        }
        //------------------------------------------
         */
        #endregion

        #endregion




    }
}
