using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotiChelas.DomainModel.Database
{
    public interface TypeBinder<T>
    {
        T getTypeFor(Type t);
    }
}
