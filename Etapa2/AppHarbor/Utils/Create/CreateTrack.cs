namespace Utils
{
    public class CreateTrack
    {
        public string Name { get; set; }
        public string Duration { get; set; }

        public CreateTrack(string name, string duration)
        {
            Name = name;
            Duration = duration;
        }
    }
}
