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

        public bool Playlist(int id, string CurrentUser)
        {
            Playlist p = repo.get(id);
            if (p == null || p.getTracks().Count > 0 || !p.Owner.Equals(CurrentUser))
                return false;
            Playlist deleted = repo.remove(p);
            return deleted!=null;
        }
    }
} 