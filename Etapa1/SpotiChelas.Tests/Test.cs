using Entities;
using DataAccess;
using System.Collections.Generic;

namespace SpotiChelas.Tests
{
    public class Test_Init_Repo
    {
        public static class testRepo
        {
            public static List<Playlist> _repo = new List<Playlist>{
                new Playlist("PL 1", "desc da playlist 1"),
                new Playlist("PL_rockalhada", "description da playlist nr2")
            };

            public static void Main()
            {
                int i = 1; foreach (Playlist p in _repo)
                {
                    p.setId(i++);
                    p.Tracks = new List<Track>();
                    p.Tracks.Add(new Track("Track 1", 2344));
                    p.Tracks.Add(new Track("Track 2", 6552));
                    p.Tracks.Add(new Track("Track 3", 85152));
                }

                DAL.putAll(_repo.ToArray());
                Program.Main(null);
            }
        }
    }
}
