using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using SSW.RulesSearch.Commands;
using SSW.RulesSearch.Data.SharePoint;
using SSW.RulesSearch.Lucene;

namespace SSW.RulesSearchService
{
    public static class SearchServiceContainerFactory
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

            builder.RegisterType<IndexAllRules>().AsSelf();

            return builder.Build();
        }

    }
}