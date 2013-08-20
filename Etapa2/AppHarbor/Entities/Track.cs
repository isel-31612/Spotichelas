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

        public int Order { get; set; }

        public int PlaylistId { get; set; }
        public virtual Playlist Playlist { get; set; }

        public Track()
        {
            Name = null;
            Duration = 0;
            Artist = new List<Artist>();
            Album = null;
            Playlist = null;
        }
        public Track(string name, string link, double duration, List<Artist> ar = null, Album al = null)
        {
            Name = name;
            Duration = (uint)duration;
            Artist = ar != null ? ar : new List<Artist>();
            Album = al;
            Link = link;
            Playlist = null;
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
