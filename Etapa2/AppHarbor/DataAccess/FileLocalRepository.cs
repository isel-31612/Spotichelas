using Entities;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;

namespace DataAccess
{
    public class FileLocalRepository
    {
        private DbContext db;
        public FileLocalRepository(string context = "DefaultConnection")
        {
            db = new EFModel(context); //TODO: Use dependency injector
        }

        public Int32 setPlaylist(Playlist playlist)
        {
            Playlist ret = db.Set<Playlist>().Add(playlist);
            db.SaveChanges();
            return ret.id;
        }

        public Playlist getPlaylist(int id)
        {
            Playlist result = db.Set<Playlist>().Where(x => x.id == id).FirstOrDefault();
            return result;
        }

        public Playlist[] getAllLike(Playlist playlist)
        {
            DbSet<Playlist> temp = db.Set<Playlist>();
            return temp.Where(x => x.match(playlist)).ToArray<Playlist>();
        }

        public Playlist update(Playlist p)
        {
            db.Entry<Playlist>(p).State = System.Data.EntityState.Modified;
            db.SaveChanges();
            return p;
        }

        public Track removeTrack(Track t,Playlist p)
        {
            /*foreach (Track track in p.getTracks())
            {
                db.Entry<Track>(track).State = System.Data.EntityState.Modified;
            }*/
            Track removed = db.Set<Track>().Remove(t);
            db.SaveChanges();
            return removed;
        }

        public Track addTrack(Track t)
        {
            Track added = db.Set<Track>().Add(t);
            db.SaveChanges();
            return added;
        }

        public Playlist[] getAll()
        {
            DbSet<Playlist> temp = db.Set<Playlist>();
            return temp.ToArray();
        }

        public Playlist remove(Playlist p)
        {
            Playlist removed = db.Set<Playlist>().Remove(p);
            db.SaveChanges();
            return removed;
        }
    }
}