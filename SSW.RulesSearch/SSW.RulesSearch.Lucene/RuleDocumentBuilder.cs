using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Documents;
using SSW.RulesSearch.Models;

namespace SSW.RulesSearch.Lucene
{
    public class RuleDocumentBuilder : IDocumentBuilder<Rule>
    {
        public Document ToDocument(Rule model)
        {
            var doc = new Document();

            doc.Add(new Field("Id", model.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field("Title", model.Title ?? "", Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("RuleContentTop", model.RuleContentTop ?? "", Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("PublishingPageContent", model.PublishingPageContent, Field.Store.YES, Field.Index.ANALYZED));

            if (model.RulesKeyWords != null)
            {
                foreach (var keyWord in model.RulesKeyWords.Split(new char[] {';'}))
                {
                    doc.Add(new Field("RulesKeyWords", keyWord, Field.Store.YES, Field.Index.ANALYZED));
                }
            }
            if (model.Created.HasValue)
            {
                var createdDateString = DateTools.DateToString(model.Created.Value, DateTools.Resolution.HOUR);
                doc.Add(new Field("Created", createdDateString, Field.Store.YES, Field.Index.NOT_ANALYZED));
            }
            if (model.Modified.HasValue)
            {
                var modifiedDateString = DateTools.DateToString(model.Modified.Value, DateTools.Resolution.HOUR);
                doc.Add(new Field("Modified", modifiedDateString, Field.Store.YES, Field.Index.NOT_ANALYZED));
            }
            return doc;
        }

        public Rule ToModel(Document doc)
        {
            return new Rule()
            {
                Id = Int32.Parse(doc.GetField("Id").StringValue),
                Created = DateTools.StringToDate(doc.GetValues("Created").FirstOrDefault()),
                Title = doc.GetValues("Title").FirstOrDefault(),
                Modified = DateTools.StringToDate(doc.GetValues("Modified").FirstOrDefault()),
                PublishingPageContent = doc.GetValues("PublishingPageContent").FirstOrDefault(),
                RuleContentTop = doc.GetValues("RuleContentTop").FirstOrDefault(),
                RulesKeyWords = string.Join(";", doc.GetValues("RulesKeyWords"))
            };
        }

        public string KeyField {
            get { return "Id"; }
        }

        public int ToKey(Rule model)
        {
            return model.Id;
        }
    }
}
