using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DBAttributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public class DBForeignKey : Attribute
    {
        public string table;
        public string field;
        public DBForeignKey(string table, string field)
        {
            this.table = table;
            this.field = field;
        }
    }
}
