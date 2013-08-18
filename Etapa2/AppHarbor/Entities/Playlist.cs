using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
    public class Playlist : Identity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
        public virtual List<Permission> Shared { get; set; }
        public virtual Dictionary<int,Track> Tracks { get; set; }
        public List<Track> DbTracks { get { return Tracks.Values.ToList(); } }

        public Playlist()
        {
            Name = null;
            Description = null;
            Owner = null;
            Tracks = new Dictionary<int,Track>();
            Shared = new List<Permission>();
        }

        public Playlist(string name, string description, string user)
        {
            Name = name;
            Description = description;
            Owner = user;
            Tracks = new Dictionary<int,Track>();
            Shared = new List<Permission>();
        }

        public override bool match(object o)
        {
            Playlist pl = o as Playlist;
            if (pl == null)
                throw new InvalidCastException();
            return  ((pl.Name==null)        || pl.Name.Equals(Name)) &&
                    ((pl.Description==null) || pl.Description.Equals(Description)) &&
                    ((pl.Tracks.Count==0)   || pl.Tracks.Equals(Tracks));
        }
    }
}