using System;
using System.Net;
using System.Net.Http;

namespace WebManager
{
    public abstract class HttpInterpreter
    {
        private static readonly int nMaxRetrys = 3;
        public string GetResponse(string uri)
        {
            string ret = Execute(uri);
            return (ret != null) ? ret : Retry(uri, nMaxRetrys);
        }

        protected virtual string Execute(string uri)
        {
            Uri requestUri = CreateUri(uri);
            HttpClient client = CreateRequest();
            HttpResponseMessage response = ProcessReply(client, requestUri);
            return ExtractString(response);
        }
        protected virtual Uri CreateUri(string uri) { return new Uri(uri); }
        protected virtual HttpClient CreateRequest() { return new HttpClient(); }
        protected virtual HttpResponseMessage ProcessReply(HttpClient client, Uri uri) { return client.GetAsync(uri).GetAwaiter().GetResult(); }
        protected virtual string ExtractString(HttpResponseMessage response)
        {
            return (response.IsSuccessStatusCode) ? response.Content.ReadAsStringAsync().GetAwaiter().GetResult() : null;
        }

        private string Retry(string uri, int maxTrys)
        {
            if (maxTrys <= 0)
                return null;
            string ret = Execute(uri);
            return (ret!=null)? ret: Retry(uri,maxTrys-1);
        }
    }
}