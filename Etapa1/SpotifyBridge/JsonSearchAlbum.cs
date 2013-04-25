using Newtonsoft.Json;
using System.Collections.Generic;

namespace SpotifyBridge
{
    public class JsonSearchAlbum
    {
        [JsonProperty("name")]
        public string Name;
        [JsonProperty("popularity")]
        public string Relevance;
        [JsonProperty("href")]
        public string Link;
        [JsonProperty("artists", ItemIsReference = true)]
        public List<JsonArtist> Artists;
        [JsonProperty("availability", ItemIsReference = true)]
        public JsonAvailability Availability;

        public JsonSearchAlbum() { }

        public class JsonArtist
        {
            [JsonProperty("name")]
            public string Name;
            [JsonProperty("href")]
            public string Link;

            public JsonArtist() { }
        }
        public class JsonAvailability
        {
            [JsonProperty("territories")]
            public string Territories;

            public JsonAvailability() { }
        }
    }
}
