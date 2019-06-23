using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace CmsTeamSmallLibrary
{
    public class RemoteCredentials
    {
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool LogonUser(string
        lpszUsername, string lpszDomain, string lpszPassword,
        int dwLogonType, int dwLogonProvider, ref
IntPtr phToken);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private unsafe static extern int FormatMessage(int
        dwFlags, ref IntPtr lpSource,
        int dwMessageId, int dwLanguageId, ref String
        lpBuffer, int nSize, IntPtr* arguments);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto,
        SetLastError = true)]
        private static extern bool CloseHandle(IntPtr handle
        );

        [DllImport("advapi32.dll", CharSet = CharSet.Auto,
        SetLastError = true)]
        public extern static bool DuplicateToken(IntPtr
        existingTokenHandle,
        int SECURITY_IMPERSONATION_LEVEL, ref IntPtr
        duplicateTokenHandle);


        // logon types
        const int LOGON32_LOGON_INTERACTIVE = 2;
        const int LOGON32_LOGON_NETWORK = 3;
        const int LOGON32_LOGON_NEW_CREDENTIALS = 9;

        // logon providers
        const int LOGON32_PROVIDER_DEFAULT = 0;
        const int LOGON32_PROVIDER_WINNT50 = 3;
        const int LOGON32_PROVIDER_WINNT40 = 2;
        const int LOGON32_PROVIDER_WINNT35 = 1;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static WindowsImpersonationContext OpenImpersonation(string UserName, string Password, string Domain, ref IntPtr token)
        {
            token = IntPtr.Zero;
            IntPtr dupToken = IntPtr.Zero;

            bool isSuccess = LogonUser(UserName, Domain, Password, LOGON32_LOGON_NEW_CREDENTIALS, LOGON32_PROVIDER_DEFAULT, ref token);

            if (!isSuccess)
            {
                RaiseLastError();
            }

            isSuccess = DuplicateToken(token, 2, ref dupToken);
            if (!isSuccess)
            {
                RaiseLastError();
            }

            WindowsIdentity newIdentity = new WindowsIdentity(dupToken);
            return newIdentity.Impersonate();
        }

        public static void CloseImpersonation(WindowsImpersonationContext impersonatedUser, IntPtr token)
        {
            impersonatedUser.Undo();


            bool isSuccess = CloseHandle(token);
            if (!isSuccess)
            {
                RaiseLastError();
            }
        }


        // GetErrorMessage formats and returns an error message
        // corresponding to the input errorCode.
        public unsafe static string GetErrorMessage(int
        errorCode)
        {
            int FORMAT_MESSAGE_ALLOCATE_BUFFER =
            0x00000100;
            int FORMAT_MESSAGE_IGNORE_INSERTS =
            0x00000200;
            int FORMAT_MESSAGE_FROM_SYSTEM = 0x00001000;

            int messageSize = 255;
            string lpMsgBuf = "";
            int dwFlags = FORMAT_MESSAGE_ALLOCATE_BUFFER |
            FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS;

            IntPtr ptrlpSource = IntPtr.Zero;
            IntPtr ptrArguments = IntPtr.Zero;

            int retVal = FormatMessage(dwFlags, ref
ptrlpSource, errorCode, 0, ref lpMsgBuf, messageSize, &ptrArguments);
            if (retVal == 0)
            {
                throw new ApplicationException(
                string.Format("Failed to format message for error code '{0}'.", errorCode));
            }

            return lpMsgBuf;
        }

        private static void RaiseLastError()
        {
            int errorCode = Marshal.GetLastWin32Error();
            string errorMessage = GetErrorMessage(
            errorCode);

            throw new ApplicationException(errorMessage
            );
        }
    }
}
