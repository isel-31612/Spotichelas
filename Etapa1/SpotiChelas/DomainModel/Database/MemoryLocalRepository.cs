using SpotiChelas.DomainModel.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotiChelas.DomainModel.Database
{
    public class MemoryLocalRepository : Repository
    {
        private Dictionary<Type, Dictionary<Int32,Object>> repoTables;
        public MemoryLocalRepository()
        {
            repoTables = new Dictionary<Type, Dictionary<Int32, Object>>();
        }

        Int32 Repository.setT<T>(T t)
        {
            Type type = t.GetType();
            Dictionary<Int32,Object> dList;
            if (repoTables.TryGetValue(type, out dList))
            {
                repoTables.Add(type, dList = new Dictionary<Int32, Object>());
            }
            int highestIdx = dList.Max(x => { return x.Key; });//se calhar devia por aqui um lock xD
            t.setId(highestIdx + 1);
            dList.Add(t.getId(), t);
            return t.getId();
        }

        T Repository.getT<T>(int id)
        {
            Type type = typeof(T);
            Dictionary<Int32, Object> dList;
            if (!repoTables.TryGetValue(type, out dList))
                throw new InvalidOperationException(); //E preciso uma excepçao melhorzita
            Object obj;
            if (!dList.TryGetValue(id, out obj))
                return default(T);
            return (T)obj;
        }
    }
}
