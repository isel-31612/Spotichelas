using System;
using System.Collections.Generic;

namespace Entities
{
    public class Album : Identity
    {
        public string Name { get; set; }        
        public uint Year { get; set; }
        public string Link { get; set; }
        
        public virtual List<Artist> Artists { get; set; }
        public virtual List<Track> Tracks  { get; set; }

        public Album()
        {
            Name = null;
            Year = 0;
            Artists = new List<Artist>();
            Tracks = new List<Track>();
        }
        public Album(string name, string link, int year = 0, List<Artist> a = null, List<Track> t = null)
        {
            Name = name;
            Year = (uint)year;
            Artists = a;
            Tracks = (t != null) ? t : new List<Track>();
            Link = link;
        }

        public override bool match(Object o)
        {
            Album al = o as Album;
            if (al == null)
                throw new InvalidCastException();
            return (al.Name==null      || al.Name.Equals(Name))          &&
                   (al.Year==0         || al.Year.Equals(Year))          &&
                   (al.Tracks.Count==0 || al.Tracks.Equals(this.Tracks)) &&
                   (al.Artists==null    || al.Artists.Equals(Artists));
        }
    }
}
