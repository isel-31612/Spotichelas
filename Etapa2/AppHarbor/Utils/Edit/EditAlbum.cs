using System.Collections.Generic;
namespace Utils
{
    public class EditAlbum
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public int Id { get; set; }
        public KeyValuePair<string,string> Artist { get; set; }
        public Dictionary<string, string> Tracks { get; set; }

        public EditAlbum(int id, string name, int year, string artistName, string artistLink, Dictionary<string, string> tracks = null)
        {
            Id = id;
            Name = name;
            Year = year;
            Artist = new KeyValuePair<string, string>(artistName,artistLink);
            Tracks = tracks != null ? tracks : new Dictionary<string, string>();
        }
    }
}
