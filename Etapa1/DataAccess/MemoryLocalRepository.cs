using Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class MemoryLocalRepository : Repository
    {
        private Dictionary<Type, Dictionary<Int32,Object>> repoTables;
        public MemoryLocalRepository()
        {
            repoTables = new Dictionary<Type, Dictionary<Int32, Object>>();
        }

        Int32 Repository.setT<T>(T t)
        {
            Type type = t.GetType();
            Dictionary<Int32,Object> dList;
            if (repoTables.TryGetValue(type, out dList))
            {
                repoTables.Add(type, dList = new Dictionary<Int32, Object>());
            }
            int highestIdx = dList.Max(x => { return x.Key; });
            t.id = highestIdx + 1;
            dList.Add(t.id, t);
            return t.id;
        }

        T Repository.getT<T>(int id)
        {
            Type type = typeof(T);
            Dictionary<Int32, Object> dList;
            if (!repoTables.TryGetValue(type, out dList))
                throw new InvalidOperationException(); //TODO: E preciso uma excepçao melhorzita
            Object obj;
            if (!dList.TryGetValue(id, out obj))
                return default(T);
            return obj as T;
        }

        T[] Repository.getAllLike<T>(T t)
        {
            Type type = typeof(T);
            Queue<T> result = new Queue<T>();
            Dictionary<Int32, Object> dList;
            if (!repoTables.TryGetValue(type, out dList))
                throw new InvalidOperationException(); //TODO: E preciso uma excepçao melhorzita
            foreach (var pair in dList)
            {
                if (t.match(pair.Value))
                {
                    result.Enqueue((T)pair.Value);
                }
            }
            return result.ToArray();
        }

        int Repository.update<T>(int id, T t)
        {
            throw new NotImplementedException(); //TODO
        }

        T[] Repository.getAll<T>()
        {
            throw new NotImplementedException(); //TODO
        }

        T Repository.remove<T>(int id)
        {
            throw new NotImplementedException(); //TODO
        }
    }
}
