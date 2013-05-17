using System;
using System.Net;
using System.Net.Http;
using WebCache;

namespace WebManager
{
    public class SpotifyInterpreter : HttpCachedInterpreter
    {
        public SpotifyInterpreter() : base(new Cache<string,string>()){}

        protected override string GetCachedValue(HttpClient client, out DateTime date)
        {
            string uri = client.BaseAddress.PathAndQuery;
            string cachedResult;
            webCache.Get(uri, out cachedResult, out date);                
            return cachedResult;
        }

        protected override string GetIfModified(Uri uri, DateTime date, string value)//TODO: i wish i knew how to break this more
        {
            HttpClient conditionalGet = new HttpClient();
            conditionalGet.DefaultRequestHeaders.IfModifiedSince = date;
            var response = ProcessReply(conditionalGet, uri);
            if (response.StatusCode.Equals(HttpStatusCode.NotModified)) { return value; }
            string s = response.RequestMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            CacheResult(response, s);
            return s;
        }

        protected override void CacheResult(HttpResponseMessage response, string value)
        {
            string uri = response.RequestMessage.RequestUri.PathAndQuery;
            DateTime expires = response.Content.Headers.Expires==null? DateTime.Now.AddDays(1):response.Content.Headers.Expires.Value.Date;
            DateTime date = response.Headers.Date.Value.Date;
            webCache.Add(uri, value, date, expires);
        }
    }
}
