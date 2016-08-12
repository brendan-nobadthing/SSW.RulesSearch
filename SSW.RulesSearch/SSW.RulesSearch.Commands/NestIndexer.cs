using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Serilog;
using SSW.RulesSearch.Data.SharePoint;
using SSW.RulesSearch.Models;

namespace SSW.RulesSearch.Commands
{
    public class NestIndexer : ICommand
    {
        private readonly ElasticClient _elasticClient;
        private readonly IRulesDataSource _rulesDataSource;

        public NestIndexer(ElasticClient elasticClient, 
            IRulesDataSource rulesDataSource)
        {
            _elasticClient = elasticClient;
            _rulesDataSource = rulesDataSource;
        }

        public void Run()
        {
            var toIndex = _rulesDataSource.GetAllRules();
            Log.Information("indexing...");
            _elasticClient.IndexMany(toIndex, "rules");
            _elasticClient.Refresh("rules");
            Log.Information("index complete");
        }
    }
}
