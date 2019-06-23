using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmsTeamSmallLibrary.IO;
using System.IO;
namespace SMOSampleInConsol
{
    class ControllerBuilder : TableFileBuilder
    {
        

        StringBuilder FlexiGridColumns = new StringBuilder();
        StringBuilder ParametersToCreateFlexiJson = new StringBuilder();
        StringBuilder ForignkeysCode = new StringBuilder();

        public ControllerBuilder(CustomTable t)
        {
            CurrentTable = t;
            init();
        }
        private void init()
        {
            ///////////////////////////////////////////////////////////////////////
            //Calculate flexigrid column width
            int flexigridColumns = CurrentTable.Columns.Count;
            if(CurrentTable.ID!=null)
            {
                --flexigridColumns;
            }
            int columnWidth = (ResourcesParameters.FlexiGridWidth  / flexigridColumns);
            ///////////////////////////////////////////////////////////////////////
            string flwxiGridColumnsPattern = "\t\t\t\tnew FlexigridColumn(\"{0}\",\"{0}\",{1},true,\"left\",false,false)";
            string ParametersToCreateFlexiJsonPattern = "\t\t\t\t\t\t\t\t\t\t\t\t\t\tx.{0}";
            //string forignkeysCodePatern = "\t\t\t\tViewData[\"{0}\"] = {1}.{2}().Select(x => new SelectListItem { Text = x.{3}, Value = x.{4}.ToString() }).ToList();";

            CustomColumn c = null;
            for (int i = 0; i < CurrentTable.Columns.Count-1; i++)
            {
                c = CurrentTable.Columns[i];
                if (!c.InPrimaryKey)
                {
                    c = CurrentTable.Columns[i];
                    FlexiGridColumns.AppendFormat(flwxiGridColumnsPattern, new string[] { c.ProgramatlyName, columnWidth.ToString() });
                    FlexiGridColumns.Append(",\n");

                    if (c.DotNetType == typeof(DateTime))
                    {
                        ParametersToCreateFlexiJson.AppendFormat(ParametersToCreateFlexiJsonPattern + ".ToString(\"MM/dd/yyyy\")", new string[] { c.ProgramatlyName });
                    }
                    else
                    {
                        ParametersToCreateFlexiJson.AppendFormat(ParametersToCreateFlexiJsonPattern, new string[] { c.ProgramatlyName });
                    }

                    ParametersToCreateFlexiJson.Append(",\n");
                }
                
            }
            c = CurrentTable.Columns[CurrentTable.Columns.Count - 1];
            FlexiGridColumns.AppendFormat(flwxiGridColumnsPattern, new string[] { c.ProgramatlyName, columnWidth.ToString() });
            if (c.DotNetType == typeof(DateTime))
            {
                ParametersToCreateFlexiJson.AppendFormat(ParametersToCreateFlexiJsonPattern + ".ToString(\"MM/dd/yyyy\")", new string[] { c.ProgramatlyName });
            }
            else
            {
                ParametersToCreateFlexiJson.AppendFormat(ParametersToCreateFlexiJsonPattern, new string[] { c.ProgramatlyName });
            }

            //-----------------------------------------------------------------------------------------------------------------
            CustomTable tableOfRelation=null;
            string columnOFTextFeild = "";
            string forignkyText = "";
            string primaryKeyText = "";
            ForeignKeyColumn rc = null;
            //-------------------------------
            foreach (ForeignKey r in CurrentTable.dbObject.ForeignKeys)
            {
                tableOfRelation = null;
                 columnOFTextFeild = "";
                 forignkyText = "";
                 primaryKeyText = "";
                 rc = null;
                if (r != null)
                {
                    rc = r.Columns[0];
                    forignkyText = Globals.GetProgramatlyName(rc.Name);
                    primaryKeyText = Globals.GetProgramatlyName(rc.ReferencedColumn);
                    tableOfRelation = ProjectBuilder.db.GetTable(r.ReferencedTable);
                    columnOFTextFeild = tableOfRelation.FirstStringColumnName;
                    ForignkeysCode.Append("\n\t\t\tViewData[\"" + forignkyText + "\"] = " + tableOfRelation.FactoryClass + "." + tableOfRelation.MethodGet + "().Select(x => new SelectListItem { Text = x." + columnOFTextFeild + ", Value = x." + primaryKeyText + ".ToString() }).ToList();");
                }
            }
            //------------------------------------------------------------------------
        }

        public static void Create(CustomTable t)
        {
            ControllerBuilder sqlProvider = new ControllerBuilder(t);

            string templatefile = FileManager.ReadingTextFile(AppDomain.CurrentDomain.BaseDirectory + "Resources/Classes/Controller.cs");

            string genratedCode = sqlProvider.Prepare(templatefile);

            FileManager.SaveFile(".cs", sqlProvider.CurrentTable.PathOfControllerClass, genratedCode);

        }
        public new string Prepare(string temp )
        {
            StringBuilder codes = new StringBuilder(base.Prepare(temp));
            codes.Replace(ResourcesParameters.ForignkeysCode, ForignkeysCode.ToString());
            codes.Replace(ResourcesParameters.FlexiGridColumns, FlexiGridColumns.ToString());
            codes.Replace(ResourcesParameters.ParametersToCreateFlexiJson, ParametersToCreateFlexiJson.ToString());
            
            return codes.ToString();
        }
        
         
    }
    
}
