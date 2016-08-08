using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSW.RulesSearchService
{
    public static class ConfigSettings
    {

        public static string SeqUrl => System.Configuration.ConfigurationManager.AppSettings["SeqUrl"];
  
        public static string RulesIndexPath => System.Configuration.ConfigurationManager.AppSettings["RulesIndexPath"];

        public static string SharePointUrl => System.Configuration.ConfigurationManager.AppSettings["SharePointUrl"];
    }
}
