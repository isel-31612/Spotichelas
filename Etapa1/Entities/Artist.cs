using System;
using System.Collections.Generic;

namespace Entities
{
    public class Artist : Identity
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public virtual List<Album> Albuns { get; set; }

        public Artist()
        {
            Name = null;
            Albuns = new List<Album>();
        }

        public Artist(string name, string link, List<Album> a = null)
        {
            Name = name;
            Albuns = a != null ? a : new List<Album>();
            Link = link;
        }

        public override bool match(object o)
        {
            Artist ar = o as Artist;
            if (ar == null)
                throw new InvalidCastException();
            return ((ar.Name == null)   || ar.Name.Equals(Name))     &&
                   ((ar.Albuns.Count==0)|| (ar.Albuns.Equals(Albuns)));
        }
    }
}