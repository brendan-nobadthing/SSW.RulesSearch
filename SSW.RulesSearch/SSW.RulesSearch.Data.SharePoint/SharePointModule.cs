using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace SSW.RulesSearch.Data.SharePoint
{
    public class SharePointModule : Module
    {
        private readonly SharePointClientConfig _sharePointClientConfig;

        public SharePointModule(SharePointClientConfig sharePointClientConfig)
        {
            _sharePointClientConfig = sharePointClientConfig;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_sharePointClientConfig)
                .AsSelf();

            builder.RegisterType<RulesDataSource>()
                .As<IRulesDataSource>();
        }
    }
}
