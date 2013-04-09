using Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            int highestIdx = dList.Max(x => { return x.Key; });//se calhar devia por aqui um lock xD
            t.id = highestIdx + 1;
            dList.Add(t.id, t);
            return t.id;
        }

        T Repository.getT<T>(int id, out T t)
        {
            Type type = typeof(T);
            Dictionary<Int32, Object> dList;
            if (!repoTables.TryGetValue(type, out dList))
                throw new InvalidOperationException(); //E preciso uma excepçao melhorzita
            Object obj;
            t = default(T);
            if (!dList.TryGetValue(id, out obj))
                return t;
            t = obj as T;
            return t;
        }

        T[] Repository.getAllLike<T>(T t)
        {
            Type type = typeof(T);
            Queue<T> result = new Queue<T>();
            Dictionary<Int32, Object> dList;
            if (!repoTables.TryGetValue(type, out dList))
                throw new InvalidOperationException(); //E preciso uma excepçao melhorzita
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
    }
}
