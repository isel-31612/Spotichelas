using System.Collections.Generic;
namespace Utils
{
    public class ViewTrack
    {
        public string Href { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }
        public List<KeyValuePair<string, string>> Artists { get; set; }
        public KeyValuePair<string,string> Album { get; set; }

        public ViewTrack(string href, string name, int duration, List<KeyValuePair<string, string>> artists, string album, string albumHref)
        {
            Href = href;
            Name = name;
            Duration = duration+"";
            Artists = artists != null ? artists : new List<KeyValuePair<string, string>>();
            Album = new KeyValuePair<string, string>(albumHref, album);
        }
    }
}
