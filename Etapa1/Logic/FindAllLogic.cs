using DataAccess;
using Entities;
using System.Collections.Generic;
using Utils;

namespace BusinessRules
{
    public class FindAllLogic
    {
        private DAL repo;
        public FindAllLogic(DAL d)
        {
            repo = d;
        }

        public ViewPlaylist[] Playlists()
        {
            var pl = repo.getAll<Playlist>();
            List<ViewPlaylist> list = new List<ViewPlaylist>();
            foreach(var p in pl){
                var vp = new ViewPlaylist(p.id, p.Name, p.Description);
                foreach (var track in p.Tracks)
                    vp.Tracks.Add(track.Name);
                list.Add(vp);
            }
            return list.ToArray();
        }
    }
}
