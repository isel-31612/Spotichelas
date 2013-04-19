using Entities;
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
                singleton = new DAL();
            return singleton;
        }

        public static DAL Factory(Repository r)
        {
            return new DAL(r);
        }

        private DAL()
        {
            searcher = new SearcherDAL();       //TODO: dependency injector
            repo = new MemoryLocalRepository(); //TODO: dependency injector
        }

        private DAL(Repository r)
        {
            repo = r;
        }

        private Repository repo;

        public T[] getAll<T>(int[] idx) where T : Identity
        {
            T[] t = new T[idx.Length];
            for (int i = 0; i < idx.Length; i++)
            {
                t[i] = get<T>(idx[i]);
            }
            return t;
        }
        public T[] getAll<T>() where T : Identity
        {
            return repo.getAll<T>();
        }

        public T[] getAll<T>(T t) where T : Identity
        {
            return repo.getAllLike(t);
        }

        public int put<T>(T t) where T : Identity
        {
            return repo.setT(t);
        }

        public int[] putAll<T>(T[] t) where T : Identity
        {
            int[] idx = new int[t.Length];
            for (int i=0;i<t.Length;i++)
            {
                idx[i]=put(t[i]);
            }
            return idx;
        }

        public T update<T>(int id, T t) where T : Identity
        {
            return repo.update(id, t);
        }

        public T remove<T>(int id) where T : Identity
        {
            return repo.remove<T>(id);
        }

        public T get<T>(int idx) where T : Identity
        {
            return repo.getT<T>(idx);
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
        public List<Album> getAllAlbum(string query)
        {
            return searcher.getAlbuns(query);
        }
        public List<Artist> getAllArtists(string query)
        {
            return searcher.getArtists(query);
        }
        public List<Track> getAllTracks(string query)
        {
            return searcher.getTracks(query);
        }
    }
}
