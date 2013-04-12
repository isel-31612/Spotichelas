using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace SpotifyBridge
{
    public class JsonArtist
    {
        [JsonProperty("albums", ItemIsReference = true)]
        public List<JsonWrapperAlbum> Albuns { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("href")]
        public string Link { get; set; }

        public JsonArtist()
        {
        }
    }
}
