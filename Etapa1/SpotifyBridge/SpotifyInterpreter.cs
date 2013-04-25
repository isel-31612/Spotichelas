using System;
using System.IO;

using System.Net;
using System.Text;

namespace SpotifyBridge
{
    public class SpotifyInterpreter
    {
        public SpotifyInterpreter()
        {
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
                var encoder = getEncoding(reply.ContentType);
                Stream stream = reply.GetResponseStream();
                byte[] array = new byte[reply.ContentLength];
                long readCount = stream.Read(array, 0, array.Length);
                String sb = encoder.Invoke(array);
                return sb;
        }

        private Func<byte[],string> getEncoding(string encoding)
        {
            if (encoding.Contains("utf-8")) return System.Text.Encoding.UTF8.GetString;
            throw new EncoderFallbackException();
        }
    }
}
