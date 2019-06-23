using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestingWebsite.App_Code
{
    public class Common
    {
        public static string GetTempData(object obj)
        {
            if (obj != null)
            {
                return (string)obj;
            }
            else
            {
                return "";
            }
        }
    }
}