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
            var vAr = new ViewArtist(ar.Link,ar.Name);
            foreach (var album in ar.Albuns)
                vAr.Albuns.Add(album.Link, album.Name);
            return vAr;
        }
        public ViewAlbum Album(string link)
        {
            var al = repo.getAlbum(link);
            var val = new ViewAlbum(al.Link,al.Name,(int)al.Year,al.Artist.Name,al.Artist.Link);
            foreach (var track in al.Tracks)
                val.Tracks.Add(track.Link,track.Name);
            return val;
        }
        public ViewTrack Track(string link)
        {
            var t = repo.getTrack(link);
            var artists = t.Artist.Select(x => new { Key = x.Link, Value = x.Name }).ToDictionary(x => x.Key, x=> x.Value);
            var vt = new ViewTrack(t.Link,t.Name,(int)t.Duration,artists,t.Album.Name,t.Album.Link);
            return vt;
        }
    }
}