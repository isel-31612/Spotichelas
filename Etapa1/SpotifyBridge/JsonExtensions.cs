using Entities;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace SpotifyBridge
{
    public static class JsonExtensions
    {
        public static List<Album> ToEntityList(this List<JsonSearchAlbum> list)
        {
            List<Album> ret = new List<Album>();
            foreach (var album in list)
            {
                List<Artist> alist = album.Artists.Select(a => new Artist(a.Name, a.Link)).ToList();
                ret.Add(new Album(album.Name, album.Link, 0, alist));
            }
            return ret;
        }

        public static List<Artist> ToEntityList(this List<JsonSearchArtist> list)
        {
            return list.Select(a => new Artist(a.Name, a.Link)).ToList();
        }

        public static List<Track> ToEntityList(this List<JsonSearchTrack> list)
        {
            List<Track> ret = new List<Track>();
            foreach (var track in list)
            {
                var artist = track.Artists.Select(x => new Artist(x.Name, x.Link)).ToList();
                var album = new Album(track.Album.Name, track.Album.Link);
                ret.Add(new Track(track.Name, track.Link, track.Duration, artist, album));
            }
            return ret;
        }

        public static SearchInfo ExtractSearchInfo(this JsonSearchResult search)
        {
            return new SearchInfo(search.Info.Count, search.Info.Max, search.Info.Offset,
                        search.Info.Query, search.Info.Type, search.Info.Page);
        }

        public static Album ToEntity(this JsonLookAlbum album)
        {
            IEnumerable<Track> t = album.Tracks.Select(x => new Track(x.Name, x.Link, 0));
            List<Artist> a = new List<Artist>();
            a.Add(new Artist(album.ArtistName, album.ArtistLink));
            return new Album(album.Name, album.Link, album.Year, a, t.ToList());
        }

        public static Artist ToEntity(this JsonLookArtist artist)
        {
            IEnumerable<Album> a = artist.Albuns.Select(x => new Album(x.Album.Name, x.Album.Link));
            return new Artist(artist.Name, artist.Link, a.ToList());
        }

        public static Track ToEntity(this JsonLookTrack track)
        {
            Album al = new Album(track.Album.Name, track.Album.Link, track.Album.Released);
            List<Artist> ar = track.Artist.Select(x => new Artist(x.Name, x.Link)).ToList();
            return new Track(track.Name, track.Link, track.Duration, ar, al);
        }
    }
}