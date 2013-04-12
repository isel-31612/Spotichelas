using Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;

using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace SpotifyBridge
{
    public class LookUp
    {
        public Track Track(string id)
        {
            var json = LookInto(id,"track");
            var obj = JSONParser<Reply>(json).track;
            string name = obj.Name;
            uint duration = (uint)obj.Duration;
            Album al = new Album(obj.Album.Name,(uint)obj.Album.Year);
            Artist ar = new Artist(obj.Name);
            return new Track(name, duration, ar, al);
        }

        public Artist Artist(string id)
        {
            var json = LookInto(id,"artist","album");
            var obj = JSONParser<Reply>(json).artist;
            string name = obj.Name;
            IEnumerable<Album> a = obj.Albuns.Select(x => new Album(x.Album.Name, (uint)x.Album.Year));
            return new Artist(name,a.ToList());
        }

        public Album Album(string id)
        {
            var json = LookInto(id,"album","track");
            var obj = JSONParser<Reply>(json).album;
            string name = obj.Name;
            uint year = (uint)obj.Year;
            IEnumerable<Track> t = obj.Tracks.Select(x => new Track(x.Name,(uint)x.Duration));
            Artist a = new Artist(obj.ArtistName);
            return new Album(name, year, t.ToList(), a);
        }

        protected virtual string LookInto(string id,string type,string detail=null)
        {
            string request = "http://ws.spotify.com/lookup/1/{0}?uri=spotify:{1}:{2}{3}";
            string requestType = ".json";
            string requestObj = type;
            string requestId = id;
            string requestExtras = detail!=null?string.Format("&extras={0}",detail):"";
            string requestUri = string.Format(request,requestType,requestObj,requestId,requestExtras);

            WebRequest wr = WebRequest.Create(new Uri(requestUri));
            wr.Method = "GET";
            HttpWebResponse reply = null;
            string response = null;
            try
            {
                reply = (HttpWebResponse)wr.GetResponse();
                Stream stream = reply.GetResponseStream();
                byte[] array = new byte[4096];
                StringBuilder sb = new StringBuilder();
                int readCount=0;
                int bytesRead=0;
                do{
                    readCount = stream.Read(array, 0,array.Length);
                    bytesRead += readCount;
                    string s = System.Text.Encoding.UTF8.GetString(array);// TODO: usar contenttype e escolher o encoding correcto
                    sb.Append(s,0,readCount);
                }while(readCount==array.Length);
                response = sb.ToString();
            }
            finally { reply.Close(); }
            return response;
        }

        protected virtual T JSONParser<T>(string json)
        {
            Console.WriteLine(json);
            T obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }
    }

    public class Reply
    {
        [JsonProperty("artist", ItemIsReference = true)]
        public JsonArtist artist { get; set; }
        [JsonProperty("album", ItemIsReference = true)]
        public JsonAlbum album { get; set; }
        [JsonProperty("track", ItemIsReference = true)]
        public JsonTrack track { get; set; }
    }

    public class ReplyType
    {
        [JsonProperty("type")]
        public string Replytype { get; set; }
    }
}