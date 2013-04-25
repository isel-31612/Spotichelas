using System.Collections.Generic;
using System;
using Newtonsoft.Json;

namespace SpotifyBridge
{
    public class JsonLookAlbum
    {
        [JsonProperty("artist-id")]
        public string ArtistLink;
        [JsonProperty("name")]
        public string Name;
        [JsonProperty("artist")]
        public string ArtistName;
        [JsonProperty("released")]
        public int Year;
        [JsonProperty("tracks", ItemIsReference = true)]
        public List<JsonLookTrack> Tracks;
        [JsonProperty("href")]
        public string Link;
        [JsonProperty("availability", ItemIsReference = true)]
        public JsonAvaibility Availability;
        

        public JsonLookAlbum()
        {
        }

        public class JsonAvaibility
        {
            [JsonProperty("territories")]
            public string Territories;

            public JsonAvaibility()
            {
            }
        }

    }
}