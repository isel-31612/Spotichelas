using DataAccess;
using Entities;

namespace BusinessRules
{
    public class EditLogic //TODO: Mudar para usando linguagem fluente adicionar parametros de procura(id, etc)
    {
        private DAL repo;

        public EditLogic(DAL d)
        {
            repo = d;
        }

        public void PlaylistTo(EditPlaylist editPlaylist)
        {
            int id = editPlaylist.Id;
            Playlist edit = repo.get<Playlist>(id);
            if ( edit!= null)
            {
                if (editPlaylist.Name != null) edit.Name = editPlaylist.Name;
                if (editPlaylist.Description != null) edit.Description = editPlaylist.Description;
                if (editPlaylist.Tracks.Count <= 0) edit.Tracks = editPlaylist.Tracks;
                repo.update(id,edit); // id == edit.id
            }
        }

        public void AlbumTo(EditAlbum editAlbum)
        {
            int id = editAlbum.Id;
            uint year = uint.Parse(editAlbum.Year);
            Album edit = repo.get<Album>(id);
            if ( edit != null)
            {
                if (editAlbum.Name != null) edit.Name = editAlbum.Name;
                if (year != 0) edit.Year = year;
                repo.update(id, edit);
            }
        }

        public void ArtistTo(EditArtist editArtist)
        {
            int id = editArtist.Id;
            Artist edit = repo.get<Artist>(id);
            if (edit!= null)
            {
                if (editArtist.Name != null) edit.Name = editArtist.Name;
                repo.update(id, edit);
            }
        }

        public void TrackTo(EditTrack editTrack)
        {
            int id = editTrack.Id;
            uint duration = uint.Parse(editTrack.Duration);
            Track edit = repo.get<Track>(id);
            if (edit != null)
            {
                if (editTrack.Name != null) edit.Name = editTrack.Name;
                if (duration != 0) edit.Duration = duration;
                repo.update(id, edit);
            }
        }
    }
}