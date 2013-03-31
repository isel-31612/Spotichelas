using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotiChelas.DomainModel.Data.DBAttributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    class DBForeignKey : Attribute
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
