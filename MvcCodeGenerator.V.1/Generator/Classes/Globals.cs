using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMOSampleInConsol.IO;

namespace SMOSampleInConsol
{
    class TableFileBuilder
    {
        string templatePath = "";

        #region --------------CurrentTable--------------
        //------------------------------------------------------------------------------------------------------
        //CurrentTable
        //--------------------------------------------------------------------
        private CustomTable _CurrentTable;
        /// <summary>
        /// Gets or sets entity CurrentTable. 
        /// </summary>
        public CustomTable CurrentTable
        {
            get { return _CurrentTable; }
            set { _CurrentTable=value; }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion
        public TableFileBuilder()
        {
        }
        /*public TableFileBuilder(CustomTable t)
        {
            _CurrentTable = t;
            init();
        }*/
        public void init()
        {
           
        }

        public static void Create(CustomTable t)
        {

        }
        public string Prepare(string temp)
        {
            StringBuilder parameters = new StringBuilder(temp);
            parameters.Replace(ResourcesParameters.NameSpace, ProjectBuilder.NameSpace);
            parameters.Replace(ResourcesParameters.TableName, CurrentTable.ProgramatlyName);
            parameters.Replace(ResourcesParameters.SqlDataPrviderClass, CurrentTable.SqlDataPrviderClass);
            parameters.Replace(ResourcesParameters.ModelClass, CurrentTable.ModelClass);
            parameters.Replace(ResourcesParameters.FactoryClass, CurrentTable.FactoryClass);
            parameters.Replace(ResourcesParameters.ControllerClass, CurrentTable.ControllerClass);
            parameters.Replace(ResourcesParameters.ModelObject, CurrentTable.ModelObject);
            parameters.Replace(ResourcesParameters.ModelList, CurrentTable.ModelList);

            parameters.Replace(ResourcesParameters.ProcedureInsert, CurrentTable.ProcedureInsert);
            parameters.Replace(ResourcesParameters.ProcedureUpdate, CurrentTable.ProcedureUpdate);
            parameters.Replace(ResourcesParameters.ProcedureUpdate, CurrentTable.ProcedureUpdate);
            parameters.Replace(ResourcesParameters.ProcedureDelete, CurrentTable.ProcedureDelete);
            parameters.Replace(ResourcesParameters.ProcedureGetPageByPage, CurrentTable.ProcedureGetPageByPage);
            parameters.Replace(ResourcesParameters.ProcedureGet, CurrentTable.ProcedureGet);

            parameters.Replace(ResourcesParameters.MethodSave, CurrentTable.MethodSave);
            parameters.Replace(ResourcesParameters.MethodCreate, CurrentTable.MethodCreate);
            parameters.Replace(ResourcesParameters.MethodUpdat, CurrentTable.MethodUpdat);
            parameters.Replace(ResourcesParameters.MethodDelete, CurrentTable.MethodDelete);
            parameters.Replace(ResourcesParameters.DeleteGroupofObjects, CurrentTable.DeleteGroupofObjects);
            parameters.Replace(ResourcesParameters.MethodGetPageByPage, CurrentTable.MethodGetPageByPage);
            parameters.Replace(ResourcesParameters.MethodGet, CurrentTable.MethodGet);
            parameters.Replace(ResourcesParameters.MethodGetObject, CurrentTable.MethodGetObject);

            parameters.Replace(ResourcesParameters.ModelID, CurrentTable.ModelID);
            parameters.Replace(ResourcesParameters.ModelIdDotNetType, CurrentTable.ModelIdDotNetType);
            parameters.Replace(ResourcesParameters.ModelIDNullValue, CurrentTable.ModelIDNullValue);

            parameters.Replace(ResourcesParameters.FirstStringColumnName, CurrentTable.FirstStringColumnName);
            parameters.Replace(ResourcesParameters.FlexiGridWidthParameter, ResourcesParameters.FlexiGridWidth.ToString());
            parameters.Replace(ResourcesParameters.FlixyGridID, CurrentTable.FlixyGridID);
            parameters.Replace(ResourcesParameters.MethodCreateFlexiJson, CurrentTable.MethodCreateFlexiJson);
            parameters.Replace(ResourcesParameters.DialogBox, CurrentTable.DialogBox);
            parameters.Replace(ResourcesParameters.DialogBoxFormID, CurrentTable.DialogBoxFormID);
            parameters.Replace(ResourcesParameters.JsObject, CurrentTable.JsObject);
            parameters.Replace(ResourcesParameters.JsForm, CurrentTable.JsForm);
            parameters.Replace(ResourcesParameters.DialogBoxTitle, CurrentTable.DialogBoxTitle);
            //parameters.Replace(ResourcesParameters.XXXXXXXXXXXX, CurrentTable.XXXXXXXXXXXX);
            //parameters.Replace(ResourcesParameters.XXXXXXXXXXXX, CurrentTable.XXXXXXXXXXXX);
            //parameters.Replace(ResourcesParameters.XXXXXXXXXXXX, CurrentTable.XXXXXXXXXXXX);

            //parameters.Replace(ResourcesParameters.DynamicParameters, DynamicParameters.ToString());
            //parameters.Replace(ResourcesParameters.DynamicPopulatedParameters, DynamicPopulatedParameters.ToString());
            return parameters.ToString();
        }
    }
}
