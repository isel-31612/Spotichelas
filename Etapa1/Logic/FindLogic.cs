using DataAccess;
using Entities;

namespace BusinessRules
{
    public class FindLogic
    {
        private DAL repo;

        public FindLogic(DAL d)
        {
            repo = d;
        }

        public Playlist Playlist(int id)
        {
            return repo.get<Playlist>(id);
        }

        public Artist Artist(int id)
        {
            return repo.get<Artist>(id);
        }
        public Album Album(int id)
        {
            return repo.get<Album>(id);
        }
        public Track Track(int id)
        {
            return repo.get<Track>(id);
        }
    }
}
