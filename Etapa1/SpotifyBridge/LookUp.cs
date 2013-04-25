using Entities;
using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

namespace SpotifyBridge
{
    public class LookUp
    {
        private SpotifyInterpreter interpreter;

        public LookUp()
        {
            interpreter = new SpotifyInterpreter();
        }

        public Track Track(string id)
        {
            string link = string.Format("spotify:track:{0}", id);
            var json = interpreter.lookup(link);
            var obj = JsonConvert.DeserializeObject<Reply>(json).track;
            Album al = new Album(obj.Album.Name, obj.Album.Link, obj.Album.Year);
            List<Artist> ar = obj.Artist.Select( x => new Artist(x.Name,x.Link)).ToList();
            return new Track(obj.Name, obj.Link, obj.Duration, ar, al);
        }

        public Artist Artist(string id)
        {
            string link = string.Format("spotify:artist:{0}", id);
            var json = interpreter.lookup(link, "album");
            var obj = JsonConvert.DeserializeObject<Reply>(json).artist;
            IEnumerable<Album> a = obj.Albuns.Select(x => new Album(x.Album.Name, x.Album.Link));
            return new Artist(obj.Name, obj.Link, a.ToList());
        }

        public Album Album(string id)
        {
            string link = string.Format("spotify:album:{0}", id);
            var json = interpreter.lookup(link, "track");
            var obj = JsonConvert.DeserializeObject<Reply>(json).album;
            IEnumerable<Track> t = obj.Tracks.Select(x => new Track(x.Name, x.Link, x.Duration));
            List<Artist> a = new List<Artist>();
            a.Add(new Artist(obj.ArtistName, obj.ArtistLink));
            return new Album(obj.Name, obj.Link, obj.Year, a,t.ToList());
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

    public class ReplyType
    {
        [JsonProperty("type")]
        public string Replytype { get; set; }
    }
}