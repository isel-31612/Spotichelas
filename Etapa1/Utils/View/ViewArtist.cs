using System.Collections.Generic;

namespace Utils
{
    public class ViewArtist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Albuns { get; set; }

        public ViewArtist(int id, string name, List<string> albuns=null)
        {
            Id = id;
            Name = name;
            Albuns = albuns;
        }
    }
}
