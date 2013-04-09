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
        private SqlContext db;

        public FileLocalRepository()//TODO: Ctor to create a DIFERENT database, and use dependency injector
        {
            db = new SqlContext();
        }

        Int32 Repository.setT<T>(T t)
        {
            T ret = db.Set<T>().Add(t);
            db.SaveChanges();
            return ret.id;
        }

        T Repository.getT<T>(int id)
        {
            return db.Set<T>().Where(x => x.id == id).First();
        }

        T[] Repository.getAllLike<T>(T t)
        {
            return db.Set<T>().Where(x => x.match(t)).ToArray<T>();
        }

        int Repository.update<T>(int id, T t)
        {
            throw new NotImplementedException(); //TODO
        }

        T[] Repository.getAll<T>()
        {
            throw new NotImplementedException(); //TODO
        }

        T Repository.remove<T>(int id)
        {
            throw new NotImplementedException(); //TODO
        }

        private class SqlContext : DbContext
        {
            public DbSet<Playlist> Playlists { get; set; }
            public DbSet<Track> Tracks { get; set; }
            public DbSet<Album> Albums { get; set; }
            public DbSet<Artist> Artists { get; set; }
        }
    }
}
