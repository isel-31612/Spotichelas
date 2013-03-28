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
        private Dictionary<Type, Dictionary<Int32,Object>> arrayTable;
        public MemoryLocalRepository()
        {
            arrayTable = new Dictionary<Type, Dictionary<Int32, Object>>();
        }

        Int32 Repository.setT<T>(T t)
        {
            Type type = t.GetType();
            Dictionary<Int32,Object> dList;
            arrayTable.TryGetValue(type, out dList);
            if (default(Dictionary<Int32, Object>) == dList)
            {
                arrayTable.Add(type, dList = new Dictionary<Int32, Object>());
            }
            int highestIdx = dList.Max(x => { return x.Key; });//se calhar devia por aqui um lock xD
            //t.setId(highestIdx + 1);
            //dList.Add(t.getId(), t); //Como e possivel t.getId dar erro?
            return 0;// t.getId();
        }

        T Repository.getT<T>(int id)
        {
            Type type = typeof(Identity);
            Dictionary<Int32, Object> dList;
            if (!arrayTable.TryGetValue(type, out dList))
                return default(T);//talvez uma excepçao aqui faça mais sentido
            Object obj;
            if (!dList.TryGetValue(id, out obj))
                return default(T);
            return (T)obj;
        }
    }
}
