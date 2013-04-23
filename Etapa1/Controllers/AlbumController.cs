using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using BusinessRules;
using Utils;
using Views;
using WebGarten2;
using WebGarten2.Html;

namespace Controllers
{
    public class AlbumController
    {
        private readonly Logic Rules;
        public AlbumController()
        {
            Rules = Logic.Factory(); //TODO: inject Logic or subclass
        }

        [HttpMethod("GET", "/album/new")]
        public HttpResponseMessage New()
        {
            return new HttpResponseMessage
            {
                Content = new PlaylistNewView(null).AsHttpContent("text/html")
            };
        }

        [HttpMethod("GET", "/album/{id}")]
        public HttpResponseMessage Get(string id)
        {
            var album = Rules.Find.Album(id);

            return album == null ? new HttpResponseMessage(HttpStatusCode.NotFound) :
                new HttpResponseMessage
                {
                    Content = new AlbumView(album).AsHttpContent("text/html")
                };
        }
    }
}
