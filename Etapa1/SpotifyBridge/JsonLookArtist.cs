using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace SpotifyBridge
{
    public class JsonLookArtist
    {
        [JsonProperty("albums", ItemIsReference = true)]
        public List<JsonWrapperAlbum> Albuns { get; set; }
        [JsonProperty("href")]
        public string Link { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        public JsonLookArtist()
        {
        }

        public class JsonWrapperAlbum
        {
            [JsonProperty("album", ItemIsReference = true)]
            public JsonAlbum Album;

            public JsonWrapperAlbum()
            {
            }

            public class JsonAlbum
            {
                [JsonProperty("artist-id")]
                public string ArtistLink;
                [JsonProperty("name")]
                public string Name;
                [JsonProperty("artist")]
                public string ArtistName;
                [JsonProperty("href")]
                public string Link;
                [JsonProperty("availability")]
                public JsonAvailability Availability;

                public JsonAlbum() { }

                public class JsonAvailability
                {
                    [JsonProperty("territories")]
                    public string Territories;

                    public JsonAvailability() { }
                }
            }
        }
    }
}