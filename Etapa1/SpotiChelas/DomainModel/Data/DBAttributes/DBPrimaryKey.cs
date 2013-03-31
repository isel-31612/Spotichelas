using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotiChelas.DomainModel.Data.DBAttributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    class DBPrimaryKey : Attribute
    {
    }
}
