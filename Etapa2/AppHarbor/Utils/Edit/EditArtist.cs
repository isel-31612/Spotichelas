using System.Collections.Generic;
namespace Utils
{
    public class EditArtist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Dictionary<string, string> Albuns { get; set; }

        public EditArtist(int id, string name, Dictionary<string, string> albuns = null)
        {
            Id = id;
            Name = name;
            Albuns = albuns != null ? albuns : new Dictionary<string, string>();
        }
    }
}
