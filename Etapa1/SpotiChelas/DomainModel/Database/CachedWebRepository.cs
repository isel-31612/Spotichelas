using SpotiChelas.DomainModel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotiChelas.DomainModel.Database
{
    public class CachedWebRepository : Repository
    {
        private Cache _cache;

        Int32 Repository.setT<T>(T t)
        {
            throw new NotImplementedException();
        }

        T Repository.getT<T>(int id, out T t)
        {
            throw new NotImplementedException();
        }

        T[] Repository.getAllLike<T>(T t)
        {
            throw new NotImplementedException();
        }
    }
}
