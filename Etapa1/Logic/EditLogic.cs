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
    }
}