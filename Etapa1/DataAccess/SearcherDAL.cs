using System;
using SpotifyBridge;
using Entities;
using System.Collections.Generic;

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
        public List<Track> getTracks(string name)
        {
            return Find.Track(name);
        }

        public Track getTrack(string Link)
        {
            return Request.Track(Link);
        }

        public List<Artist> getArtists(string name)
        {
            return Find.Artist(name);
        }

        public Artist getArtist(string Link)
        {
            return Request.Artist(Link);
        }

        public List<Album> getAlbuns(string name)
        {
            return Find.Album(name);
        }

        public Album getAlbuns(string Link)
        {
            return Request.Album(Link);
        }
    }
}
