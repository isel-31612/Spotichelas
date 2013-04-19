using System.Collections.Generic;

namespace Utils
{
    public class ViewAlbum
    {
        public string Href { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public KeyValuePair<string, string> Artist { get; set; }
        public Dictionary<string, string> Tracks { get; set; }

        public ViewAlbum(string href, string name, int year, string artistName,string artistLink, Dictionary<string, string> tracks = null)
        {
            Href = href;
            Name = name;
            Year = year;
            Artist = new KeyValuePair<string, string>(artistName,artistLink);
            Tracks = tracks!=null?tracks:new Dictionary<string, string>();
        }
    }
}
