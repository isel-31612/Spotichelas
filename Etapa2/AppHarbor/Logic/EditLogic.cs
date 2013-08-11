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

        public bool AddTrack(ViewPlaylist vp, ViewTrack t, string CurrentUser)
        {
            var playlist = repo.get<Playlist>(vp.Id);
            Permission per;
            if (!playlist.Owner.Equals(CurrentUser) && (!playlist.Shared.TryGetValue(vp.Owner, out per) || !per.CanWrite))
                return false;
            if (playlist == null || playlist.Tracks.Values.Any(music => music.Href.Equals(t.Href)))
                return false;
            int nextId = playlist.Tracks.Count + 1;
            playlist.Tracks.Add(nextId, new Music(t.Href, t.Name));
            repo.update<Playlist>(vp.Id, playlist);
            return true;
        }

        public bool RemoveTrack(ViewPlaylist vp, string href, string CurrentUser)
        {
            var playlist = repo.get<Playlist>(vp.Id);
            Permission per;
            if (!playlist.Owner.Equals(CurrentUser) && (!playlist.Shared.TryGetValue(vp.Owner, out per) || !per.CanWrite))
                return false;
            if (playlist == null || playlist.Tracks.Any(kvp => kvp.Value.Href.Equals(href)))
                return false;
            int trackId = playlist.Tracks.Single(kvp => kvp.Value.Href.Equals(href)).Key;//Note: There cannot be 2 musics with the same Href in the same playlist(ther can be 2 equal musics, but they must have distinct href)
            playlist.Tracks.Remove(trackId);
            foreach(KeyValuePair<int,Music> kvp in playlist.Tracks){
                if (kvp.Key > trackId)
                {
                    playlist.Tracks.Remove(kvp.Key);//Note: Has to be done this way, since KeyValuePair Items cannot be moddifed, only read. So we remove EVERYTHING after the removed item, and re-add them with proper indexes
                    playlist.Tracks.Add(kvp.Key-1,kvp.Value);//Note: Throws exception if already exists the index.
                }
            }
            repo.update<Playlist>(vp.Id, playlist);
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