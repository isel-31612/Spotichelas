using DataAccess;
using Entities;
using System.Collections.Generic;
using Utils;
using System.Linq;

namespace BusinessRules
{
    public class FindAllLogic
    {
        private DAL repo;
        public FindAllLogic(DAL d)
        {
            repo = d;
        }

        public ViewPlaylist[] Playlists()
        {
            var pl = repo.getAll<Playlist>();
            List<ViewPlaylist> list = new List<ViewPlaylist>();
            foreach(var p in pl){
                var vp = new ViewPlaylist(p.id, p.Name, p.Description,p.Tracks);
                list.Add(vp);
            }
            return list.ToArray();
        }

        public List<ViewAlbum> Albuns(string query, out SearchInfo info)
        {
            var al = repo.getAllAlbum(query, out info);
            var ret = new List<ViewAlbum>();
            foreach (var album in al)
            {
                List<KeyValuePair<string, string>> artist = album.Artists.Select(a => new KeyValuePair<string, string>(simpleHref(a.Link), a.Name)).ToList();
                ret.Add(new ViewAlbum(simpleHref(album.Link), album.Name, (int)album.Year, artist));
            }
            return ret;
        }

        public List<ViewArtist> Artists(string query, out SearchInfo info)
        {
            var al = repo.getAllArtists(query, out info);
            var ret = new List<ViewArtist>();
            foreach (var artist in al)
            {
                List<KeyValuePair<string, string>> albuns = artist.Albuns.Select(a => new KeyValuePair<string, string>(simpleHref(a.Link), a.Name)).ToList();
                ret.Add(new ViewArtist(simpleHref(artist.Link), artist.Name, albuns));
            }
            return ret;
        }

        public List<ViewTrack> Tracks(string query, out SearchInfo info)
        {
            var al = repo.getAllTracks(query, out info);
            var ret = new List<ViewTrack>();
            foreach (var track in al)
            {
                List<KeyValuePair<string, string>> artists = track.Artist.Select(a => new KeyValuePair<string, string>(simpleHref(a.Link), a.Name)).ToList();
                var album = track.Album;
                ret.Add( new ViewTrack(simpleHref(track.Link), track.Name, (int)track.Duration, artists, track.Album.Name, simpleHref(track.Album.Link))); 
            }
            return ret;
        }

        private string simpleHref(string href)
        {
            var array = href.Split(':');
            return array.Last();
        }
    }
}
