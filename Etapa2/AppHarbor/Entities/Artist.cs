using System;
using System.Collections.Generic;

namespace Entities
{
    public class Artist : Identity
    {
        public string Name { get; set; }
        public string Link { get; set; }

        public virtual List<Album> Albuns { get; set; }
        public virtual List<Track> Tracks { get; set; }

        public Artist()
        {
            Name = null;
            Albuns = new List<Album>();
            Tracks = new List<Track>();
        }

        public Artist(string name, string link, List<Album> a = null, List<Track> t = null)
        {
            Name = name;
            Albuns = a != null ? a : new List<Album>();
            Tracks = t != null ? t : new List<Track>();
            Link = link;
        }

        public override bool match(object o)
        {
            Artist ar = o as Artist;
            if (ar == null)
                throw new InvalidCastException();
            return ((ar.Name == null) || ar.Name.Equals(Name)) &&
                   ((ar.Albuns.Count == 0) || (ar.Albuns.Equals(Albuns))) &&
                   ((ar.Tracks.Count == 0) || (ar.Tracks.Equals(Tracks)));
        }
    }
}