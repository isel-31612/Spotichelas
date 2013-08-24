using Entities;
using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public class ViewArtist
    {
        public string Href { get; set; }
        public string Name { get; set; }
        public List<KeyValuePair<string, string>> Albuns { get; set; }

        public ViewArtist()
        {
            Href = null;
            Name = null;
            Albuns = new List<KeyValuePair<string, string>>();
        }

        public ViewArtist(Artist artist)
        {
            Href = artist.Link;
            Name = artist.Name;
            Albuns = artist.Albuns.Select(a => new KeyValuePair<string, string>(a.Link, a.Name)).ToList();
        }
    }
}
