using System.Collections.Generic;

namespace SSW.RulesSearch.Domain
{
    public interface ITextSearch
    {
        IEnumerable<RuleSearchResult> Search(string query);
    }
}