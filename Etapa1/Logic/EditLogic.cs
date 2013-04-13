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

        public ViewPlaylist PlaylistTo(EditPlaylist editPlaylist)
        {
            int id = editPlaylist.Id;
            Playlist edit = repo.get<Playlist>(id);
            if ( edit!= null)
            {
                if (editPlaylist.Name != null) edit.Name = editPlaylist.Name;
                if (editPlaylist.Description != null) edit.Description = editPlaylist.Description;
                if (editPlaylist.Tracks.Count <= 0) edit.Tracks = null;//TODO: get all tracks editPlaylist.Tracks;
                repo.update(id,edit); // id == edit.id
            }
            return new ViewPlaylist(id, editPlaylist.Name, editPlaylist.Description, editPlaylist.Tracks);
        }

        public ViewAlbum AlbumTo(EditAlbum editAlbum)
        {
            int id = editAlbum.Id;
            uint year = (uint)editAlbum.Year;
            Album edit = repo.get<Album>(id);
            if ( edit != null)
            {
                if (editAlbum.Name != null) edit.Name = editAlbum.Name;
                if (year != 0) edit.Year = year;
                repo.update(id, edit);
            }
            return new ViewAlbum(id, editAlbum.Name, editAlbum.Year, editAlbum.Artist, editAlbum.Tracks);
        }

        public ViewArtist ArtistTo(EditArtist editArtist)
        {
            int id = editArtist.Id;
            Artist edit = repo.get<Artist>(id);
            if (edit!= null)
            {
                if (editArtist.Name != null) edit.Name = editArtist.Name;
                repo.update(id, edit);
            }
            return new ViewArtist(id,editArtist.Name,editArtist.Albuns);
        }

        public ViewTrack TrackTo(EditTrack editTrack)
        {
            int id = editTrack.Id;
            uint duration = (uint)editTrack.Duration;
            Track edit = repo.get<Track>(id);
            if (edit != null)
            {
                if (editTrack.Name != null) edit.Name = editTrack.Name;
                if (duration != 0) edit.Duration = duration;
                repo.update(id, edit);
            }
            return new ViewTrack(id,editTrack.Name,editTrack.Duration,editTrack.Artist,editTrack.Album);
        }
    }
}