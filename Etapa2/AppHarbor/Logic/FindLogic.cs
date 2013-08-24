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
            Artist ar = repo.getArtist(link);
            List<KeyValuePair<string, string>> albuns = ar.Albuns.Select(a => new KeyValuePair<string, string>(a.Link, a.Name)).ToList();
            ViewArtist vAr = new ViewArtist(ar.Link, ar.Name,albuns);
            return vAr;
        }
        public ViewAlbum Album(string link)
        {
            Album al = repo.getAlbum(link);
            List<KeyValuePair<string, string>> artists = al.Artists.Select(a => new KeyValuePair<string, string>(a.Link != null ? a.Link : null, a.Name)).ToList();
            List<KeyValuePair<string, string>> tracks = al.Tracks.Select(t => new KeyValuePair<string, string>(t.Link, t.Name)).ToList();
            ViewAlbum val = new ViewAlbum(al.Link, al.Name, (int)al.Year, artists,tracks);
            return val;
        }
        public ViewTrack Track(string link)
        {
            Track t = repo.getTrack(link);
            ViewTrack vt = new ViewTrack(t);
            return vt;
        }
    }
}