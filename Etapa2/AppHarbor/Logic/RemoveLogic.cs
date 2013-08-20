using DataAccess;
using Entities;
using Utils;

namespace BusinessRules
{
    public class RemoveLogic
    {
        private DAL repo;
        public RemoveLogic(DAL d)
        {
            repo = d;
        }

        public ViewPlaylist Playlist(int id, string CurrentUser)
        {
            Playlist p = repo.get(id);
            if (p == null || p.Tracks.Count > 0 || !p.Owner.Equals(CurrentUser))
                return null;
            Playlist old = repo.remove(id);
            return new ViewPlaylist(old);
        }
    }
}