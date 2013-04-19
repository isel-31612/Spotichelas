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

        public ViewPlaylist Playlist(int id)
        {
            Playlist p = repo.get<Playlist>(id);
            if (p == null && p.Tracks.Count > 0)
                return null;
            var old = repo.remove<Playlist>(id);
            var removed = new ViewPlaylist(old.id, old.Name, old.Description);
            foreach( var track in old.Tracks)
                removed.Tracks.Add(track.Key,track.Value);
            return removed;
        }
    }
}
