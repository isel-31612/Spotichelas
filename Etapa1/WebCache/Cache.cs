using System;
using System.Collections.Generic;


namespace WebCache
{
    public class Cache<T>
    {
        private Dictionary<string, CacheValue<T>> map;

        public Cache()
        {
            map = new Dictionary<string, CacheValue<T>>();
        }

        public bool Get(string uri, out T value) 
        {
            CacheValue<T> result;
            if (map.TryGetValue(uri, out result) && !result.Expired())
            {
                value = result.Get();
                return true;
            }
            else
            {
                value = default(T);
                return false;
            }
        }

        public void Add(string uri, T t)
        {
            if (map.ContainsKey(uri))
                return;
            CacheValue<T> value = new CacheValue<T>(t,DateTime.Now);
            map.Add(uri, value);
        }

        private class CacheValue<T>
        {
            readonly KeyValuePair<T, DateTime> value;

            public CacheValue(T t, DateTime dt)
            {
                value = new KeyValuePair<T, DateTime>(t,dt);
            }

            public bool Expired()
            {
                return DateTime.Compare(DateTime.Now, value.Value) < 0;
            }

            public T Get()
            {
                return value.Key;
            }
        }
    }
}
