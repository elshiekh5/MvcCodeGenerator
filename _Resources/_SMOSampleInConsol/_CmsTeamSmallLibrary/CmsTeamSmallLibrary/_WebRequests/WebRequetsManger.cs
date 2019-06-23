using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace CmsTeamSmallLibrary
{
    public class WebRequetsManger
    {
        #region --------------CreateWebRequest--------------
        //------------------------------------------------------------------
        //CreateWebRequest
        //------------------------------------------------------------------
        public static string CreateWebRequest(string Address, string PackagePath, string UserName, string Password)
        {
            HttpWebRequest request = WebRequest.Create(Address) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/zip";

            string authInfo = UserName + ":" + Password;
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));

            request.Headers["Authorization"] = "Basic " + authInfo;
            request.Headers["Content-Disposition"] = "filename=" + PackagePath;
            request.Headers["X-Packaging"] = "http://purl.org/net/sword-types/METSDSpaceSIP";
            request.Headers["X-No-Op"] = "false";
            request.Headers["X-Verbose"] = "true";

            byte[] byteArray = File.ReadAllBytes(PackagePath);
            request.ContentLength = byteArray.Length;

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            //Console.WriteLine("Article: " + PackagePath + " " + response.StatusCode);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseFromServer;
        }
        //------------------------------------------------------------------
        #endregion


        #region --------------CheckWebUrlIsExists--------------
        //------------------------------------------------------------------
        //CheckWebUrlIsExists
        //------------------------------------------------------------------
        public static bool CheckWebUrlIsExists(string Address , out string status)
        {
            status = HttpStatusCode.NotFound.ToString();
            bool result;
            try
            {
                //------------------------------------------------------------------
                if (string.IsNullOrEmpty(Address)) return false;
                //------------------------------------------------------------------
                Uri urlCheck = new Uri(Address);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlCheck);
                request.Method = "HEAD";
                //request.Timeout = 15000;
                HttpWebResponse response;
                response = (HttpWebResponse)request.GetResponse();
                //------------------------------------------------------------------
                status = response.StatusCode.ToString();
                result = response.StatusCode == HttpStatusCode.OK;
                //------------------------------------------------------------------
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    HttpStatusCode xxx = response.StatusCode;
                }
            }
            catch (Exception ex)
            {
                result =  false; //could not connect to the internet (maybe) 
                status = ex.Message;
            }
            return result;
        }
        //------------------------------------------------------------------
        #endregion

    }
}
