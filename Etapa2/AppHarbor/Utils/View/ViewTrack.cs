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
        public List<KeyValuePair<string, string>> Artists { get; set; }
        public KeyValuePair<string,string> Album { get; set; }

        public ViewTrack(string href, string name, int duration, List<KeyValuePair<string, string>> artists, string album, string albumHref)
        {
            Href = href;
            Name = name;
            Duration = duration+"";
            Artists = artists != null ? artists : new List<KeyValuePair<string, string>>();
            Album = new KeyValuePair<string, string>(albumHref, album);
        }

        public ViewTrack(Track track)
        {
            Href = track.Link;
            Name = track.Name;
            Duration = track.Duration+"";
            Artists = track.Artist.Select(a => new KeyValuePair<string, string>(a.Link,a.Name)).ToList();
            Album = new KeyValuePair<string,string>(track.Album.Link,track.Album.Name);
        }

        public Track ToTrack()
        {
            return new Track(Name, Href, double.Parse(Duration), null, null);
        }
    }
}
