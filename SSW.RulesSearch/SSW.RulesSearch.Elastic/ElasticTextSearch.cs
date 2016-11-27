using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using SSW.RulesSearch.Domain;

namespace SSW.RulesSearch.Elastic
{
    public class ElasticTextSearch : ITextSearch
    {

        private readonly ElasticClient _elasticClient;

        public ElasticTextSearch(ElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public IEnumerable<RuleSearchResult> Search(string query)
        {
            var results = _elasticClient.Search<Rule>(s => s
                .From(0)
                .Size(20)
                //.Query(q => q.QueryString(qs => qs.Query(query)))
                .Query(q => q.Term(t => t.PublishingPageContent, query))
                .Highlight(h => h
                    .PreTags("<b style='color:red'>")
                    .PostTags("</b>")
                    .Fields(fs => fs
                        .Field(f => f.PublishingPageContent)
                        .FragmentSize(50)
                        .NumberOfFragments(4)
                        .HighlightQuery(q => q.Term(t => t.PublishingPageContent, query))
                    )
                    
                )
            );
            return results.Hits.Select(hit => new RuleSearchResult()
            {
                Rule = hit.Source,
                Score = hit.Score,
                Context = string.Join("...", hit.Highlights.SelectMany(h => h.Value.Highlights))
            });
        }
    }
}
