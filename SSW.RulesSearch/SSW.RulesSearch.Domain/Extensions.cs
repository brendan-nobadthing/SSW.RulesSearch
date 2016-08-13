using HtmlAgilityPack;

namespace SSW.RulesSearch.Domain
{
    public static class Extensions
    {

        public static string StripHtml(this string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml($"<body>{html}</body>");
            return doc.DocumentNode.SelectSingleNode("//body").InnerText;
        }

    }
}