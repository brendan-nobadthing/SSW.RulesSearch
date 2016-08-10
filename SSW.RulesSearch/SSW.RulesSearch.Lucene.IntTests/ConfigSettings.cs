using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSW.RulesSearch.Lucene.IntTests
{
    public class ConfigSettings
    {

        public static LuceneSettings LuceneSettings => new LuceneSettings()
        {
            IndexDirectory = System.Configuration.ConfigurationManager.AppSettings["RulesIndexPath"]
        };

    }
}
