using System;
using System.Collections.Generic;


namespace WebCache
{
    public class Cache<E,T>
    {
        private Dictionary<E, CacheValue<T>> map;

        public Cache()
        {
            map = new Dictionary<E, CacheValue<T>>();
        }

        public bool Get(E e, out T value, out DateTime dateAcquired) 
        {
            CacheValue<T> result;
            if (map.TryGetValue(e, out result) && !result.isFresh())
            {
                dateAcquired = result.GetDate();
                value = result.GetValue();
                return true;
            }
            else
            {
                dateAcquired = default(DateTime);
                value = default(T);
                return false;
            }
        }

        public void Add(E e, T t, DateTime date,DateTime expires)
        {
            if (map.ContainsKey(e))
                return;
            CacheValue<T> value = new CacheValue<T>(t, date,expires);
            map.Add(e, value);
        }

        private class CacheValue<F> where F : T
        {
            public readonly F Value;
            public readonly DateTime Date;
            public readonly DateTime Expires;

            public CacheValue(F f, DateTime date, DateTime expiration)
            {
                Value = f;
                Date = date;
                Expires = expiration;
            }

            public bool isFresh()
            {
                return DateTime.Now.CompareTo(Expires) > 0;
            }

            public F GetValue()
            {
                return Value;
            }

            public DateTime GetDate()
            {
                return Date;
            }
        }
    }
}