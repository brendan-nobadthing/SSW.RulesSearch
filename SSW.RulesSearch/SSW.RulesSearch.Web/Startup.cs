using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.Owin;
using Owin;
using Serilog;
using SSW.RulesSearch.Commands;
using SSW.RulesSearch.Data.SharePoint;
using SSW.RulesSearch.Lucene;

[assembly: OwinStartupAttribute(typeof(SSW.RulesSearch.Web.Startup))]
namespace SSW.RulesSearch.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
            ConfigureSerilog(app);
            ConfigureAutofac(app);

           
        }

        private void ConfigureAutofac(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            // STANDARD MVC SETUP:

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterModule<RulesSearchWebModule>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // OWIN MVC SETUP:

            // Register the Autofac middleware FIRST, then the Autofac MVC middleware.
            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();
        }

        private void ConfigureSerilog(IAppBuilder app)
        {
            Log.Logger = new LoggerConfiguration()
                 .MinimumLevel.Debug()
                 .WriteTo.LiterateConsole()
                 .WriteTo.RollingFile(@"c:\log\SSW.RulesSearchWeb{Date}.log")
                 .WriteTo.Seq("http://localhost:5341")
                 .Enrich.WithProperty("ApplicationName", "SSW.RulesSearch.Web")
                 .CreateLogger();

            Log.Information("Starting application {ApplicationName}");
        }
    }
}
