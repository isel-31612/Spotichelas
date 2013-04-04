using Entities;

namespace Logic
{
    public class Remove
    {
        public void Playlist(Playlist p)
        {
            p.delete(true);
        }

        public void Album(Album a)
        {
            a.delete(true);
        }

        public void Artist(Artist a)
        {
            a.delete(true);
        }

        public void Track(Track t)
        {
            t.delete(true);
        }
    }
}