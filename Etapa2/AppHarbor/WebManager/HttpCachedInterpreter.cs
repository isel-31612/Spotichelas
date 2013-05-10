using System;
using System.Net.Http;
using WebCache;

namespace WebManager
{
    public abstract class HttpCachedInterpreter : HttpInterpreter
    {
        protected Cache<string, string> webCache;

        protected HttpCachedInterpreter(Cache<string, string> cache)
        {
            webCache = cache;
        }
        public override string GetResponse(string uri)
        {
            Uri requestUri = CreateUri(uri);
            HttpClient client = CreateRequest();
            DateTime dt;
            var value = GetCachedValue(client, out dt);
            if (value != null)
            {
                return GetIfModified(requestUri, dt, value);
            }
            HttpResponseMessage response = ProcessReply(client, requestUri);
            string result = ExtractString(response);
            CacheResult(response, result);
            return result;
        }

        protected abstract string GetCachedValue(HttpClient client, out DateTime dt);
        protected abstract string GetIfModified(Uri uri, DateTime date, string value);
        protected abstract void CacheResult(HttpResponseMessage response, string value);
    }
}