using System;
using SpotifyBridge;
using Entities;
using System.Collections.Generic;
using Utils;

namespace DataAccess
{
    public class SearcherDAL
    {
        private LookUp Request;
        private Searcher Find;
        public SearcherDAL()
        {
            Find = new Searcher(); //TODO: dependency Injector
            Request = new LookUp(); //TODO: dependency Injector
        }
        public List<Track> getTracks(string name, out SearchInfo info)
        {
            return Find.Track(name, out info);
        }

        public Track getTrack(string Link)
        {
            return Request.Track(Link);
        }

        public List<Artist> getArtists(string name, out SearchInfo info)
        {
            return Find.Artist(name, out info);
        }

        public Artist getArtist(string Link)
        {
            return Request.Artist(Link);
        }

        public List<Album> getAlbuns(string name, out SearchInfo info)
        {
            return Find.Album(name, out info);
        }

        public Album getAlbum(string Link)
        {
            return Request.Album(Link);
        }
    }
}
