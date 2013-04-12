using Entities;
using System;

namespace DataAccess
{
    public class CachedWebRepository : Repository //TODO: this whole class
    {
        private Cache _cache;//TODO: dependency injector

        Int32 Repository.setT<T>(T t)
        {
            _cache = null;
            throw new NotImplementedException();
        }

        T Repository.getT<T>(int id)
        {
            if (_cache == null)
                _cache = null;
            throw new NotImplementedException();
        }

        T[] Repository.getAllLike<T>(T t)
        {
            throw new NotImplementedException();
        }

        T Repository.update<T>(int id, T t)
        {
            throw new NotImplementedException();
        }

        T[] Repository.getAll<T>()
        {
            throw new NotImplementedException();
        }

        T Repository.remove<T>(int id)
        {
            throw new NotImplementedException();
        }
    }
}
