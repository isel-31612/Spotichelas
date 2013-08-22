using System.Collections.Generic;
using System.Linq;

using DataAccess;
using Entities;
using Utils;
using System;

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
            Playlist[] pl = repo.getAll();
            Permission per;
            ViewPlaylist[] list = pl.Where(p => p.Owner.Equals(user) || (per = p.Shared.Find( perm => perm.User.Equals(user)))!=null && per.CanRead)
                            .Select(p => new ViewPlaylist(p))
                            .ToArray();
            return list;
        }

        public ViewPlaylist[] PlaylistsWithOwnerAccess(string user)
        {
            return PlaylistsWith((Playlist p) => (p.Owner.Equals(user)));
        }

        public ViewPlaylist[] PlaylistsWithWriteAccess(string user)
        {
            return PlaylistsWith((Playlist p) => (p.Owner.Equals(user) || p.Shared.Find( per => per.User.Equals(user)).CanWrite));
        }

        public ViewPlaylist[] PlaylistsWith(Func<Playlist,bool> expression)
        {
            Playlist[] pl = repo.getAll();

            ViewPlaylist[] list = pl.Where(p => expression(p)).Select(p => new ViewPlaylist(p))
                            .ToArray();
            return list;
        }

        public List<ViewAlbum> Albuns(string query, out SearchInfo info)
        {
            SpotifyBridge.SearchResult<Album> result = repo.getAllAlbum(query);
            List<ViewAlbum> ret = new List<ViewAlbum>();
            foreach (var album in result.Results)
            {
                List<KeyValuePair<string, string>> artist = album.Artists.Select(a => new KeyValuePair<string, string>(a.Link, a.Name)).ToList();
                ret.Add(new ViewAlbum(album.Link, album.Name, (int)album.Year, artist));
            }
            info = result.Info;
            return ret;
        }

        public List<ViewArtist> Artists(string query, out SearchInfo info)
        {
            SpotifyBridge.SearchResult<Artist> result = repo.getAllArtists(query);
            List<ViewArtist> ret = new List<ViewArtist>();
            foreach (var artist in result.Results)
            {
                List<KeyValuePair<string, string>> albuns = artist.Albuns.Select(a => new KeyValuePair<string, string>(a.Link, a.Name)).ToList();
                ret.Add(new ViewArtist(artist.Link, artist.Name, albuns));
            }
            info = result.Info;
            return ret;
        }

        public List<ViewTrack> Tracks(string query, out SearchInfo info)
        {
            SpotifyBridge.SearchResult<Track> result = repo.getAllTracks(query);
            List<ViewTrack> ret = new List<ViewTrack>();
            foreach (var track in result.Results)
            {
                //List<KeyValuePair<string, string>> artists = track.Artist.Select(a => new KeyValuePair<string, string>(a.Link!=null?a.Link:null, a.Name)).ToList();
                //Album album = track.Album;
                ret.Add( new ViewTrack(track)); 
            }
            info = result.Info;
            return ret;
        }
    }
}