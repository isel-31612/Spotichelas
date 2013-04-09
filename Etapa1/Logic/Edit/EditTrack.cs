namespace BusinessRules
{
    public class EditTrack
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }

        public EditTrack(int id, string name, string duration)
        {
            Id = id;
            Name = name;
            Duration = duration;
        }
    }
}
