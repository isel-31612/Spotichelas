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
            int id = editPlaylist.Id;
            var oldP = repo.get<Playlist>(id);
            Permission per;
            if (oldP == null || !oldP.Owner.Equals(CurrentUser) || (oldP.Shared.TryGetValue(CurrentUser, out per) && per.CanWrite))
                return null;
            Playlist p = new Playlist(editPlaylist.Name, editPlaylist.Description, CurrentUser);
            Playlist edit = repo.update<Playlist>(id, p);
            if ( edit!= null)
            {
                return new ViewPlaylist(id, edit.Name, edit.Description, edit.Owner,edit.Tracks);
            }
            return null;
        }

        public bool AddTrack(ViewPlaylist p, ViewTrack t, string CurrentUser)
        {
            var playlist = repo.get<Playlist>(p.Id);
            Permission per;
            if (!playlist.Owner.Equals(CurrentUser) && (!playlist.Shared.TryGetValue(p.Owner, out per) || !per.CanWrite))
                return false;
            if(playlist == null || playlist.Tracks.ContainsKey(t.Href))
                return false;
            playlist.Tracks.Add(t.Href, t.Name);
            repo.update<Playlist>(p.Id, playlist);
            return true;
        }

        public bool RemoveTrack(ViewPlaylist p, string href, string CurrentUser)
        {
            var playlist = repo.get<Playlist>(p.Id);
            Permission per;
            if (!playlist.Owner.Equals(CurrentUser) && (!playlist.Shared.TryGetValue(p.Owner, out per) || !per.CanWrite))
                return false;
            if (playlist == null || !playlist.Tracks.ContainsKey(href))
                return false;
            playlist.Tracks.Remove(href);
            repo.update<Playlist>(p.Id, playlist);
            return true;
        }

        public bool AddUser(ViewPlaylist p, string newUser,bool canRead, bool canWrite, string CurrentUser)
        {
            var playlist = repo.get<Playlist>(p.Id);
            if (playlist.Owner.Equals(CurrentUser))
            {
                if (playlist.Shared.ContainsKey(newUser))
                    playlist.Shared.Remove(newUser);
                playlist.Shared.Add(newUser, new Permission(canRead, canWrite));
                repo.update<Playlist>(p.Id,playlist);
                return true;
            }
            return false;
        }

        public bool RemoveUser(ViewPlaylist p, string oldUser, string CurrentUser)
        {
            var playlist = repo.get<Playlist>(p.Id);
            if (playlist.Owner.Equals(CurrentUser) && playlist.Shared.ContainsKey(oldUser))
            {
                playlist.Shared.Remove(oldUser);
                repo.update<Playlist>(p.Id, playlist);
                return true;
            }
            return false;
        }
    }
}