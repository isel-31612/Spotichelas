using Entities;
using System.Collections.Generic;
using System.Linq;
namespace Utils
{
    public class ViewTrack
    {
        public string Href { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }
        public int Order { get; set; }
        public List<KeyValuePair<string, string>> Artists { get; set; }
        public KeyValuePair<string,string> Album { get; set; }

        public ViewTrack()
        {
            Href = null;
            Name = null;
            Duration = null;
            Order = 0;
            Artists = new List<KeyValuePair<string, string>>();
            Album = new KeyValuePair<string, string>();
        }

        public ViewTrack(Track track)
        {
            Href = track.Link;
            Name = track.Name;
            Duration = track.Duration+"";
            Order = track.Order;
            Artists = track.Artist.Select(a => new KeyValuePair<string, string>(a.Link,a.Name)).ToList();
            Album = new KeyValuePair<string, string>(track.Album.Link,track.Album.Name);
        }
    }
}
