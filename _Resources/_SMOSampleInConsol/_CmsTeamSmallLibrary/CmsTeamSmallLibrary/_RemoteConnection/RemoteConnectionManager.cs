using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace CmsTeamSmallLibrary
{
    public class RemoteConnectionManager
    {
        public static IntPtr token = IntPtr.Zero;
        public static WindowsImpersonationContext obj;
        public static void OpenImpersonation(string sourceFolder)
        {
            token = IntPtr.Zero;
            obj =
            RemoteCredentials.OpenImpersonation("originator",
            "owqiuqrp97fy3w0fj94rkheno70t98dsk23ulq90ug02d98e57wq98g7yr34krt08uwi9bgy305n9",
                        sourceFolder, ref token);
        }

        public static void CloseImpersonation()
        {
            //Reading from remote location goes here
            RemoteCredentials.CloseImpersonation(obj, token);
        }
    }
}
