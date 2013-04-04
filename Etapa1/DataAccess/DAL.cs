using Entities;
using System;

namespace DataAccess
{
    public class DAL
    {
        private static DAL singleton = null;

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
            repo = new MemoryLocalRepository();
        }

        private DAL(Repository r)
        {
            repo = r;
        }

        private Repository repo;

        public T get<T>(int idx, out T t) where T : Identity
        {
            t = repo.getT(idx, out t);
            return t;
        }

        public T[] getAll<T>(int[] idx, out T[] t) where T : Identity
        {
            t = new T[idx.Length];
            for (int i = 0; i < idx.Length; i++)
            {
                T x;
                get(idx[i], out x);
                t[i] = x;
            }
            return t;
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
    }
}
