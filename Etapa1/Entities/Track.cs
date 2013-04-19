using System;
using System.Collections.Generic;

namespace Entities
{
    public class Track : Identity
    {
        public string Name { get; set; }
        public uint Duration { get; set; } // in seconds, [0...]
        public string Link { get; set; }
        public virtual List<Artist> Artist { get; set;}
        public virtual Album Album { get; set; }

        public Track()
        {
            Name = null;
            Duration = 0;
            Artist = null;
            Album = null;
        }
        public Track(string name, uint duration, List<Artist> ar=null, Album al = null, string link=null)//shift link to 1st and remove default
        {
            Name = name;
            Duration = duration;
            Artist = ar;
            Album = al;
            Link = link;
        }

        public override bool match(Object o)
        {
            Track t = o as Track;
            if (t == null)
                throw new InvalidCastException();
            return ((t.Name == null)   || t.Name.Equals(Name))         &&
                   ((t.Duration == 0)  || t.Duration.Equals(Duration)) &&
                   ((t.Artist == null) || t.Artist.Equals(Artist))     &&
                   ((t.Album == null)  || t.Album.Equals(Album));
        }
    }
}
