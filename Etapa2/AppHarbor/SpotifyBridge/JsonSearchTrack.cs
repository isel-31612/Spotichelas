using Newtonsoft.Json;
using System.Collections.Generic;

namespace SpotifyBridge
{
    public class JsonSearchTrack
    {
        [JsonProperty("album", ItemIsReference = true)]
        public JsonAlbum Album;
        [JsonProperty("name")]
        public string Name;
        [JsonProperty("popularity")]
        public string Relevance;
        [JsonProperty("length")]
        public double Duration;
        [JsonProperty("href")]
        public string Link;
        [JsonProperty("artists", ItemIsReference = true)]
        public List<JsonArtist> Artists;
        [JsonProperty("track-number")]
        public string Number;

        public JsonSearchTrack(){}

        public class JsonAlbum
        {
            [JsonProperty("released")]
            public string Year;
            [JsonProperty("href")]
            public string Link;
            [JsonProperty("name")]
            public string Name;
            [JsonProperty("availability", ItemIsReference = true)]
            public JsonAvailability Availability;

            public JsonAlbum() { }

            public class JsonAvailability
            {
                [JsonProperty("territories")]
                public string Territories;

                public JsonAvailability() { }
            }
        }
        public class JsonArtist
        {
            [JsonProperty("href")]
            public string Link;
            [JsonProperty("name")]
            public string Name;
        }
    }
}