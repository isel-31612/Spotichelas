using Entities;
using System;

namespace DataAccess
{
    class DAL
    {
        private static Repository repo
        {
            get { if (repo == null) repo = new MemoryLocalRepository(); return repo; }
            set { repo = value; }
        }

        public static T get<T>(int idx, out T t) where T : Identity
        {
            t = repo.getT(idx, out t);
            return t;
        }

        public static T[] getAll<T>(int[] idx, out T[] t) where T : Identity
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

        public static T[] getAll<T>(T t) where T : Identity
        {
            return repo.getAllLike(t);
        }

        public static int put<T>(T t) where T : Identity
        {
            return repo.setT(t);
        }

        public static int[] putAll<T>(T[] t) where T : Identity
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
