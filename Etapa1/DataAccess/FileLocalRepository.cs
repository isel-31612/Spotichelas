using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class FileLocalRepository : Repository
    {
        Int32 Repository.setT<T>(T t)
        {
            return 0;
        }

        T Repository.getT<T>(int id, out T t)
        {
            t = default(T);
            return t;
        }

        T[] Repository.getAllLike<T>(T t)
        {
            throw new NotImplementedException();
        }
    }
}
