using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using SSW.RulesSearch.Commands;
using SSW.RulesSearch.Data.SharePoint;
using SSW.RulesSearch.Elastic;
using SSW.RulesSearch.Lucene;

namespace SSW.RulesSearch.ConsoleRunner
{
    public static class AutofacContainerFactory
    {

        public static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new LuceneModule(new LuceneSettings()
            {
                IndexDirectory = ConfigSettings.RulesIndexPath
            }));

            builder.RegisterModule(new SharePointModule(new SharePointClientConfig()
            {
                Url = ConfigSettings.SharePointUrl
            }));

            builder.RegisterModule(new ElasticSearchModule(new ElasticSearchSettings()
            {
                Url = ConfigSettings.ElasticSearchUrl
            }));

            builder.RegisterType<LuceneIndexer>()
                .Named<ICommand>(typeof(LuceneIndexer).Name)
                .As<ICommand>();

            builder.RegisterType<NestIndexer>()
               .Named<ICommand>(typeof(NestIndexer).Name)
               .As<ICommand>();



            return builder.Build();
        }
    }
}
