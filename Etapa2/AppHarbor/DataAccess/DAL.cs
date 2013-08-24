using Entities;
using SpotifyBridge;
using System;
using System.Collections.Generic;
using Utils;

namespace DataAccess
{
    public class DAL
    {
        private static DAL singleton = null;
        private SearcherDAL searcher = null;

        public static DAL Factory()
        {
            if(singleton == null)
                singleton = new DAL(new FileLocalRepository(),new SearcherDAL());
            return singleton;
        }

        public static DAL Factory(FileLocalRepository r, SearcherDAL d)
        {
            return new DAL(r,d);
        }

        protected DAL(FileLocalRepository r, SearcherDAL dal)
        {
            repo = r;
            searcher = dal;
        }

        private FileLocalRepository repo;

        public Playlist[] getAll(int[] idx)
        {
            Playlist[] t = new Playlist[idx.Length];
            for (int i = 0; i < idx.Length; i++)
            {
                t[i] = get(idx[i]);
            }
            return t;
        }
        public Playlist[] getAll()
        {
            return repo.getAll();
        }

        public int put(Playlist p)
        {
            return repo.setPlaylist(p);
        }

        public int[] putAll(Playlist[] p)
        {
            int[] idx = new int[p.Length];
            for (int i=0;i<p.Length;i++)
            {
                idx[i]=put(p[i]);
            }
            return idx;
        }

        public Playlist update(Playlist p)
        {
            return repo.update(p);
        }

        public Track addTrack(Track t)
        {
            return repo.addTrack(t);
        }

        public Track removeTrack(Track t, Playlist p)
        {
            return repo.removeTrack(t,p);
        }

        public Playlist remove(Playlist p)
        {
            return repo.remove(p);
        }

        public Playlist get(int idx)
        {
            return repo.getPlaylist(idx);
        }

        public Album getAlbum(string link)
        {
            return searcher.getAlbum(link);
        }
        public Artist getArtist(string link)
        {
            return searcher.getArtist(link);
        }
        public Track getTrack(string link)
        {
            return searcher.getTrack(link);
        }
        public SearchResult<Album> getAllAlbum(string query)
        {
            return searcher.getAlbuns(query);
        }
        public SearchResult<Artist> getAllArtists(string query)
        {
            return searcher.getArtists(query);
        }
        public SearchResult<Track> getAllTracks(string query)
        {
            return searcher.getTracks(query);
        }
    }
}
