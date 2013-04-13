using DataAccess;
using Entities;
using Utils;

namespace BusinessRules
{
    public class FindLogic
    {
        private DAL repo;

        public FindLogic(DAL d)
        {
            repo = d;
        }

        public ViewPlaylist Playlist(int id)
        {
            var pl = repo.get<Playlist>(id);
            var vpl = new ViewPlaylist(pl.id,pl.Name,pl.Description);
            foreach (var track in pl.Tracks)
                vpl.Tracks.Add(track.Name);
            return vpl;
        }

        public ViewArtist Artist(int id)
        {
            var ar = repo.get<Artist>(id);
            var vAr = new ViewArtist(ar.id,ar.Name);
            foreach (var album in ar.Albuns)
                vAr.Albuns.Add(album.Name);
            return vAr;
        }
        public ViewAlbum Album(int id)
        {
            var al = repo.get<Album>(id);
            var val = new ViewAlbum(al.id,al.Name,(int)al.Year,al.Artist.Name,null);
            foreach (var track in al.Tracks)
                val.Tracks.Add(track.Name);
            val.Artist = al.Artist.Name;
            return val;
        }
        public ViewTrack Track(int id)
        {
            var t = repo.get<Track>(id);
            var vt = new ViewTrack(t.id,t.Name,(int)t.Duration,t.Artist.Name,t.Album.Name);
            return vt;
        }
    }
}
