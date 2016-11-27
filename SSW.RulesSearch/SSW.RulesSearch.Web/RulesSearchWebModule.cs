using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using SSW.RulesSearch.Commands;
using SSW.RulesSearch.Data.SharePoint;
using SSW.RulesSearch.Elastic;
using SSW.RulesSearch.Lucene;

namespace SSW.RulesSearch.Web
{
    public class RulesSearchWebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
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


           // builder.RegisterType<SimpleTextSearch>()
            //   .AsSelf()
            //   .AsImplementedInterfaces();

            builder.RegisterType<ElasticTextSearch>()
                .AsSelf()
               .AsImplementedInterfaces();
        }
    }
}