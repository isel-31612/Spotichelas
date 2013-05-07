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
using WebCache;

namespace SpotifyBridge
{
    public class Searcher
    {
        public SpotifyInterpreter interpreter;
        public Searcher()
        {
            interpreter = new SpotifyInterpreter(new Cache<string,string>());
        }

        public List<Track> Track(string Name, out SearchInfo info)
        {
            string query = Name.Replace(' ', '+');
            var json = interpreter.search("track", query);
            var search = JsonConvert.DeserializeObject<JsonSearchResult>(json);
            var list = search.tracks.ToEntityList();

            info = search.ExtractSearchInfo();
            return list;
        }

        public List<Album> Album(string Name, out SearchInfo info)
        {
            string query = Name.Replace(' ', '+');
            var json = interpreter.search("album", query);
            var search = JsonConvert.DeserializeObject<JsonSearchResult>(json);
            var list = search.albums.ToEntityList();

            info = search.ExtractSearchInfo();
            return list;
        }

        public List<Artist> Artist(string Name, out SearchInfo info)
        {
            string query = Name.Replace(' ', '+');
            var json = interpreter.search("artist", query);
            var search = JsonConvert.DeserializeObject<JsonSearchResult>(json);
            var list = search.artists.ToEntityList();

            info = search.ExtractSearchInfo();
            return list;
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