using Newtonsoft.Json;
using System.Collections.Generic;
namespace SpotifyBridge
{
    public class JsonTrack
    {
        [JsonProperty("href")]
        public string Link;
        [JsonProperty("name")]
        public string Name;
        [JsonProperty("artists", ItemIsReference = true)]
        public List<JsonArtist> Artist;
        [JsonProperty("album", ItemIsReference = true)]
        public JsonAlbum Album;
        [JsonProperty("available")]
        public bool Available;
        [JsonProperty("track-number")]
        public int TrackNumber;
        [JsonProperty("length")]
        public double Duration;
        [JsonProperty("popularity")]
        public double Popularity;

        public JsonTrack()
        {
        }
    }
}