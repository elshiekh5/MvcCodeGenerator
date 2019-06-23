using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace CmsTeamSmallLibrary
{
    public class AppConfig
    {
        public static NameValueCollection AppSettings;
        static AppConfig()
        {
            AppSettings = System.Configuration.ConfigurationManager.AppSettings;
        }
    }
}
