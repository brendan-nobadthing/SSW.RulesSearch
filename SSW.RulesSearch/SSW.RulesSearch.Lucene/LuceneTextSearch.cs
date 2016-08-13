using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Documents;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Search.Highlight;
using SSW.RulesSearch.Domain;
using Version = Lucene.Net.Util.Version;

namespace SSW.RulesSearch.Lucene
{
    public class SimpleTextSearch : ITextSearch
    {
        private readonly LuceneContext _luceneContext;
        private readonly IDocumentBuilder<Rule> _ruleDocumentBuilder;

        public SimpleTextSearch(LuceneContext luceneContext, IDocumentBuilder<Rule> ruleDocumentBuilder)
        {
            _luceneContext = luceneContext;
            _ruleDocumentBuilder = ruleDocumentBuilder;
        }

        public IEnumerable<RuleSearchResult> Search(string queryText)
        {
            using (var searcher = _luceneContext.CreateSearcher())
            {
                // Create a query
                var queryParser = new QueryParser(Version.LUCENE_30, "PublishingPageContent", _luceneContext.Analyzer);
                var query = queryParser.Parse(queryText);


                // hit highlighting
                var scorer = new QueryScorer(query);
                var formatter = new SimpleHTMLFormatter("<b class='searchMatch'>", "</b>");
                var highlighter = new Highlighter(formatter, scorer);
                highlighter.TextFragmenter = new SimpleFragmenter(50);

                // run query - top 20 hits
                var searchResult = searcher.Search(query, 20);

                return searchResult.ScoreDocs
                    .Select(sd => new
                    {
                        ScoreDoc = sd,
                        Document = searcher.Doc(sd.Doc)
                    })
                    .Select(s => new RuleSearchResult()
                    {
                        Rule = _ruleDocumentBuilder.ToModel(s.Document),
                        Score = s.ScoreDoc.Score,
                        Context = BuildHighlights(s.Document, highlighter)
                    })
                    .ToList();
            } 
        }

        private string BuildHighlights(Document doc, Highlighter highlighter)
        {
            const string contentField = "PublishingPageContent";
            var fragments = highlighter.GetBestTextFragments(
                    _luceneContext.Analyzer.TokenStream(contentField, new StringReader(doc.Get(contentField))),
                    doc.Get(contentField), true, 5
                );
            return string.Join("...", fragments.Select(f => f.ToString()));
        }
    }
}
