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
    class FactoryBuilder : TableFileBuilder
    {

        public FactoryBuilder()
        {
        }

        public FactoryBuilder(CustomTable t)
        {
            CurrentTable = t;
            init();
        }

        public static void Create(CustomTable t)
        {
            FactoryBuilder sqlProvider = new FactoryBuilder(t);

            string FactoryFile = FileManager.ReadingTextFile(AppDomain.CurrentDomain.BaseDirectory + "Resources/Classes/Factory.cs");

            string genFactoryFile = sqlProvider.Prepare(FactoryFile);

            FileManager.SaveFile(".cs", sqlProvider.CurrentTable.PathOfFactoryClass, genFactoryFile);

        }
       
    }
    
}
