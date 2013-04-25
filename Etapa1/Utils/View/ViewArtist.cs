using System.Collections.Generic;

namespace Utils
{
    public class ViewArtist
    {
        public string Href { get; set; }
        public string Name { get; set; }
        public List<KeyValuePair<string, string>> Albuns { get; set; }

        public ViewArtist(string href, string name, List<KeyValuePair<string, string>> albuns = null)
        {
            Href = href;
            Name = name;
            Albuns = albuns != null ? albuns : new List<KeyValuePair<string, string>>();
        }
    }
}
