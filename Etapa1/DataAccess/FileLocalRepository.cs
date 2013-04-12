using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;

namespace DataAccess
{
    public class FileLocalRepository : Repository
    {
        private DbContext db;
        public FileLocalRepository(string context = "Debug")
        {
            db = new EFModel(context); //TODO: Use dependency injector
        }

        Int32 Repository.setT<T>(T t)
        {
            T ret = db.Set<T>().Add(t);
            db.SaveChanges();
            return ret.id;
        }

        T Repository.getT<T>(int id)
        {
            return db.Set<T>().Where(x => x.id == id).FirstOrDefault();
        }

        T[] Repository.getAllLike<T>(T t)
        {
            return db.Set<T>().Where(x => x.match(t)).ToArray<T>();
        }

        T Repository.update<T>(int id, T newT) //TODO: Esta a ser removido A e inserido B. Logo o id nao fica o mesmo
        {
            var table = db.Set<T>();
            T oldT = table.Where(x => x.id == id).FirstOrDefault(); //TODO: Nao e possivel chamar o remove?
            if (oldT == default(T))
                return oldT;
            table.Remove(oldT);
            table.Add(newT);
            db.SaveChanges();
            return oldT;
        }

        T[] Repository.getAll<T>()
        {
            return db.Set<T>().ToArray();
        }

        T Repository.remove<T>(int id)
        {
            var table = db.Set<T>();
            T oldT = table.Where(x => x.id == id).FirstOrDefault(); //TODO: nao e possivel chamar o get?
            if (oldT == default(T))
                return oldT;
            oldT = table.Remove(oldT);
            db.SaveChanges();
            return oldT;
        }
    }
}
