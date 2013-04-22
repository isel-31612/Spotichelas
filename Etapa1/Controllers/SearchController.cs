using BusinessRules;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using Views;
using WebGarten2;
using WebGarten2.Html;
using Utils;

namespace Controllers
{
    public class SearchController
    {
        private readonly Logic Rules;

        public SearchController()
        {
            Rules = Logic.Factory();
        }

        [HttpMethod("GET", "/search")]
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage
            {
                Content = new SearchView().AsHttpContent("text/html")
            };
        }

        [HttpMethod("POST", "/search")]
        public HttpResponseMessage Post(NameValueCollection content)
        {
            string searchType = content["type"];
            string searchQuery = content["search"];
            return new HttpResponseMessage
            {
                Content = Method(searchQuery,searchType).AsHttpContent("text/html")
            };
        }

        public HtmlDoc Method(string searchQuery, string type)
        {
            if (type.Equals("artists")) return new SearchView(Rules.FindAll.Artists(searchQuery));
            if (type.Equals("albuns"))  return new SearchView(Rules.FindAll.Albuns(searchQuery));
            if (type.Equals("tracks"))  return new SearchView(Rules.FindAll.Tracks(searchQuery));
            return null;
        }
    }
}
