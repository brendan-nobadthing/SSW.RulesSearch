using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Version = Lucene.Net.Util.Version;
using Serilog;

namespace SSW.RulesSearch.Lucene
{

    /// <summary>
    /// Simple, singleton lucene instance that proives access to lucene artifacts
    /// </summary>

    public class LuceneContext : IDisposable
    {
        private readonly LuceneSettings _luceneSettings;

        public LuceneContext(LuceneSettings luceneSettings)
        {
            _luceneSettings = luceneSettings;

            Log.Information("creating Lucene cotext with directory folder {IndexDirectory}",
                _luceneSettings.IndexDirectory);

            Directory = FSDirectory.Open(_luceneSettings.IndexDirectory);
            Analyzer = new StandardAnalyzer(Version.LUCENE_30);
            Writer = new IndexWriter(Directory, Analyzer, IndexWriter.MaxFieldLength.UNLIMITED);
        }

        public Directory Directory { get; set; }
        public Analyzer Analyzer { get; set; }
        public IndexWriter Writer { get; set; }

        public IndexSearcher CreateSearcher()
        {
            return new IndexSearcher(Directory);
        }

        public void Dispose()
        {
            Log.Information("Disposing Lucene Context");

            Analyzer.Dispose();
            Writer.Flush(true, true, true);
            Writer.Dispose(true);
            Directory.Dispose(); 
        }

    }
}
