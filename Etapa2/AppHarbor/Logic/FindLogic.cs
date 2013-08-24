using System.Collections.Generic;
using System.Linq;

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
            Playlist pl = repo.get(id);
            if (pl != null)
                return new ViewPlaylist(pl);
            return null;
        }

        public ViewPlaylist PlaylistWithOwnerAccess(int id, string user)
        {
            Playlist pl = repo.get(id);
            if (pl != null && (pl.Owner.Equals(user)))
                return new ViewPlaylist(pl);
            return null;
        }

        public ViewPlaylist PlaylistWithReadAccess(int id, string user)
        {
            Playlist pl = repo.get(id);
            if (pl != null && (pl.Owner.Equals(user) || (pl.Shared.Find(per => per.User.Equals(user)).CanRead)))
                return new ViewPlaylist(pl);
            return null;
        }

        public ViewPlaylist PlaylistWithWriteAccess(int id, string user)
        {
            Playlist pl = repo.get(id);
            if (pl != null && (pl.Owner.Equals(user) || (pl.Shared.Find(per => per.User.Equals(user)).CanWrite)))
                return new ViewPlaylist(pl);
            return null;
        }
        public ViewArtist Artist(string link)
        {
            Artist artist = repo.getArtist(link);
            if (artist == null)
                return null;
            return new ViewArtist(artist);
        }
        public ViewAlbum Album(string link)
        {
            Album album = repo.getAlbum(link);
            if (album == null)
                return null;
            return new ViewAlbum(album);
        }
        public ViewTrack Track(string link)
        {
            Track track = repo.getTrack(link);
            if (track == null)
                return null;
            return new ViewTrack(track);
        }
    }
}