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
    public class AlbumController
    {
        private readonly DAL _rep;
        public AlbumController()
        {
            _rep = DAL.Factory();
        }

        [HttpMethod("GET", "/album/{id}")]
        public HttpResponseMessage Get(int id)
        {
            Album alb;
            _rep.get(id,out alb);

            return alb == null ? new HttpResponseMessage(HttpStatusCode.NotFound) :
                new HttpResponseMessage
                {
                    Content = new AlbumView(alb).AsHttpContent("text/html")
                };
        }

        [HttpMethod("POST", "/album")]
        public HttpResponseMessage Post(NameValueCollection content)
        {

            String name = content["name"];
            uint year = UInt32.Parse(content["year"]);
            if (name == "" || year == 0)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            Album alb = new Album(name, year);
            _rep.put(alb);
            var resp = new HttpResponseMessage(HttpStatusCode.Redirect);
            resp.Headers.Location = new Uri(ResolveUri.For(alb));
            return resp;
        }
    }
}
