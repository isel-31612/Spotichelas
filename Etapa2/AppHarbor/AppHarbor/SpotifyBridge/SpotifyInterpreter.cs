using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Http;

using WebCache;
using System.Threading.Tasks;

namespace SpotifyBridge
{
    public class SpotifyInterpreter
    {
        Cache<string,string> cache;
        public SpotifyInterpreter(Cache<string,string> c)
        {
            cache = c;
        }

        public string search(string type, string query)
        {
            string request = "http://ws.spotify.com/search/1/{0}{1}?q={2}";
            string requestType = ".json";
            string requestObj = type;
            string requestUri = string.Format(request, requestObj, requestType, query);

            return ProcessRequest(requestUri);
        }
        public string lookup(string id, string detail = null)
        {
            string request = "http://ws.spotify.com/lookup/1/{0}?uri={1}{2}";
            string requestType = ".json";
            string requestId = id;
            string requestExtras = detail != null ? string.Format("&extras={0}", detail) : "";
            string requestUri = string.Format(request, requestType, requestId, requestExtras);

            return ProcessRequest(requestUri);
        }

        private string ProcessRequest(string requestUri)
        {
            HttpClient c = new HttpClient();
            var uri =new Uri(requestUri);

            HttpResponseMessage reply;            

            string cachedResult;
            DateTime date;
            if (cache.Get(uri.PathAndQuery, out cachedResult, out date)) c.DefaultRequestHeaders.IfModifiedSince = date;
            reply = ProcessReply(c, uri);

            if (reply.StatusCode.Equals(HttpStatusCode.NotModified)) return cachedResult;
            return ExtractString(reply);
        }

        private HttpResponseMessage ProcessReply(HttpClient client, Uri uri)
        {
            return client.GetAsync(uri).GetAwaiter().GetResult();
        }

        private string ExtractString(HttpResponseMessage response)
        {
            string uri = response.RequestMessage.RequestUri.PathAndQuery;
            DateTime expires = DateTime.Now.AddDays(1); //TODO: to be fixed once i know how to extract expires header
            DateTime date = response.Headers.Date.GetValueOrDefault().DateTime;//TODO: might not have the expected behaviour

            string s = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            cache.Add(uri, s, date, expires);
            return s;
        }
    }
}