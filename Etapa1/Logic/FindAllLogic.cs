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

        public List<ViewAlbum> Albuns(string query)
        {
            var al = repo.getAllAlbum(query);
            var ret = new List<ViewAlbum>();
            foreach (var album in al)
            {
                ret.Add(new ViewAlbum(simpleHref(album.Link), album.Name, (int)album.Year, album.Artist.Name, simpleHref(album.Artist.Link)));
            }
            return ret;
        }

        public List<ViewArtist> Artists(string query)
        {
            var al = repo.getAllArtists(query);
            var ret = new List<ViewArtist>();
            foreach (var artist in al)
            {
                var va = new ViewArtist(simpleHref(artist.Link), artist.Name);
                foreach (var album in artist.Albuns)
                    va.Albuns.Add(simpleHref(album.Link), album.Name);
                ret.Add(va);
            }
            return ret;
        }

        public List<ViewTrack> Tracks(string query)
        {
            var al = repo.getAllTracks(query);
            var ret = new List<ViewTrack>();
            foreach (var track in al)
            {
                var artists = new Dictionary<string, string>();
                foreach (var a in track.Artist)
                {
                    artists.Add(simpleHref(a.Link), a.Name);
                }
                var album = track.Album;
                var vt = new ViewTrack(simpleHref(track.Link), track.Name, (int)track.Duration, artists, track.Album.Name, simpleHref(track.Album.Link)); 
                ret.Add(vt);
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
