//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option or rebuild the Visual Studio project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Web.Application.StronglyTypedResourceProxyBuilder", "11.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Common {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Common() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Resources.Common", global::System.Reflection.Assembly.Load("App_GlobalResources"));
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to تمت إضافة عنصر جديد بإسم .
        /// </summary>
        internal static string Adding_Done {
            get {
                return ResourceManager.GetString("Adding_Done", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to فشلت عملية الإضافة.
        /// </summary>
        internal static string Adding_Failed {
            get {
                return ResourceManager.GetString("Adding_Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to تم حذف .
        /// </summary>
        internal static string Deleting_Done {
            get {
                return ResourceManager.GetString("Deleting_Done", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to فشلت عملية الحذف.
        /// </summary>
        internal static string Deleting_Failed {
            get {
                return ResourceManager.GetString("Deleting_Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to تم تعديل بيانات .
        /// </summary>
        internal static string Updating_Done {
            get {
                return ResourceManager.GetString("Updating_Done", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to فشلت عملية التعديل.
        /// </summary>
        internal static string Updating_Failed {
            get {
                return ResourceManager.GetString("Updating_Failed", resourceCulture);
            }
        }
    }
}
