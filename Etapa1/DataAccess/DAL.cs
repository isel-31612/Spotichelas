﻿using Entities;
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

        public T get<T>(int idx) where T : Identity
        {
            return repo.getT<T>(idx);            
        }

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
            return null; //TODO: create this->repo.getAll(); // in interface and implementing classes
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

        public int update<T>(int id, T t) where T : Identity
        {
            return repo.update(id, t);
        }

        public T remove<T>(int id) where T : Identity
        {
            throw new NotImplementedException(); //TODO: do it!
        }
    }
}
