using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using SSW.RulesSearch.Data.SharePoint;
using SSW.RulesSearch.Lucene;
using SSW.RulesSearch.Models;

namespace SSW.RulesSearch.Commands
{
    public class IndexAllRules
    {

        private readonly IIndexer<Rule> _ruleIndexer;
        private readonly IRulesDataSource _rulesDataSource;

        public IndexAllRules(IIndexer<Rule> ruleIndexer, 
            IRulesDataSource rulesDataSource)
        {
            _ruleIndexer = ruleIndexer;
            _rulesDataSource = rulesDataSource;
        }

        public void Run()
        {
            foreach (var rule in _rulesDataSource.GetAllRules())
            {
                _ruleIndexer.Index(rule);
            }
            _ruleIndexer.Commit();
            Log.Information("{countIndexed} rules indexed", _ruleIndexer.GetItemCount());
        }
    }
}

