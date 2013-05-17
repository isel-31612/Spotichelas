using Entities;
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
        public FileLocalRepository(string context = "DefaultConnection")
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
            var result = db.Set<T>();
            return result.Where(x => x.id == id).FirstOrDefault();
        }

        T[] Repository.getAllLike<T>(T t)
        {
            return db.Set<T>().Where(x => x.match(t)).ToArray<T>();
        }

        T Repository.update<T>(int id, T newT)
        {
            if (newT is Playlist)
                update(id, newT as Playlist);
            db.SaveChanges();
            return newT; //A alteraçao de um objecto é automaticamente registado pela base de dados
        }

        Playlist update(int id, Playlist p)
        {
            Playlist ret = db.Set<Playlist>().Where(x => x.id == id).FirstOrDefault();
            if (ret != null)
            {
                if (p.Name != null) ret.Name = p.Name;
                if (p.Description != null) ret.Description = p.Description;
                if (p.Tracks.Count > 0) ret.Tracks = p.Tracks;
            }
            return ret;
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