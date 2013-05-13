using System.Collections.Generic;

namespace Utils
{
    public class ViewAlbum
    {
        public string Href { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public List<KeyValuePair<string, string>> Artist { get; set; }
        public List<KeyValuePair<string, string>> Tracks { get; set; }

        public ViewAlbum(string href, string name, int year, List<KeyValuePair<string, string>> artist, List<KeyValuePair<string, string>> tracks = null)
        {
            Href = href;
            Name = name;
            Year = year+"";
            Artist = artist != null ? artist : new List<KeyValuePair<string, string>>();
            Tracks = tracks != null ? tracks : new List<KeyValuePair<string, string>>();
        }
    }
}
