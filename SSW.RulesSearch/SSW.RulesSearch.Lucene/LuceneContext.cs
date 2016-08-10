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

        private Directory _directory = null;
        private Analyzer _analyser = null;
        private IndexWriter _indexWriter = null;


        public LuceneContext(LuceneSettings luceneSettings)
        {
            _luceneSettings = luceneSettings;

            Log.Information("creating Lucene context with directory folder {IndexDirectory}",
                _luceneSettings.IndexDirectory);

        }

        public Directory Directory => _directory ?? (_directory = FSDirectory.Open(_luceneSettings.IndexDirectory));

        public Analyzer Analyzer => _analyser ?? (_analyser = new StandardAnalyzer(Version.LUCENE_30));

        public IndexWriter Writer => _indexWriter ?? (_indexWriter = new IndexWriter(Directory, Analyzer, IndexWriter.MaxFieldLength.UNLIMITED));

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
