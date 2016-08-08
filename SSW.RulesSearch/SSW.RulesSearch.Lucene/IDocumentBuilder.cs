using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Documents;

namespace SSW.RulesSearch.Lucene
{
    public interface IDocumentBuilder<T> where T : class
    {
        Document ToDocument(T model);

        T ToModel(Document doc);

        string KeyField { get; }

        int ToKey(T model);


    }

}
