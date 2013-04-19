using System.Collections.Generic;
namespace Utils
{
    public class ViewPlaylist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Dictionary<string,string> Tracks { get; set; }

        public ViewPlaylist(int id, string name, string description, Dictionary<string, string> tracks = null)
        {
            Id = id;
            Name = name;
            Description = description;
            Tracks = tracks != null ? tracks : new Dictionary<string, string>();
        }
    }
}
