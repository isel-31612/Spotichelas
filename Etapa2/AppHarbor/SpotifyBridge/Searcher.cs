using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;

using Utils;
using WebManager;
using Entities;

using Newtonsoft.Json;

namespace SpotifyBridge
{
    public class Searcher
    {
        public HttpInterpreter interpreter;
        public Searcher()
        {
            interpreter = new SpotifyInterpreter();
        }

        public SearchResult<Track> Track(string Name)
        {
            string query = Name.Replace(' ', '+');
            string json = search("track", query);
            if (json == null)
                return new SearchResult<Track>();
            JsonSearchResult results = JsonConvert.DeserializeObject<JsonSearchResult>(json);
            List<Track> list = results.tracks.ToEntityList();

            SearchInfo info = results.ExtractSearchInfo();
            return new SearchResult<Track>(list,info);
        }

        public SearchResult<Album> Album(string Name)
        {
            string query = Name.Replace(' ', '+');
            string json = search("album", query);
            if (json == null)
                return new SearchResult<Album>();
            JsonSearchResult results = JsonConvert.DeserializeObject<JsonSearchResult>(json);
            List<Album> list = results.albums.ToEntityList();

            SearchInfo info = results.ExtractSearchInfo();
            return new SearchResult<Album>(list,info);
        }

        public SearchResult<Artist> Artist(string Name)
        {
            string query = Name.Replace(' ', '+');
            string json = search("artist", query);
            if (json == null)
                return new SearchResult<Artist>();
            JsonSearchResult results = JsonConvert.DeserializeObject<JsonSearchResult>(json);
            List<Artist> list = results.artists.ToEntityList();

            SearchInfo info = results.ExtractSearchInfo();
            return new SearchResult<Artist>(list,info);
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

    public class SearchResult<T>
    {
        public List<T> Results;
        public SearchInfo Info;

        public SearchResult()
        {
            Results = new List<T>();
            Info = new SearchInfo();
        }

        public SearchResult(List<T> items, SearchInfo info)
        {
            Results = items;
            Info = info;
        }
    }
}
