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
            FactoryBuilder factory = new FactoryBuilder(t);

            string FactoryFile = FileManager.ReadingTextFile(AppDomain.CurrentDomain.BaseDirectory + "Resources/Classes/Factory.cs");

            string genFactoryFile = factory.Prepare(FactoryFile);

            FileManager.SaveFile(".cs", factory.CurrentTable.PathOfFactoryClass, genFactoryFile);

        }
       
    }
    
}
