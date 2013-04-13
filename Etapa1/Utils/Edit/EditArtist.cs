using System.Collections.Generic;
namespace Utils
{
    public class EditArtist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Albuns { get; set; }

        public EditArtist(int id, string name,List<string> albuns = null)
        {
            Id = id;
            Name = name;
            Albuns = albuns;
        }
    }
}
