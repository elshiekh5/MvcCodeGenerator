using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml;
using System.Xml.Schema;
using System.IO;
namespace CmsTeamSmallLibrary.XML
{
    public class XmlManager
    {
        static XmlReaderSettings settings;
        
        #region --------------BuildSchemaSettings--------------
        //------------------------------------------------------------------
        //BuildSchemaSettings
        //------------------------------------------------------------------
        public static XmlReaderSettings BuildSchemaSettings(string targetNameSpace, string schemaUri)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add(targetNameSpace, schemaUri);
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);
            return settings;

        }
        //------------------------------------------------------------------
        #endregion

        #region --------------CheckXMLContentValidation--------------
        //------------------------------------------------------------------
        //CheckXMLContentValidation
        //------------------------------------------------------------------
        public static bool CheckXMLContentValidation(string xmlContent, string targetNameSpace, string schemaUri)
        {
            try
            {
                if (settings == null) { settings = BuildSchemaSettings(targetNameSpace, schemaUri); }
                using (XmlReader reader = XmlReader.Create(new StringReader(xmlContent), settings))
                {
                    // Parse the file. 
                    while (reader.Read()) ;


                }
                return true;
            }
            catch
            {
                return false;
                //Console.WriteLine(ex.Message);
            }
        }
        //------------------------------------------------------------------
        #endregion

        #region --------------ValidationCallBack--------------
        //------------------------------------------------------------------
        //ValidationCallBack
        //------------------------------------------------------------------
        private static void ValidationCallBack(object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
                Console.WriteLine("\tWarning: Matching schema not found.  No validation occurred." + args.Message);
            else
                Console.WriteLine("\tValidation error: " + args.Message);

        }
        //------------------------------------------------------------------
        #endregion
    }
}
