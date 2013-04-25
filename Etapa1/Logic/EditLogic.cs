using DataAccess;
using Entities;
using Utils;
using System.Linq;
using System.Collections.Generic;
namespace BusinessRules
{
    public class EditLogic
    {
        private DAL repo;

        public EditLogic(DAL d)
        {
            repo = d;
        }

        public ViewPlaylist PlaylistTo(EditPlaylist editPlaylist)
        {
            int id = editPlaylist.Id;
            Playlist p = new Playlist(editPlaylist.Name, editPlaylist.Description);
            Playlist edit = repo.update<Playlist>(id, p);
            if ( edit!= null)
            {
                return new ViewPlaylist(id, edit.Name, edit.Description,edit.Tracks);
            }
            return null;
        }

        public bool AddTrack(ViewPlaylist p,ViewTrack t)
        {
            var playlist = repo.get<Playlist>(p.Id);
            if(playlist == null || playlist.Tracks.ContainsKey(t.Href))
                return false;
            playlist.Tracks.Add(t.Href, t.Name);
            return true;
        }

        public bool RemoveTrack(ViewPlaylist p, ViewTrack t)
        {
            var playlist = repo.get<Playlist>(p.Id);
            if (playlist == null || !playlist.Tracks.ContainsKey(t.Href))
                return false;
            playlist.Tracks.Remove(t.Href);
            return true;
        }
    }
}