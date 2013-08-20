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
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string Owner { get; set; }
        public List<ViewPermission> Shared { get; set; }
        public Dictionary<int,ViewTrack> Tracks { get; set; }

        public ViewPlaylist()//Note: Only for LINQ query
        {
            Name = null;
            Description = null;
            Owner = null;
            Shared = new List<ViewPermission>();
            Tracks = new Dictionary<int,ViewTrack>();
        }

        public ViewPlaylist(Playlist pl)
        {
            Name = pl.Name;
            Id = pl.id;
            Description = pl.Description;
            Owner = pl.Owner;
            Shared = pl.Shared.Select(p => new ViewPermission(p)).ToList();
            Tracks = new Dictionary<int, ViewTrack>();
            foreach (var track in pl.Tracks)
            {
                Tracks.Add(track.Order, new ViewTrack(track));
            }
        }
    }
}