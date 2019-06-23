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
    class SqlProviderBuilder : TableFileBuilder
    {

        StringBuilder DynamicParametersForCreate = new StringBuilder();
        StringBuilder DynamicParametersForUpdate = new StringBuilder();
        StringBuilder DynamicPopulatedParameters = new StringBuilder();
        StringBuilder DynamicReturnParameters = new StringBuilder();
        StringBuilder IDParameter = new StringBuilder();
        StringBuilder IDParameterDeceration = new StringBuilder();
        
        public SqlProviderBuilder()
        {
        }
        public SqlProviderBuilder(CustomTable t)
        {
            CurrentTable = t;
            init();
        }
        private new void init()
        {
            string parametterPattern1 = "\t\t\t\tmyCommand.Parameters.Add(\"@{0}\", SqlDbType.{1}, {2}).Value = {3}.{0};\n";
            string parametterPattern2 = "\t\t\t\tmyCommand.Parameters.Add(\"@{0}\", SqlDbType.{1}).Value = {2}.{0};\n";
            string primaryKeyParametterPattern = "\t\t\t\tmyCommand.Parameters.Add(\"@{0}\", SqlDbType.{1}, {2}).Direction = ParameterDirection.Output;\n";
            string returnParametterPattern = "\t\t\t\t{0}.{1} = (int)myCommand.Parameters[\"@{1}\"].Value;\n";

            string idParameterPattern1 = "myCommand.Parameters.Add(\"@{0}\", SqlDbType.{1}, {2}).Value = {0};\n";
            string idParameterPattern2 = "myCommand.Parameters.Add(\"@{0}\", SqlDbType.{1}).Value = {0};\n";

            string idParameterDecerationPattern1 = "myCommand.Parameters.Add(\"@{0}\", SqlDbType.{1}, {2});\n";
            string idParameterDecerationPattern2 = "myCommand.Parameters.Add(\"@{0}\", SqlDbType.{1});\n";

            string populatePropPattern = "\n\t\t\t//------------------------------------------------";
            populatePropPattern += "\n\t\t\t//[{2}]";
            populatePropPattern += "\n\t\t\t//------------------------------------------------";
            populatePropPattern += "\n\t\t\tif (reader[\"{0}\"] != DBNull.Value)";
            populatePropPattern += "\n\t\t\t    {1}.{2} = ({3})reader[\"{0}\"];";
            populatePropPattern += "\n\t\t\t//------------------------------------------------";

            foreach (CustomColumn c in CurrentTable.Columns)
            {
                if (c.InPrimaryKey && c.IsIdentity)
                {
                    DynamicParametersForCreate.AppendFormat(primaryKeyParametterPattern, new string[] { c.ProgramatlyName, c.SqlDbTypeString, c.MaxLength.ToString(), CurrentTable.ModelObject });
                    DynamicParametersForUpdate.AppendFormat(parametterPattern1, new string[] { c.ProgramatlyName, c.SqlDbTypeString, c.MaxLength.ToString(), CurrentTable.ModelObject });
                    DynamicReturnParameters.AppendFormat(returnParametterPattern, new string[] { CurrentTable.ModelObject, c.ProgramatlyName });
               
                }
                else
                {
                    if (c.MaxLength > 0)
                    {
                        DynamicParametersForCreate.AppendFormat(parametterPattern1, new string[] { c.ProgramatlyName, c.SqlDbTypeString, c.MaxLength.ToString(), CurrentTable.ModelObject });
                        DynamicParametersForUpdate.AppendFormat(parametterPattern1, new string[] { c.ProgramatlyName, c.SqlDbTypeString, c.MaxLength.ToString(), CurrentTable.ModelObject });

                    }
                    else
                    {
                        DynamicParametersForCreate.AppendFormat(parametterPattern2, new string[] { c.ProgramatlyName, c.SqlDbTypeString, CurrentTable.ModelObject });
                        DynamicParametersForUpdate.AppendFormat(parametterPattern2, new string[] { c.ProgramatlyName, c.SqlDbTypeString, CurrentTable.ModelObject });
                    }
                }
                DynamicPopulatedParameters.AppendFormat(populatePropPattern, c.NameInDatabase, CurrentTable.ModelObject, c.ProgramatlyName, c.DotNetTypeAlias);

            }
            if (CurrentTable.ID != null)
            {
                if (CurrentTable.ID.MaxLength > 0)
                {
                    if (CurrentTable.ID.DotNetType == typeof(int) ||
                        CurrentTable.ID.DotNetType == typeof(short) ||
                        CurrentTable.ID.DotNetType == typeof(long))
                    {
                        IDParameter.Append("if(" + CurrentTable.ID.ProgramatlyName + ">0)");
                    }
                    else 
                    {
                        IDParameter.Append("if(!string.IsNullOrEmpty(" + CurrentTable.ID.ProgramatlyName + "))");
                    }
                        IDParameter.AppendFormat(idParameterPattern1, new string[] { CurrentTable.ID.ProgramatlyName, CurrentTable.ID.SqlDbTypeString, CurrentTable.ID.MaxLength.ToString() });
                        
                    IDParameterDeceration.AppendFormat(idParameterDecerationPattern1, new string[] { CurrentTable.ID.ProgramatlyName, CurrentTable.ID.SqlDbTypeString, CurrentTable.ID.MaxLength.ToString() });
                }
                else
                {
                    IDParameter.AppendFormat(idParameterPattern2, new string[] { CurrentTable.ID.ProgramatlyName, CurrentTable.ID.SqlDbTypeString });
                    
                    IDParameterDeceration.AppendFormat(idParameterDecerationPattern2, new string[] { CurrentTable.ID.ProgramatlyName, CurrentTable.ID.SqlDbTypeString });
                }
            }
        }

        public new static void Create(CustomTable t)
        {
            SqlProviderBuilder sqlProvider = new SqlProviderBuilder(t);

            string SqlDataProviderFile = FileManager.ReadingTextFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Classes\\SqlDataProvider.cs");

            string genSqlDataProviderFile = sqlProvider.Prepare(SqlDataProviderFile);

            FileManager.SaveFile(".cs", sqlProvider.CurrentTable.PathOfSqlDataPrviderClass, genSqlDataProviderFile);

        }
        public new string Prepare(string temp )
        {
            StringBuilder codes = new StringBuilder(base.Prepare(temp));
            codes.Replace(ResourcesParameters.DynamicParametersForCreate, DynamicParametersForCreate.ToString());
            codes.Replace(ResourcesParameters.DynamicParametersForUpdate, DynamicParametersForUpdate.ToString());
            codes.Replace(ResourcesParameters.DynamicReturnParameters, DynamicReturnParameters.ToString());
            codes.Replace(ResourcesParameters.IDParameter, IDParameter.ToString());
            codes.Replace(ResourcesParameters.IDParameterDeceration, IDParameterDeceration.ToString());
            codes.Replace(ResourcesParameters.DynamicPopulatedParameters, DynamicPopulatedParameters.ToString());
            return codes.ToString();
        }
        
         
    }
    
}
