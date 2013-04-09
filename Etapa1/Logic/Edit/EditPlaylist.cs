using Entities;
using System.Collections.Generic;

namespace BusinessRules
{
    public class EditPlaylist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Track> Tracks { get; set; }

        public EditPlaylist(int id, string name, string description, List<Track> tracks=null)
        {
            Id = id;
            Name = name;
            Description = description;
            Tracks = tracks == null ? new List<Track>() : tracks;
        }
    }
}
