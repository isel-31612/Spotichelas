using System.Collections.Generic;

using Entities;

using Newtonsoft.Json;

namespace SpotifyBridge
{
    public class JsonSearchArtist
    {
        [JsonProperty("href")]
        public string Link;
        [JsonProperty("name")]
        public string Name;
        [JsonProperty("popularity")]
        public string Relevance;

        public JsonSearchArtist() { }
    }
}
