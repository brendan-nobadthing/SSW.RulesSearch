using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using SSW.RulesSearch.Domain;

namespace SSW.RulesSearch.Lucene
{
    public class LuceneModule : Module
    {

        private readonly LuceneSettings _luceneSettings;

        public LuceneModule(LuceneSettings luceneSettings)
        {
            _luceneSettings = luceneSettings;
        }

        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterInstance(_luceneSettings)
                .AsSelf();

            builder.RegisterType<LuceneContext>()
                .AsSelf()
                .SingleInstance();

            builder.RegisterGeneric(typeof(LuceneIndexer<>)).As(typeof(IIndexer<>));
            builder.RegisterType<RuleDocumentBuilder>().As<IDocumentBuilder<Rule>>();

           

        }
    }
}
