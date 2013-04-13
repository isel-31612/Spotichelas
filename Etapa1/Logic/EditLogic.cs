using DataAccess;
using Entities;
using Utils;
using System.Linq;
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
                if (editPlaylist.Tracks.Count <= 0) edit.Tracks = editPlaylist.Tracks.Select(x => new Track(x, 0)).ToList();//TODO: find the diference, and search ONLY the diferences  editPlaylist.Tracks;
                repo.update<Playlist>(id, edit);
                return new ViewPlaylist(id, editPlaylist.Name, editPlaylist.Description, editPlaylist.Tracks);
            }
            return null;
        }

        public ViewAlbum AlbumTo(EditAlbum editAlbum)
        {
            int id = editAlbum.Id;
            Album edit = repo.get<Album>(id);
            if ( edit != null)
            {
                if (editAlbum.Name != null) edit.Name = editAlbum.Name;
                if (editAlbum.Year != 0) edit.Year = (uint)editAlbum.Year;
                if (editAlbum.Artist != null) edit.Artist = new Artist(editAlbum.Artist); //TODO: Lookup.Artist(editAlbum.Artist);
                if (editAlbum.Tracks.Count != 0) edit.Tracks = editAlbum.Tracks.Select(x => new Track(x, 0)).ToList();//TODO: find the diference, and search ONLY the diferences Lookup.Artist(editAlbum.Tracks).Tracks;
                repo.update<Album>(id, edit);               
                return new ViewAlbum(id, editAlbum.Name, editAlbum.Year, editAlbum.Artist, editAlbum.Tracks);
            }
            return null;
        }

        public ViewArtist ArtistTo(EditArtist editArtist)
        {
            int id = editArtist.Id;
            Artist edit = repo.get<Artist>(id);
            if (edit!= null)
            {
                if (editArtist.Name != null) edit.Name = editArtist.Name;
                if (editArtist.Albuns.Count <= 0) edit.Albuns = editArtist.Albuns.Select(x => new Album(x, 0)).ToList();//TODO: find the diference, and search ONLY the diferences 
                repo.update<Artist>(id, edit);
                return new ViewArtist(id, editArtist.Name, editArtist.Albuns);
            }
            return null;
        }

        public ViewTrack TrackTo(EditTrack editTrack)
        {
            int id = editTrack.Id;
            Track edit = repo.get<Track>(id);
            if (edit != null)
            {
                if (editTrack.Name != null) edit.Name = editTrack.Name;
                if (editTrack.Duration != 0) edit.Duration = (uint)editTrack.Duration;;
                if (editTrack.Album != null && !editTrack.Album.Equals(edit.Album.Name)) edit.Album = new Album(editTrack.Album, 0); //TODO: Lookup.Album(editTrack.Album)
                if (editTrack.Artist != null && !editTrack.Artist.Equals(edit.Artist.Name)) edit.Artist = new Artist(editTrack.Artist);  //TODO: Lookup.Album(editTrack.Album)
                repo.update<Track>(id, edit);
                return new ViewTrack(id,editTrack.Name,editTrack.Duration,editTrack.Artist,editTrack.Album);
            }
            return null;
        }
    }
}