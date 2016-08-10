using System;
using Autofac;

namespace SSW.RulesSearch.Lucene.IntTests
{
    public class AutofacContainerFactory
    {

        public static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new LuceneModule(ConfigSettings.LuceneSettings));
       
            return builder.Build();
        }
    }
}
