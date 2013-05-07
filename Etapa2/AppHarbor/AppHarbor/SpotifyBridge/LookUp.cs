using Entities;
using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;
using WebCache;

namespace SpotifyBridge
{
    public class LookUp
    {
        private SpotifyInterpreter interpreter;

        public LookUp()
        {
            interpreter = new SpotifyInterpreter(new Cache<string, string>());
        }

        public Track Track(string id)
        {
            string link = string.Format("spotify:track:{0}", id);
            var json = interpreter.lookup(link);
            var obj = JsonConvert.DeserializeObject<Reply>(json).track;
            return obj.ToEntity();
        }

        public Artist Artist(string id)
        {
            string link = string.Format("spotify:artist:{0}", id);
            var json = interpreter.lookup(link, "album");
            var obj = JsonConvert.DeserializeObject<Reply>(json).artist;
            return obj.ToEntity();
        }

        public Album Album(string id)
        {
            string link = string.Format("spotify:album:{0}", id);
            var json = interpreter.lookup(link, "track");
            var obj = JsonConvert.DeserializeObject<Reply>(json).album;
            return obj.ToEntity();
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