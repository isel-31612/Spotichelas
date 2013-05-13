using System.Collections.Generic;
using System.Linq;

using DataAccess;
using Entities;
using Utils;

namespace BusinessRules
{
    public class FindAllLogic
    {
        private DAL repo;
        public FindAllLogic(DAL d)
        {
            repo = d;
        }

        public ViewPlaylist[] Playlists(string user)
        {
            var pl = repo.getAll<Playlist>();
            Permission per;
            var list = pl.Where(p => p.Owner.Equals(user) || (p.Shared.TryGetValue(user, out per) && per.CanRead) )
                         .Select(p => new ViewPlaylist(p.id, p.Name, p.Description,p.Owner, p.Tracks))
                         .ToArray();
            return list;
        }

        public List<ViewAlbum> Albuns(string query, out SearchInfo info)
        {
            var result = repo.getAllAlbum(query);
            var ret = new List<ViewAlbum>();
            foreach (var album in result.Results)
            {
                List<KeyValuePair<string, string>> artist = album.Artists.Select(a => new KeyValuePair<string, string>(simpleHref(a.Link), a.Name)).ToList();
                ret.Add(new ViewAlbum(simpleHref(album.Link), album.Name, (int)album.Year, artist));
            }
            info = result.Info;
            return ret;
        }

        public List<ViewArtist> Artists(string query, out SearchInfo info)
        {
            var result = repo.getAllArtists(query);
            var ret = new List<ViewArtist>();
            foreach (var artist in result.Results)
            {
                List<KeyValuePair<string, string>> albuns = artist.Albuns.Select(a => new KeyValuePair<string, string>(simpleHref(a.Link), a.Name)).ToList();
                ret.Add(new ViewArtist(simpleHref(artist.Link), artist.Name, albuns));
            }
            info = result.Info;
            return ret;
        }

        public List<ViewTrack> Tracks(string query, out SearchInfo info)
        {
            var result = repo.getAllTracks(query);
            var ret = new List<ViewTrack>();
            foreach (var track in result.Results)
            {
                List<KeyValuePair<string, string>> artists = track.Artist.Select(a => new KeyValuePair<string, string>(simpleHref(a.Link), a.Name)).ToList();
                var album = track.Album;
                ret.Add( new ViewTrack(simpleHref(track.Link), track.Name, (int)track.Duration, artists, track.Album.Name, simpleHref(track.Album.Link))); 
            }
            info = result.Info;
            return ret;
        }

        private string simpleHref(string href)
        {
            var array = href.Split(':');
            return array.Last();
        }
    }
}
