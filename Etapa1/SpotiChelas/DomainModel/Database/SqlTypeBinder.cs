using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotiChelas.DomainModel.Database 
{
    public class SqlTypeBinder : TypeBinder<string>
    {
        private Dictionary<Type, string> typeLib = new Dictionary<Type, string>();

        private SqlTypeBinder()
        {
            typeLib = new Dictionary<Type, string>();
            typeLib.Add(typeof(int),"INT");
            typeLib.Add(typeof(uint), "INT");
            typeLib.Add(typeof(string), "NVARCHAR(20)");
        }

        public string getTypeFor(Type t)
        {
            string str = null;
            typeLib.TryGetValue(t, out str);
            return str;
        }

        public void Add(Type t, string str)
        {
            typeLib.Add(t,str);
        }

        private static SqlTypeBinder singleton=null;

        public static SqlTypeBinder get()
        {
            if(singleton==null)
                singleton = new SqlTypeBinder();
            return singleton;
        }
    }
}
