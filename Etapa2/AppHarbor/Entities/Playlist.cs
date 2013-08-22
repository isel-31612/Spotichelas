﻿using System;
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
        public virtual List<Track> HideTracks { get; set; }

        public Playlist()
        {
            Name = null;
            Description = null;
            Owner = null;
            HideTracks = new List<Track>();
            Shared = new List<Permission>();
        }

        public Playlist(string name, string description, string user)
        {
            Name = name;
            Description = description;
            Owner = user;
            HideTracks = new List<Track>();
            Shared = new List<Permission>();
        }

        public ICollection<Track> getTracks(){
            return HideTracks;
        }

        public void setTracks(List<Track> c){
            HideTracks = c;
        }

        public override bool match(object o)
        {
            Playlist pl = o as Playlist;
            if (pl == null)
                throw new InvalidCastException();
            return ((pl.Name == null) || pl.Name.Equals(Name)) &&
                    ((pl.Description == null) || pl.Description.Equals(Description));/* &&
                    ((pl.Tracks.Count==0)   || pl.Tracks.Equals(Tracks));*/
        }
    }
}