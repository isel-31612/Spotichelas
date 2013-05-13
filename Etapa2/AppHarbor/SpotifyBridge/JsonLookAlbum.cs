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
        public List<JsonTrack> Tracks;
        [JsonProperty("href")]
        public string Link;
        [JsonProperty("availability", ItemIsReference = true)]
        public JsonAvaibility Availability;

        public JsonLookAlbum()
        { }

        public class JsonTrack
        {
            [JsonProperty("available")]
            public bool Available;
            [JsonProperty("href")]
            public string Link;
            [JsonProperty("artists")]
            public List<JsonArtist> Artists;
            [JsonProperty("name")]
            public string Name;

            public JsonTrack()
            { }

            public class JsonArtist
            {
                [JsonProperty("href")]
                public string Link;
                [JsonProperty("name")]
                public string Name;
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

    }
}