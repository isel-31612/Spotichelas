using System;
using System.Collections.Generic;

namespace Entities
{
    public class Album : Identity
    {
        public string Name { get; set; }        
        public uint Year { get; set; }
        public string Link { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual List<Track> Tracks  { get; set; }

        public Album()
        {
            Name = null;
            Year = 0;
            Artist = null;
            Tracks = new List<Track>();
        }
        public Album(string name, uint year, List<Track> t=null, Artist a=null,string link = null)
        {
            Name = name;
            Year = year;
            Artist = a;
            Tracks = (t == null) ? new List<Track>() : t;
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
                   (al.Artist==null    || al.Artist.Equals(Artist));
        }
    }
}
