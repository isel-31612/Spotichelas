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

        public void PlaylistTo(int id, Playlist p)          //TODO: mudar de argumentos para contentor de alteraPlaylist
        {
            Playlist edit = repo.get<Playlist>(id);
            if ( edit!= null)
            {
                if (p.Name != null)edit.Name = p.Name;
                if (p.Description != null)edit.Description = p.Description;
                if (p.Tracks.Count <= 0)edit.Tracks = p.Tracks;
                repo.update(id,edit); // id == edit.id
            }
        }

        public void AlbumTo(int id, Album a)                           //TODO: mudar de argumentos para contentor de alteraAlbum
        {
            Album edit = repo.get<Album>(id);
            if ( edit != null)
            {
                if (a.Name != null) edit.Name = a.Name;
                if (a.Year != 0) edit.Year = a.Year;
                repo.update(id, edit);
            }
        }

        public void ArtistTo(int id,Artist a)                        //TODO: mudar de argumentos para contentor de alteraArtista
        {
            Artist edit = repo.get<Artist>(id);
            if (edit!= null)
            {
                if (a.Name != null) edit.Name = a.Name;
                repo.update(id, edit);
            }
        }

        public void TrackTo(int id,Track t)                          //TODO: mudar de argumentos para contentor de alteraTrack
        {
            Track edit = repo.get<Track>(id);
            if (edit != null)
            {
                if (t.Name != null) edit.Name = t.Name;
                if (t.Duration != 0) edit.Duration = t.Duration;
                repo.update(id, edit);
            }
        }
    }
}