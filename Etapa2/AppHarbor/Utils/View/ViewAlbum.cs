using Entities;
using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public class ViewAlbum
    {
        public string Href { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public List<KeyValuePair<string, string>> Artist { get; set; }
        public List<KeyValuePair<string, string>> Tracks { get; set; }

        public ViewAlbum()
        {
            Href = null;
            Name = null;
            Year = null;
            Artist = new List<KeyValuePair<string, string>>();
            Tracks = new List<KeyValuePair<string, string>>();
        }

        public ViewAlbum(Album album)
        {
            Href = album.Link;
            Name = album.Name;
            Year = album.Year + "";
            Artist = album.Artists.Select(a => new KeyValuePair<string, string>(a.Link, a.Name)).ToList();
            Tracks = album.Tracks.Select(t => new KeyValuePair<string, string>(t.Link, t.Name)).ToList();
        }
    }
}
