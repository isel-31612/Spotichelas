using System.Linq;
using System.Collections.Generic;

using DataAccess;
using Entities;
using Utils;

namespace BusinessRules
{
    public class EditLogic
    {
        private DAL repo;

        public EditLogic(DAL d)
        {
            repo = d;
        }

        public ViewPlaylist PlaylistTo(ViewPlaylist editPlaylist, string CurrentUser)
        {
            Playlist oldP = repo.get(editPlaylist.Id);
            if (oldP == null || !oldP.Owner.Equals(CurrentUser))
                return null;
            oldP.Name = editPlaylist.Name;
            oldP.Description = editPlaylist.Description;
            Playlist edit = repo.update(oldP);
            if ( edit!= null)
            {
                return new ViewPlaylist(edit);
            }
            return null;
        }

        public bool AddTrack(int id, string href, string CurrentUser)
        {
            Playlist p = repo.get(id);
            if (p==null || !p.Owner.Equals(CurrentUser) && (!p.Shared.Find( per=>per.User.Equals(CurrentUser)).CanWrite))
                return false;
            if (p.getTracks().Any(music => music.Link.Equals(href)))
                return false;
            Track track = repo.getTrack(href);
            if (track == null)
                return false;
            track.Order = p.getTracks().Count + 1;
            p.getTracks().Add(track);
            repo.addTrack(track);
            return true;
        }

        public bool RemoveTrack(int id, string href, string CurrentUser)
        {
            Playlist playlist = repo.get(id);
            if (playlist == null || !playlist.Owner.Equals(CurrentUser) && (!playlist.Shared.Find(per => per.User.Equals(CurrentUser)).CanWrite))
                return false;
            Track track = playlist.getTracks().Where( t=>t.Link.Equals(href) ).FirstOrDefault();
            if (track==null || !playlist.getTracks().Remove(track))
                return false;
            foreach (Track t in playlist.getTracks().Where(t => t.Order > track.Order))
            {
                t.Order = t.Order - 1;
            }
            repo.removeTrack(track,playlist);
            return true;
        }

        public bool AddUser(ViewPlaylist p, string newUser,bool canRead, bool canWrite, string CurrentUser)
        {
            Playlist playlist = repo.get(p.Id);
            if (playlist !=null && playlist.Owner.Equals(CurrentUser))
            {
                playlist.Shared.RemoveAll(permission => permission.User.Equals(newUser));
                playlist.Shared.Add(new Permission(newUser,canRead, canWrite));
                repo.update(playlist);
                return true;
            }
            return false;
        }

        public bool RemoveUser(ViewPlaylist p, string oldUser, string CurrentUser)
        {
            Playlist playlist = repo.get(p.Id);
            if (playlist!=null && playlist.Owner.Equals(CurrentUser) )
            {
                playlist.Shared.RemoveAll(per => per.User.Equals(oldUser));
                repo.update(playlist);
                return true;
            }
            return false;
        }
    }
}