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

        public ViewPlaylist Playlist(int id, string user) //TODO: check user
        {
            Playlist p = repo.get<Playlist>(id);
            if (p == null || p.Tracks.Count > 0 || !p.Owner.Equals(user))
                return null;
            var old = repo.remove<Playlist>(id);
            var removed = new ViewPlaylist(old.id, old.Name, old.Description,old.Owner);
            foreach( var track in old.Tracks)
                removed.Tracks.Add(track.Key,track.Value);
            return removed;
        }
    }
}