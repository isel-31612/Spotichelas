using DataAccess;
using Entities;
using System.Collections.Generic;
using Utils;
using System.Linq;

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
            return new ViewPlaylist(pl.id,pl.Name,pl.Description,pl.Tracks);
        }

        public ViewArtist Artist(string link)
        {
            var ar = repo.getArtist(link);
            var vAr = new ViewArtist(simpleHref(ar.Link), ar.Name);
            foreach (var album in ar.Albuns)
                vAr.Albuns.Add(simpleHref(album.Link), album.Name);
            return vAr;
        }
        public ViewAlbum Album(string link)
        {
            var al = repo.getAlbum(link);
            var val = new ViewAlbum(simpleHref(al.Link), al.Name, (int)al.Year, al.Artist.Name, simpleHref(al.Artist.Link));
            foreach (var track in al.Tracks)
                val.Tracks.Add(simpleHref(track.Link), track.Name);
            return val;
        }
        public ViewTrack Track(string link)
        {
            var t = repo.getTrack(link);
            var artists = t.Artist.Select(x => new { Key = simpleHref(x.Link), Value = x.Name }).ToDictionary(x => x.Key, x => x.Value);
            var vt = new ViewTrack(simpleHref(t.Link), t.Name, (int)t.Duration, artists, t.Album.Name, simpleHref(t.Album.Link));
            return vt;
        }

        private string simpleHref(string href)
        {
            var array = href.Split(':');
            return array.Last();
        }
    }
}