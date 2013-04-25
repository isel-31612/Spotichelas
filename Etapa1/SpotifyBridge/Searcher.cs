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

namespace SpotifyBridge
{
    public class Searcher
    {
        public SpotifyInterpreter interpreter;
        public Searcher()
        {
            interpreter = new SpotifyInterpreter();
        }
        public List<Track> Track(string Name, out SearchInfo info)
        {
            string query = Name.Replace(' ', '+');
            var json = interpreter.search("track", query);
            var search = JsonConvert.DeserializeObject<JsonSearchResult>(json);
            var list = search.tracks;
            List<Track> ret = new List<Track>();
            info = new SearchInfo(search.Info.Count, search.Info.Max, search.Info.Offset, search.Info.Query, search.Info.Type, search.Info.Page);
            foreach (var track in list)
            {
                var artist = track.Artists.Select( x => new Artist(x.Name,x.Link)).ToList();
                var album = new Album(track.Album.Name, track.Album.Link);
                ret.Add(new Track(track.Name, track.Link, track.Duration, artist, album));
            }
            return ret;
        }

        public List<Album> Album(string Name, out SearchInfo info)
        {
            string query = Name.Replace(' ', '+');
            var json = interpreter.search("album", query);
            var search = JsonConvert.DeserializeObject<JsonSearchResult>(json);
            var list = search.albums;
            info = new SearchInfo(search.Info.Count, search.Info.Max, search.Info.Offset, search.Info.Query, search.Info.Type, search.Info.Page);
            List<Album> ret = new List<Album>();
            foreach (var album in list)
            {
                List<Artist> alist = album.Artists.Select(a => new Artist(a.Name,a.Link)).ToList();
                ret.Add(new Album(album.Name,album.Link,0,alist));
            }
            return ret;
        }

        public List<Artist> Artist(string Name, out SearchInfo info)
        {
            string query = Name.Replace(' ', '+');
            var json = interpreter.search("artist", query);
            var search = JsonConvert.DeserializeObject<JsonSearchResult>(json);
            var list = search.artists;
            info = new SearchInfo(search.Info.Count, search.Info.Max, search.Info.Offset, search.Info.Query, search.Info.Type, search.Info.Page);
            List<Artist> ret = list.Select(a => new Artist(a.Name, a.Link)).ToList();
            return ret;
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

        public class JsonSearchInfo
        {
            [JsonProperty("num_results")]
            public int Count;
            [JsonProperty("limit")]
            public int Max;
            [JsonProperty("offset")]
            public int Offset;
            [JsonProperty("query")]
            public string Query;
            [JsonProperty("type")]
            public string Type;
            [JsonProperty("page")]
            public int Page;
        }
    }
}