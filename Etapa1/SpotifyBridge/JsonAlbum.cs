using System.Collections.Generic;
using System;
using Newtonsoft.Json;

namespace SpotifyBridge
{
    public class JsonAlbum
    {
        [JsonProperty("name")]
        public string Name;
        [JsonProperty("href")]
        public string Link;
        [JsonProperty("artist-id")]
        public string ArtistId;
        [JsonProperty("artist")]
        public string ArtistName;
        [JsonProperty("released")]
        public int Year;
        [JsonProperty("tracks", ItemIsReference = true)]
        public List<JsonTrack> Tracks;
        [JsonProperty("availability", ItemIsReference = true)]
        public JsonAvaibility Availability;

        public JsonAlbum()
        {
        }
    }

    public class JsonAvaibility
    {
        [JsonProperty("territories")]
        public string Territories;

        public JsonAvaibility()
        {
        }
    }

    public class JsonWrapperAlbum
    {
        [JsonProperty("album")]
        public JsonAlbum Album;

        public JsonWrapperAlbum()
        {
        }
    }
}