namespace Utils
{
    public class CreateAlbum
    {
        public string Name { get; set; }
        public string Year { get; set; }

        public CreateAlbum(string name, string year)
        {
            Name = name;
            Year = year;
        }
    }
}
