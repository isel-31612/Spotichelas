using SpotiChelas.DomainModel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotiChelas.Controller;

namespace SpotiChelas
{
    public static class testRepo
    {
         public static List<Playlist> _repo = new List<Playlist>{
                new Playlist("PL 1", "desc da playlist 1"),
                new Playlist("PL_rockalhada", "description da playlist nr2")
            };
        public static void init(){
            int i = 1; foreach (Playlist p in _repo)
            {
                p.setId(i++);
                p.Tracks = new List<Track>();
                p.Tracks.Add(new Track("Track 1", 2344));
                p.Tracks.Add(new Track("Track 2", 6552));
                p.Tracks.Add(new Track("Track 3", 85152));
            }
        }
           
    }
    class Program
    {
        static void Main(string[] args)
        {
            var host = new WebGarten2.HttpHost("http://localhost:8080");

            testRepo.init();

            host.Add(WebGarten2.DefaultMethodBasedCommandFactory.GetCommandsFor(typeof(HomeController)));
            host.Add(WebGarten2.DefaultMethodBasedCommandFactory.GetCommandsFor(typeof(PlaylistController)));
            host.Open();
            Console.WriteLine("Server is running, press any key to continue...");
            Console.ReadKey();
            host.Close();
        }
    }
}
