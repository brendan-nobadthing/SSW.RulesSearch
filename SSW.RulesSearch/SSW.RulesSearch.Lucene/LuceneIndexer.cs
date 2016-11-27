using Lucene.Net.Index;
using Serilog;

namespace SSW.RulesSearch.Lucene
{
    public interface IIndexer<T> where T : class
    {
        void Index(T model);

        void Remove(T model);

        void Remove(int id);

        void ClearIndex();

        void Commit();

        int GetItemCount();

    }

    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    public class LuceneIndexer<T> : IIndexer<T> where T : class
    {   
        private readonly LuceneContext _luceneContext;
        private readonly IDocumentBuilder<T> _documentBuilder;

        public LuceneIndexer(LuceneContext luceneContext, IDocumentBuilder<T> documentBuilder)
        {
            _luceneContext = luceneContext;
            _documentBuilder = documentBuilder;
        }

        public void Index(T model)
        {
            Remove(model);
            Log.Debug("adding item to index {id} ", _documentBuilder.ToKey(model));
            _luceneContext.Writer.AddDocument(_documentBuilder.ToDocument(model));
        }

        public void Remove(T model)
        {
           Remove(_documentBuilder.ToKey(model));
        }

        public void Remove(int id)
        {
            using (var indexReader = IndexReader.Open(_luceneContext.Directory, false))
            {
                if (!indexReader.TermDocs(new Term(_documentBuilder.KeyField, id.ToString())).Next()) return;
                Log.Debug("removing item from index {id} ", id);
                _luceneContext.Writer.DeleteDocuments(new Term(_documentBuilder.KeyField, id.ToString()));
            }
        }


        public void ClearIndex()
        {
            _luceneContext.Writer.DeleteAll();
        }


        public virtual void Commit()
        {
            _luceneContext.Writer.Commit();
        }

        public int GetItemCount()
        {
            return _luceneContext.Writer.NumDocs();
        }

    }
}
