using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using SSW.RulesSearch.Domain;
using Xunit;
using Version = Lucene.Net.Util.Version;

namespace SSW.RulesSearch.Lucene.IntTests
{

    /// <summary>
    /// These tests are terrible! - they assume that data has already been loaded into the index
    /// </summary>

    public class LuceneQueryTests
    {

        [Fact]
        public void ContentSearchTest()
        {
            using (var container = AutofacContainerFactory.CreateContainer())
            {
                var luceneContext = container.Resolve<LuceneContext>();

                using (var indexSearcher = luceneContext.CreateSearcher())
                {
                    var queryParser = new QueryParser(Version.LUCENE_30, "PublishingPageContent", luceneContext.Analyzer);
                    var query = queryParser.Parse("Email number your tasks");
                    var searchResults = indexSearcher.Search(query, 20);

                    Assert.NotNull(searchResults);
                    Assert.NotEmpty(searchResults.ScoreDocs);

                    var firstDoc = indexSearcher.Doc(searchResults.ScoreDocs[0].Doc);
                    Assert.NotNull(firstDoc);

                    var ruleDocumentBuilder = container.Resolve<IDocumentBuilder<Rule>>();
                    var rule = ruleDocumentBuilder.ToModel(firstDoc);
                    Assert.NotNull(rule);
                }
            }
        }

    }
}
