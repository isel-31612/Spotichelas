using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using DataAccess;
using Entities;
using Utils;
using Views;
using WebGarten2;
using WebGarten2.Html;

namespace Controllers
{
    public class ArtistController
    {
        private readonly DAL _rep;
        public ArtistController()
        {
            _rep = DAL.Factory();
        }

        [HttpMethod("GET", "/artist/{id}")]
        public HttpResponseMessage Get(int id)
        {
            Artist art;
            _rep.get(id,out art);

            return art == null ? new HttpResponseMessage(HttpStatusCode.NotFound) :
                new HttpResponseMessage
                {
                    Content = new ArtistView(art).AsHttpContent("text/html")
                };
        }

        [HttpMethod("POST", "/artist")]
        public HttpResponseMessage Post(NameValueCollection content)
        {

            String name = content["name"];
            if (name == "")
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            Artist art = new Artist(name);
            _rep.put(art);
            var resp = new HttpResponseMessage(HttpStatusCode.Redirect);
            resp.Headers.Location = new Uri(ResolveUri.For(art));
            return resp;
        }
    }
}
