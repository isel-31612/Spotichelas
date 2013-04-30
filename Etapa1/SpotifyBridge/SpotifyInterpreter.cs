using System;
using System.IO;
using System.Net;
using System.Text;

using WebCache;

namespace SpotifyBridge
{
    public class SpotifyInterpreter
    {
        Cache<string> cache;
        public SpotifyInterpreter()
        {
            cache = new Cache<string>();
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
            string cachedResult;                //TODO: É o sitio certo para a cache? Nao e melhor fazer cache dum JsonResult?
            if (cache.Get(requestUri, out cachedResult))
                return cachedResult;

            WebRequest wr = WebRequest.Create(new Uri(requestUri));
            wr.Method = "GET";
            HttpWebResponse reply = null;
            try
            {
                reply = (HttpWebResponse)wr.GetResponse(); //Throws exception if there is an issue with comunicating(Bad Requests, GatewayTimeout, etc)
                return ProcessReply(reply);//The only reason this is included is to allow the finnaly block to always close the connection
            }
            catch (Exception e)
            {
                throw new ApplicationException("Error in acquiring data from spotify.", e);//TODO: 5XX errors(none actually) should crash application
            }
            finally
            {
                reply.Close();
            }
        }

        private string ProcessReply(HttpWebResponse reply)
        {
                var encode = getEncoding(reply.ContentType);
                StreamReader stream = new StreamReader(reply.GetResponseStream(), encode);
                return stream.ReadToEnd();
        }

        private Encoding getEncoding(string encoding)
        {
            if (encoding.Contains("utf-8")) return System.Text.Encoding.GetEncoding("utf-8");
            throw new EncoderFallbackException();
        }
    }
}