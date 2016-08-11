using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSW.RulesSearch.Models
{
    public class RuleSearchResult
    {

        public Rule Rule { get; set; }

        public float Score { get; set; }

        public string Context { get; set; }


    }
}
