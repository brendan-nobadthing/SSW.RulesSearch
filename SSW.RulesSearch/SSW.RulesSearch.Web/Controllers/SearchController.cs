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

        public SearchController(IIndexer<Rule> ruleIndexer)
        {
            _ruleIndexer = ruleIndexer;
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
    }
}