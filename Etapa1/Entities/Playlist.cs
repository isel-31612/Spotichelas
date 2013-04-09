﻿using System;
using System.Collections.Generic;

namespace Entities
{
    public class Playlist : Identity
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public virtual List<Track> Tracks {get; set;}

        public Playlist(string name, string description)
        {
            Name = name;
            Description = description;
            Tracks = new List<Track>();
        }

        public override bool match(object o)
        {
            Playlist pl = o as Playlist;
            if (pl == null)
                throw new InvalidCastException();
            return ((pl.Name==null)         || pl.Name.Equals(Name)) &&
                    ((pl.Description==null) || pl.Description.Equals(Description)) &&
                    ((pl.Tracks.Count==0)   || pl.Tracks.Equals(Tracks));
        }
    }
}