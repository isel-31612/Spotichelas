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
            Playlist oldP = repo.get<Playlist>(editPlaylist.Id);
            if (oldP == null || !oldP.Owner.Equals(CurrentUser))
                return null;
            Playlist p = new Playlist(editPlaylist.Name, editPlaylist.Description, CurrentUser); //CurrentUser=oldP.Owner
            Playlist edit = repo.update<Playlist>(editPlaylist.Id, p);
            if ( edit!= null)
            {
                return new ViewPlaylist(edit);
            }
            return null;
        }

        public bool AddTrack(ViewPlaylist vp, ViewTrack t, string CurrentUser)
        {
            Playlist p = repo.get<Playlist>(vp.Id);
            if (p==null || !p.Owner.Equals(CurrentUser) && (!p.Shared.Find( per=>per.User.Equals(CurrentUser)).CanWrite))
                return false;
            if (p == null || p.Tracks.Any(music => music.Value.Link.Equals(t.Href)))
                return false;
            Track track = repo.getTrack(t.Href);
            int nextId=p.Tracks.Count+1;
            p.Tracks.Add(nextId,track);
            repo.update<Playlist>(vp.Id, p);
            return true;
        }

        public bool RemoveTrack(ViewPlaylist vp, string href, string CurrentUser)
        {
            Playlist playlist = repo.get<Playlist>(vp.Id);
            if (playlist == null || !playlist.Owner.Equals(CurrentUser) && (!playlist.Shared.Find(per => per.User.Equals(CurrentUser)).CanWrite))
                return false;
            var track = playlist.Tracks.Where( t=>t.Value.Link.Equals(href) ).FirstOrDefault();
            if (track.Value!=null || playlist.Tracks.Remove(track.Key))
                return false;
            playlist.Tracks.Remove(track.Key);
            repo.update<Playlist>(vp.Id, playlist);
            return true;
        }

        public bool AddUser(ViewPlaylist p, string newUser,bool canRead, bool canWrite, string CurrentUser)
        {
            Playlist playlist = repo.get<Playlist>(p.Id);
            if (playlist !=null && playlist.Owner.Equals(CurrentUser))
            {
                playlist.Shared.RemoveAll(permission => permission.User.Equals(newUser));
                playlist.Shared.Add(new Permission(newUser,canRead, canWrite));
                repo.update<Playlist>(p.Id,playlist);
                return true;
            }
            return false;
        }

        public bool RemoveUser(ViewPlaylist p, string oldUser, string CurrentUser)
        {
            Playlist playlist = repo.get<Playlist>(p.Id);
            if (playlist!=null && playlist.Owner.Equals(CurrentUser) )
            {
                playlist.Shared.RemoveAll(per => per.User.Equals(oldUser));
                repo.update<Playlist>(p.Id, playlist);
                return true;
            }
            return false;
        }
    }
}