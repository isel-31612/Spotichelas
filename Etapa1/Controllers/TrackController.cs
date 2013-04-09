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
    public class TrackController
    {
        private readonly Logic Rules;
        public TrackController()
        {
            Rules = Logic.Factory(); //TODO: inject Logic or subclass
        }

        [HttpMethod("GET", "/track/{id}")]
        public HttpResponseMessage Get(int id)
        {
            var t = Rules.Find.Track(id);

            return t == null ? new HttpResponseMessage(HttpStatusCode.NotFound) :
                new HttpResponseMessage
                {
                   //TODO: create TrackView Content = new TrackView(t).AsHttpContent("text/html")
                };
        }

        [HttpMethod("POST", "/track")]
        public HttpResponseMessage Post(NameValueCollection content)
        {
            CreateTrack ct = new CreateTrack(content["name"], content["duration"]);
            var track = Rules.Create.Track(ct);
            if (track!=null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            var resp = new HttpResponseMessage(HttpStatusCode.Redirect);
            resp.Headers.Location = new Uri(ResolveUri.For(track));
            return resp;
        }
    }
}
