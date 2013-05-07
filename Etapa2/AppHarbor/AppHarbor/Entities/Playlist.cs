using System;
using System.Collections.Generic;

namespace Entities
{
    public class Playlist : Identity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
        public virtual Dictionary<string, Permission> Shared { get; set; }
        public virtual Dictionary<string, string> Tracks {get; set;}

        public Playlist(string name, string description, string user)
        {
            Name = name;
            Description = description;
            Owner = user;
            Tracks = new Dictionary<string, string>();
            Shared = new Dictionary<string, Permission>();
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

    public class Permission
    {
        public bool CanRead { get; set; }
        public bool CanWrite { get; set; }

        public Permission(bool read, bool write)
        {
            CanRead = read;
            CanWrite = write;
        }
    }
}