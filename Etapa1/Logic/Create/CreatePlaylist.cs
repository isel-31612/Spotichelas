namespace BusinessRules
{
    public class CreatePlaylist
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public CreatePlaylist(string name,string description)
        {
            Name = name;
            Description = description;
        }
    }
}
