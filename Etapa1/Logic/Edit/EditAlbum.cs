namespace BusinessRules
{
    public class EditAlbum
    {
        public string Name { get; set; }
        public string Year { get; set; }
        public int Id { get; set; }

        public EditAlbum(int id, string name, string year)
        {
            Id = id;
            Name = name;
            Year = year;
        }
    }
}
