using Entities;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;
using System.Xml.Linq;

namespace SpotifyBridge
{
    public class Searcher
    {
        public List<Track> Track(string Name)
        {
            string query = Name.Replace(' ', '+');
            List<JsonTrack> list;
            try { list = getHref("track", query).tracks; }
            catch { return new List<Track>(); }
            
            return list.Select((x) => new Track(x.Name, (uint)x.Duration,null,null,x.Link)).ToList(); //TODO: melhorar. Preencher com mais dados
        }

        public List<Album> Album(string Name)
        {
            string query = Name.Replace(' ', '+');
            List<JsonAlbum> list;
            try { list = getHref("album", query).albums; }
            catch { return new List<Album>(); }

            return list.Select((x) => new Album(x.Name, (uint)x.Year, null, null, x.Link)).ToList();  //TODO: melhorar. Preencher com mais dados
        }

        public List<Artist> Artist(string Name)
        {
            string query = Name.Replace(' ', '+');
            List<JsonArtist> list;
            try { list = getHref("artist", query).artists; }
            catch { return new List<Artist>(); }

            return list.Select((x) => new Artist(x.Name, null, x.Link)).ToList();  //TODO: melhorar. Preencher com mais dados
        }

        protected virtual Result getHref(string type,string query)
        {
            var request = PrepareRequest(type, query);
            var response = (HttpWebResponse)request.GetResponse();
            var json = ReadResponse(response);

            return JsonConvert.DeserializeObject<Result>(json);
        }

        private WebRequest PrepareRequest(string type, string query)
        {
            string request = "http://ws.spotify.com/search/1/{0}{1}?q={2}";
            string requestType = ".json";
            string requestObj = type;
            string requestUri = string.Format(request, requestObj, requestType, query);

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
            [JsonProperty("albums",ItemIsReference=true)]
            public List<JsonAlbum> albums;
            [JsonProperty("artists", ItemIsReference = true)]
            public List<JsonArtist> artists;
            [JsonProperty("tracks", ItemIsReference = true)]
            public List<JsonTrack> tracks;
            
            public Result()
            {
            }
        }
    }
}