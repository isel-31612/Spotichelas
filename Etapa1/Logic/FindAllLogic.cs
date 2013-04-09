using DataAccess;
using Entities;
namespace BusinessRules
{
    public class FindAllLogic
    {
        private DAL repo;
        public FindAllLogic(DAL d)
        {
            repo = d;
        }

        public Playlist[] Playlists()
        {
            return repo.getAll<Playlist>();
        }
    }
}
