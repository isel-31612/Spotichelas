﻿using Entities;
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
            AddTo(dList,t);
            return t.id;
        }

        T Repository.getT<T>(int id)
        {
            var dList = getTable<T>();
            Object obj;
            if (!dList.TryGetValue(id, out obj))
                return default(T);
            return obj as T;
        }

        T[] Repository.getAllLike<T>(T t)
        {
            var dList = getTable<T>();
            Queue<T> result = new Queue<T>();
            foreach (var pair in dList)
            {
                if (t.match(pair.Value))
                {
                    result.Enqueue((T)pair.Value);
                }
            }
            return result.ToArray();
        }

        T Repository.update<T>(int id, T t)
        {
            var dList = getTable<T>();
            Object obj;
            if (!dList.TryGetValue(id, out obj))
                return null;
            dList.Remove(id);
            AddTo(dList, t);
            return (T)obj;
        }

        T[] Repository.getAll<T>()
        {
            var dList = getTable<T>();
            Queue<T> result = new Queue<T>();
            foreach (var item in dList)
            {
                result.Enqueue((T)item.Value);
            }
            return result.ToArray();
        }

        T Repository.remove<T>(int id)
        {
            var dList = getTable<T>();
            Object obj;
            if (!dList.TryGetValue(id, out obj))
                return null;
            dList.Remove(id);
            return (T)obj;
        }

        private Dictionary<Int32, Object> getTable<T>()
        {
            Type type = typeof(T);
            Dictionary<Int32, Object> dList;
            if (!repoTables.TryGetValue(type, out dList))
                throw new InvalidOperationException();
            return dList;
        }

        private void AddTo<T>(Dictionary<Int32,Object> dList,T t) where T : Identity
        {
            int highestIdx = dList.Max(x => { return x.Key; });
            t.id = highestIdx + 1;
            dList.Add(t.id, t);
        }
    }
}
