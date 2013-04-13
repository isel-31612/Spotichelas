using System.Collections.Generic;
namespace Utils
{
    public class ViewPlaylist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Tracks { get; set; }

        public ViewPlaylist(int id, string name, string description, List<string> tracks=null)
        {
            Id = id;
            Name = name;
            Description = description;
            Tracks = tracks;
        }
    }
}
