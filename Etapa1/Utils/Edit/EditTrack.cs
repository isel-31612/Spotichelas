namespace Utils
{
    public class EditTrack
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }

        public EditTrack(int id, string name, int duration,string artist, string album)
        {
            Id = id;
            Name = name;
            Duration = duration;
            Album = album;
            Artist = artist;
        }
    }
}
