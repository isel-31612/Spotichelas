using DataAccess;
using Entities;

namespace BusinessRules
{
    public class RemoveLogic
    {
        private DAL repo;
        public RemoveLogic(DAL d)
        {
            repo = d;
        }

        public Playlist Playlist(int id)
        {
            Playlist p = repo.get<Playlist>(id);
            if (p == null && p.Tracks.Count > 0)
                return null;
            return repo.remove<Playlist>(id);
        }
    }
}
