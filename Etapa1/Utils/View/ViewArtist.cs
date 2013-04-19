using System.Collections.Generic;

namespace Utils
{
    public class ViewArtist
    {
        public string Href { get; set; }
        public string Name { get; set; }
        public Dictionary<string, string> Albuns { get; set; }

        public ViewArtist(string href, string name, Dictionary<string, string> albuns = null)
        {
            Href = href;
            Name = name;
            Albuns = albuns!=null?albuns:new Dictionary<string,string>();
        }
    }
}
