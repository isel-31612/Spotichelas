using Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Utils.View;
using System.Linq;

namespace Utils
{
    public class ViewPlaylist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
        public List<ViewPermission> Shared { get; set; }
        public List<ViewTrack> Tracks { get; set; }

        public ViewPlaylist()//Note: Only for LINQ query
        {
            Name = null;
            Description = null;
            Owner = null;
            Shared = new List<ViewPermission>();
            Tracks = new List<ViewTrack>();
        }

        public ViewPlaylist(Playlist pl)
        {
            Name = pl.Name;
            Id = pl.id;
            Description = pl.Description;
            Owner = pl.Owner;
            Shared = pl.Shared.Select(p => new ViewPermission(p)).ToList();
            Tracks = pl.Tracks.Select(t => new ViewTrack(t)).ToList();
        }
    }
}