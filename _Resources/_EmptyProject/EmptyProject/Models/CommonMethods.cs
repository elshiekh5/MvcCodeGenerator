using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Security.Principal;
using System.Threading;
using System.IO;
using System.Text;

    public class CommonMethods
    {
        //public static string UserName;
        //public static string UserEmail;
        //public static int? RoleID;
        //public static int UserID;
        public static CommonMethods instance
        {

            get
            {
                return new CommonMethods();
            }
        }





          public static string ExtractName(string username)
          {
              int indexOfAttSign=username.IndexOf('@');

              if (indexOfAttSign > -1)
              {
                  username = username.Remove(indexOfAttSign, (username.Length - indexOfAttSign));
              }
              //string username = 
              if (username == null)
                  return "";
              string Output = username.Replace(".", " ");
              try
              {
                  Output = Output.Substring(0, 1).ToUpper() + Output.Substring(1);
              }
              catch { }
              int SecondNameIndex = Output.IndexOf(" ") + 1;
              if (SecondNameIndex > 1)
              {
                  Output = Output.Substring(0, SecondNameIndex)
                      + Output.Substring(SecondNameIndex, 1).ToUpper()
                      + Output.Substring(SecondNameIndex + 1);
              }

              if (Output.Contains("qa"))
              {
                  Output = Output.Replace("Qa", "QA");

              }
              return Output;
          }
          public static string ReadFile(string FileName)
          {
              string Path;
              Path = HttpContext.Current.Server.MapPath("~/MailTemplates/") + FileName;
              StreamReader streamReader = new StreamReader(Path, Encoding.GetEncoding(1250));
              string Content = streamReader.ReadToEnd();
              streamReader.Close();
              return Content;
          }

          public static bool IsIEClientWebBrowser()
          {
              bool falg = false;
              if (HttpContext.Current.Request.Browser.Type.IndexOf("IE") >= 0)
              {
                  if (HttpContext.Current.Request.Browser.MajorVersion > 8)
                      falg = false;
                  else
                      falg = true;
              }
              else
                  falg = false;

              return falg;
          }
    }
