using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Nest;

namespace SSW.RulesSearch.Elastic
{
    public class ElasticSearchModule : Module
    {
        private readonly ElasticSearchSettings _elasticSearchSettings;

        public ElasticSearchModule(ElasticSearchSettings elasticSearchSettings)
        {
            _elasticSearchSettings = elasticSearchSettings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(new ElasticClient(new Uri(_elasticSearchSettings.Url)))
                .AsSelf();
        }
    }
}
