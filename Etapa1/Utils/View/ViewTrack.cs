using System.Collections.Generic;
namespace Utils
{
    public class ViewTrack
    {
        public string Href { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public Dictionary<string,string> Artists { get; set; }
        public KeyValuePair<string,string> Album { get; set; }

        public ViewTrack(string href, string name, int duration, Dictionary<string,string> artists, string album, string albumHref)
        {
            Href = href;
            Name = name;
            Duration = duration;
            Artists = artists != null ? artists : new Dictionary<string, string>();
            Album = new KeyValuePair<string, string>(album, albumHref);
        }
    }
}
