using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotiChelas.DomainModel.Database
{
    public class FileLocalRepository : Repository
    {
        Int32 Repository.setT<T>(T t)
        {
            return 0;
        }

        T Repository.getT<T>(int id)
        {
            return default(T);
        }

        T[] Repository.getAllLike<T>(T t)
        {
            throw new NotImplementedException();
        }
    }
}
