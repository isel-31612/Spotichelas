using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CachedWebRepository : Repository
    {
        private Cache _cache;

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

        int Repository.update<T>(int id, T t)
        {
            throw new NotImplementedException();
        }
    }
}
