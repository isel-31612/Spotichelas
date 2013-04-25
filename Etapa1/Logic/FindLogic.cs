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
            List<KeyValuePair<string, string>> albuns = ar.Albuns.Select(a => new KeyValuePair<string, string>(simpleHref(a.Link), a.Name)).ToList();
            var vAr = new ViewArtist(simpleHref(ar.Link), ar.Name,albuns);
            return vAr;
        }
        public ViewAlbum Album(string link)
        {
            var al = repo.getAlbum(link);
            List<KeyValuePair<string, string>> artists = al.Artists.Select(a => new KeyValuePair<string, string>(simpleHref(a.Link), a.Name)).ToList();
            List<KeyValuePair<string, string>> tracks = al.Tracks.Select(t => new KeyValuePair<string, string>(simpleHref(t.Link), t.Name)).ToList();
            var val = new ViewAlbum(simpleHref(al.Link), al.Name, (int)al.Year, artists,tracks);
            return val;
        }
        public ViewTrack Track(string link)
        {
            var t = repo.getTrack(link);
            List<KeyValuePair<string, string>> artists = t.Artist.Select(x => new KeyValuePair<string, string>(simpleHref(x.Link), x.Name)).ToList();
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