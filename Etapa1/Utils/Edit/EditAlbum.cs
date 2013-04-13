using System.Collections.Generic;
namespace Utils
{
    public class EditAlbum
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public int Id { get; set; }
        public string Artist { get; set; }
        public List<string> Tracks { get; set; }

        public EditAlbum(int id, string name, int year, string artist,List<string> tracks = null)
        {
            Id = id;
            Name = name;
            Year = year;
            Artist = artist;
            Tracks = tracks;
        }
    }
}
