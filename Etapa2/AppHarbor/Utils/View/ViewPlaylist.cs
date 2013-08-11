using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public Dictionary<string, Permission> Shared { get; set; }
        public SortedDictionary<int,Music> Tracks { get; set; }

        public ViewPlaylist(int id, string name, string description, string user, SortedDictionary<int, Music> tracks = null, Dictionary<string, Permission> shared = null)
        {
            Id = id;
            Name = name;
            Description = description;
            Owner = user;
            Shared = shared != null ? shared : new Dictionary<string, Permission>();
            Tracks = tracks != null ? tracks : new SortedDictionary<int, Music>();
        }
    }
}
