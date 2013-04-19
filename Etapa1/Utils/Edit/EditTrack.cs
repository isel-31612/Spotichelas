using System.Collections.Generic;
namespace Utils
{
    public class EditTrack
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public Dictionary<string, string> Artists { get; set; }
        public KeyValuePair<string,string> Album { get; set; }

        public EditTrack(int id, string name, int duration, string albumName, string albumLink, Dictionary<string, string> artists)
        {
            Id = id;
            Name = name;
            Duration = duration;
            Album = new KeyValuePair<string, string>(albumName, albumLink);
            Artists = artists != null ? artists : new Dictionary<string, string>();
        }
    }
}
