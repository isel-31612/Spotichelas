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
            foreach (Album album in result.Results)
            {
                ret.Add(new ViewAlbum(album));
            }
            info = result.Info;
            return ret;
        }

        public List<ViewArtist> Artists(string query, out SearchInfo info)
        {
            SpotifyBridge.SearchResult<Artist> result = repo.getAllArtists(query);
            List<ViewArtist> ret = new List<ViewArtist>();
            foreach (Artist artist in result.Results)
            {
                ret.Add(new ViewArtist(artist));
            }
            info = result.Info;
            return ret;
        }

        public List<ViewTrack> Tracks(string query, out SearchInfo info)
        {
            SpotifyBridge.SearchResult<Track> result = repo.getAllTracks(query);
            List<ViewTrack> ret = new List<ViewTrack>();
            foreach (Track track in result.Results)
            {
                ret.Add( new ViewTrack(track)); 
            }
            info = result.Info;
            return ret;
        }
    }
}