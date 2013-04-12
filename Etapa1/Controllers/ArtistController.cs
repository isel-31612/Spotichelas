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
        public HttpResponseMessage Get(int id)
        {
            var art = Rules.Find.Artist(id);

            return art == null ? new HttpResponseMessage(HttpStatusCode.NotFound) :
                new HttpResponseMessage
                {
                    Content = new ArtistView(art).AsHttpContent("text/html")
                };
        }

        [HttpMethod("POST", "/artist")]
        public HttpResponseMessage Post(NameValueCollection content)
        {
            CreateArtist ca = new CreateArtist(content["name"]);
            var artist = Rules.Create.Artist(ca);
            if (artist == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            var resp = new HttpResponseMessage(HttpStatusCode.SeeOther);
            resp.Headers.Location = new Uri(ResolveUri.For(artist));
            return resp;
        }
    }
}
