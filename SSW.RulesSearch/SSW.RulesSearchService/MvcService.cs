using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace SSW.RulesSearchService
{
    public class MvcService
    {

        private IDisposable _webapp;

        public void Start()
        {
            _webapp = WebApp.Start<Startup>("http://localhost:8080");
        }

        public void Stop()
        {
            _webapp?.Dispose();
        }

    }
}
