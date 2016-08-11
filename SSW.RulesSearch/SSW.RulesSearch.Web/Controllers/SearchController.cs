using System;
using System.Collections.Generic;
//using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SSW.RulesSearch.Lucene;
using SSW.RulesSearch.Models;

namespace SSW.RulesSearch.Web.Controllers
{
    public class SearchController : Controller
    {

        private readonly IIndexer<Rule> _ruleIndexer;
        private readonly ITextSearch _textSearch;

        public SearchController(IIndexer<Rule> ruleIndexer, ITextSearch textSearch)
        {
            _ruleIndexer = ruleIndexer;
            _textSearch = textSearch;
        }

        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Count()
        {
            return Content(_ruleIndexer.GetItemCount().ToString());
        }

        public ActionResult Search(string query)
        {
            var searchResults = _textSearch.Search(query);
            return PartialView("_SearchResults", searchResults);
        }
    }
}