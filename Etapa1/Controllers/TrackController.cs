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
    public class TrackController
    {
        private readonly DAL _rep;
        public TrackController()
        {
            _rep = DAL.Factory();
        }

        [HttpMethod("GET", "/track/{id}")]
        public HttpResponseMessage Get(int id)
        {
            Artist t;
            _rep.get(id, out t);

            return t == null ? new HttpResponseMessage(HttpStatusCode.NotFound) :
                new HttpResponseMessage
                {
                    Content = new ArtistView(t).AsHttpContent("text/html")
                };
        }

        [HttpMethod("POST", "/track")]
        public HttpResponseMessage Post(NameValueCollection content)
        {

            String name = content["name"];
            UInt32 duration = UInt32.Parse(content["duration"]);
            if (name == "" || duration == 0)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            Track t = new Track(name, duration);
            _rep.put(t);
            var resp = new HttpResponseMessage(HttpStatusCode.Redirect);
            resp.Headers.Location = new Uri(ResolveUri.For(t));
            return resp;
        }
    }
}
