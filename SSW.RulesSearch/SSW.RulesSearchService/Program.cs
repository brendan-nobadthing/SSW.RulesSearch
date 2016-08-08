using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Serilog;
using SSW.RulesSearch.Commands;

namespace SSW.RulesSearchService
{
    class Program
    {
        static void Main(string[] args)
        {

            Log.Logger = new LoggerConfiguration()
                 .MinimumLevel.Debug()
                 .WriteTo.LiterateConsole()
                 .WriteTo.RollingFile(@"c:\log\SSW.RulesSearchService{Date}.log")
                 .WriteTo.Seq("http://localhost:5341")
                 .Enrich.WithProperty("ApplicationName", "SSW.RulesSearchService")
                 .CreateLogger();

            Log.Information("Starting application {ApplicationName}");

            using (var container = SearchServiceContainerFactory.CreateContainer())
            {
                var indexCommand = container.Resolve<IndexAllRules>();
                indexCommand.Run();
                Log.Information("Index Complete");
            }
            Console.ReadLine();

        }
    }
}
