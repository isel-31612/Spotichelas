using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using DataAccess;
using Utils;
using Views;
using WebGarten2;
using WebGarten2.Html;
using BusinessRules;

namespace Controllers
{
    public class ArtistController
    {
        private readonly Logic Rules;
        public ArtistController()
        {
            Rules = Logic.Factory(); //TODO: inject Logic or subclass
        }

        [HttpMethod("GET", "/artist/new")]
        public HttpResponseMessage New()
        {
            return new HttpResponseMessage
            {
                Content = new PlaylistNewView(null).AsHttpContent("text/html")
            };
        }

        [HttpMethod("GET", "/artist/{id}")]
        public HttpResponseMessage Get(string id)
        {
            string link = string.Format("spotify:artist:{0}",id); //TODO: receber o link do artist e nao o id
            var art = Rules.Find.Artist(link);

            return art == null ? new HttpResponseMessage(HttpStatusCode.NotFound) :
                new HttpResponseMessage
                {
                    Content = new ArtistView(art).AsHttpContent("text/html")
                };
        }
    }
}
