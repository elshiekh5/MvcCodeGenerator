using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMOSampleInConsol.IO;
using System.IO;
namespace SMOSampleInConsol
{
    class ModelBuilder : TableFileBuilder
    {
        

        //StringBuilder DynamicParameters = new StringBuilder();
        //StringBuilder DynamicPopulatedParameters = new StringBuilder();
        StringBuilder DynamicProperties = new StringBuilder();

        public ModelBuilder(CustomTable t)
        {
            CurrentTable = t;
            init();
        }
        private void init()
        {
            //string parametterPattern = "\t\t\t\tmyCommand.Parameters.Add(\"@{0}\", SqlDbType.{1}, {2}).Value = {3}.{0};\n";
            //string populatePropPattern = "\n\t\t\t//------------------------------------------------";
            //populatePropPattern += "\n\t\t\t//[{2}]";
            //populatePropPattern += "\n\t\t\t//------------------------------------------------";
            //populatePropPattern += "\n\t\t\tif (reader[\"{0}\"] != DBNull.Value)";
            //populatePropPattern += "\n\t\t\t    {1}.{2} = ({3})reader[\"{0}\"];";
            //populatePropPattern += "\n\t\t\t//------------------------------------------------";
            string optionalprifex = "";
            foreach (CustomColumn c in CurrentTable.Columns)
            {
                optionalprifex = "";
                //[Key]
                //[DataType(DataType.Password)]
                //[Required]
                //[Display(Name = "User name")]
                //if (c.InPrimaryKey)
                //{
                //    DynamicProperties.Append("\n\t\t[Key]");
                //}
                if (c.InPrimaryKey && c.IsIdentity)
                {
                    optionalprifex = "?";
                }
                else if (!c.IsNullable)
                {
                    DynamicProperties.Append("\n\t\t[Required]");
                }
                if (c.DataTypeAttr != System.ComponentModel.DataAnnotations.DataType.Text)
                {
                    DynamicProperties.AppendFormat("\n\t\t[DataType(DataType.{0})]", c.DataTypeAttr);
                }
                if (c.HasDefaultValue)
                {
                    DynamicProperties.Append("\n\t\t[Editable(false, AllowInitialValue = true)]");
                }
                if (c.SqlDbType == System.Data.SqlDbType.Char)
                {
                    DynamicProperties.AppendFormat("\n\t\t[StringLength({0})]", c.MaxLength);
                }
                else if (c.SqlDbType == System.Data.SqlDbType.NChar)
                {
                    DynamicProperties.AppendFormat("\n\t\t[StringLength({0})]", (c.MaxLength/2));
                }
                DynamicProperties.AppendFormat("\n\t\t[Display(Name = \"{0}\")]" ,c.ProgramatlyName);
                DynamicProperties.Append("\n\t\tpublic " + c.DotNetTypeAlias + optionalprifex + " " + c.ProgramatlyName + " { get; set; }\n");
                if (!c.InPrimaryKey)
                {
                    //DynamicParameters.AppendFormat(parametterPattern, new string[] { c.ProgramatlyName, c.SqlDbTypeString, c.MaxLength.ToString(), CurrentTable.ModelObject });
                    //DynamicPopulatedParameters.AppendFormat(populatePropPattern, c.NameInDatabase, CurrentTable.ModelObject, c.ProgramatlyName, c.DotNetTypeAlias);
                    //DynamicProperties.AppendFormat(prop, c.DotNetType, CurrentTable.ProgramatlyName);
                }
            }
        }

        public static void Create(CustomTable t)
        {
            ModelBuilder sqlProvider = new ModelBuilder(t);

            string EntityFile = FileManager.ReadingTextFile(AppDomain.CurrentDomain.BaseDirectory + "Resources/Classes/Entity.cs");

            string genEntityFile = sqlProvider.Prepare(EntityFile);

            FileManager.SaveFile(".cs", sqlProvider.CurrentTable.PathOfModelClass, genEntityFile);


        }
        public new string Prepare(string temp )
        {
            StringBuilder codes = new StringBuilder(base.Prepare(temp));
            //codes.Replace(ResourcesParameters.DynamicParameters, DynamicParameters.ToString());
            //codes.Replace(ResourcesParameters.DynamicPopulatedParameters, DynamicPopulatedParameters.ToString());
            codes.Replace(ResourcesParameters.DynamicProperties, DynamicProperties.ToString());
            return codes.ToString();
        }
    }
}
