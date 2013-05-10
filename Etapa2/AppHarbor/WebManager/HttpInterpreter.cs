using System;
using System.Net.Http;

namespace WebManager
{
    public abstract class HttpInterpreter
    {
        public virtual string GetResponse(string uri)
        {
            Uri requestUri = CreateUri(uri);
            HttpClient client = CreateRequest();
            HttpResponseMessage response = ProcessReply(client, requestUri);
            string result = ExtractString(response);
            return result;
        }

        protected virtual Uri CreateUri(string uri) { return new Uri(uri); }
        protected virtual HttpClient CreateRequest() { return new HttpClient(); }
        protected virtual HttpResponseMessage ProcessReply(HttpClient client, Uri uri) { return client.GetAsync(uri).GetAwaiter().GetResult(); }
        protected virtual string ExtractString(HttpResponseMessage response) { return response.Content.ReadAsStringAsync().GetAwaiter().GetResult(); }
    }
}