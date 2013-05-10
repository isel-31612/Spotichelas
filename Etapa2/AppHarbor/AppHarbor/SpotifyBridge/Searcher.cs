using Entities;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;
using Utils;
using WebManager;

namespace SpotifyBridge
{
    public class Searcher
    {
        public HttpInterpreter interpreter;
        public Searcher()
        {
            interpreter = new SpotifyInterpreter();
        }

        public List<Track> Track(string Name, out SearchInfo info)
        {
            string query = Name.Replace(' ', '+');
            var json = search("track", query);
            var results = JsonConvert.DeserializeObject<JsonSearchResult>(json);
            var list = results.tracks.ToEntityList();

            info = results.ExtractSearchInfo();
            return list;
        }

        public List<Album> Album(string Name, out SearchInfo info)
        {
            string query = Name.Replace(' ', '+');
            var json = search("album", query);
            var results = JsonConvert.DeserializeObject<JsonSearchResult>(json);
            var list = results.albums.ToEntityList();

            info = results.ExtractSearchInfo();
            return list;
        }

        public List<Artist> Artist(string Name, out SearchInfo info)
        {
            string query = Name.Replace(' ', '+');
            var json = search("artist", query);
            var results = JsonConvert.DeserializeObject<JsonSearchResult>(json);
            var list = results.artists.ToEntityList();

            info = results.ExtractSearchInfo();
            return list;
        }

        public string search(string type, string query)
        {
            string request = "http://ws.spotify.com/search/1/{0}{1}?q={2}";
            string requestType = ".json";
            string requestObj = type;
            string requestUri = string.Format(request, requestObj, requestType, query);
            return interpreter.GetResponse(requestUri);
        }
    }
    public class JsonSearchResult
    {
        [JsonProperty("info", ItemIsReference = true)]
        public JsonSearchInfo Info;
        [JsonProperty("albums", ItemIsReference = true)]
        public List<JsonSearchAlbum> albums;
        [JsonProperty("artists", ItemIsReference = true)]
        public List<JsonSearchArtist> artists;
        [JsonProperty("tracks", ItemIsReference = true)]
        public List<JsonSearchTrack> tracks;

        public JsonSearchResult()
        {
        } 
    }
}
