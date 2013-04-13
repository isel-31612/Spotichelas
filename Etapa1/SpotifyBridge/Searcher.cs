using Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace SpotifyBridge
{
    public class Searcher
    {
        public List<string> Track(Track t)
        {
            string query = t.Name.Replace(' ', '+');
            return getHref("track", query);
        }

        public List<string> Album(Album a)
        {
            string query = a.Name.Replace(' ', '+');
            return getHref("album", query);
        }

        public List<string> Artist(Artist a)
        {
            string query = a.Name.Replace(' ', '+');
            return getHref("artist", query);
        }

        protected virtual List<string> getHref(string type,string query)
        {
            var request = PrepareRequest(type, query);
            var response = (HttpWebResponse)request.GetResponse();
            var json = ReadResponse(response);

            return JsonConvert.DeserializeObject<Result>(json).List;
        }

        private WebRequest PrepareRequest(string type, string query)
        {
            string request = "http://ws.spotify.com/search/1/{0}{1}?q={2}";
            string requestType = ".json";
            string requestObj = type;
            string requestUri = string.Format(request, requestType, requestObj);

            WebRequest wr = WebRequest.Create(new Uri(requestUri));
            wr.Method = "GET";
            return wr;
        }

        private string ReadResponse(HttpWebResponse reply)
        {           
            Stream stream = reply.GetResponseStream();
            string response = null;
            byte[] array = new byte[4096];
            StringBuilder sb = new StringBuilder();
            int readCount = 0, bytesRead = 0;
            try{
                do{
                    readCount = stream.Read(array, 0, array.Length);
                    bytesRead += readCount;
                    string s = System.Text.Encoding.UTF8.GetString(array);// TODO: usar contenttype e escolher o encoding correcto
                    sb.Append(s, 0, readCount);
                } while (readCount == array.Length);
                response = sb.ToString();
            }finally { reply.Close(); }
            return response;
        }

        public class Result
        {
            public List<string> List { get; set; }
            public string Info;

            public Result()
            {
            }
        }
    }
}