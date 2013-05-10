using Entities;
using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;
using WebManager;

namespace SpotifyBridge
{
    public class LookUp
    {
        private HttpInterpreter interpreter;

        public LookUp()
        {
            interpreter = new SpotifyInterpreter();
        }

        public Track Track(string id)
        {
            string link = string.Format("spotify:track:{0}", id);
            var json = lookup(link);
            var obj = JsonConvert.DeserializeObject<Reply>(json).track;
            return obj.ToEntity();
        }

        public Artist Artist(string id)
        {
            string link = string.Format("spotify:artist:{0}", id);
            var json = lookup(link, "album");
            var obj = JsonConvert.DeserializeObject<Reply>(json).artist;
            return obj.ToEntity();
        }

        public Album Album(string id)
        {
            string link = string.Format("spotify:album:{0}", id);
            var json = lookup(link, "track");
            var obj = JsonConvert.DeserializeObject<Reply>(json).album;
            return obj.ToEntity();
        }

        public string lookup(string id, string detail = null)
        {
            string request = "http://ws.spotify.com/lookup/1/{0}?uri={1}{2}";
            string requestType = ".json";
            string requestId = id;
            string requestExtras = detail != null ? string.Format("&extras={0}", detail) : "";
            string requestUri = string.Format(request, requestType, requestId, requestExtras);
            return interpreter.GetResponse(requestUri);
        }
    }

    public class Reply
    {
        [JsonProperty("artist", ItemIsReference = true)]
        public JsonLookArtist artist { get; set; }
        [JsonProperty("album", ItemIsReference = true)]
        public JsonLookAlbum album { get; set; }
        [JsonProperty("track", ItemIsReference = true)]
        public JsonLookTrack track { get; set; }
    }
}