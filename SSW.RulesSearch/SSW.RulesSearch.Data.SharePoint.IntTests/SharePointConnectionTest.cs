using System;
using System.Collections.Generic;

using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SSW.RulesSearch.Domain;
using Xunit;

namespace SSW.RulesSearch.Data.SharePoint.IntTests
{
    public class SharePointConnectionTest
    {

        private SharePointClientConfig Config => new SharePointClientConfig()
        {
            Url = "https://rules.ssw.com.au"
        };

        [Fact]
        public void CanContactSharePoint()
        {
            var dataSource = new RulesDataSource(Config);
            var response = dataSource.HttpClient.GetAsync(Config.Url + "/_api/web/lists/getByTitle('Pages')/items?$top=5")
                .GetAwaiter().GetResult();
           Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }



        [Fact]
        public void CanDeserializeToRule()
        {
            var dataSource = new RulesDataSource(Config);
            var response = dataSource.HttpClient.GetAsync(Config.Url + "/_api/web/lists/getByTitle('Pages')/items?$top=5")
                .GetAwaiter().GetResult();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            JObject jobject = JObject.Parse(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
            var rules = JsonConvert.DeserializeObject<IEnumerable<Rule>>(jobject["value"].ToString());

            Assert.Equal(5, rules.Count());
        }


    }
}
