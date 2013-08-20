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
            foreach (Track t in db.Set<Track>().Where(x => x.PlaylistId == id)) ;
            return result;
        }

        public Playlist[] getAllLike(Playlist playlist)
        {
            DbSet<Playlist> temp = db.Set<Playlist>();
            return temp.Where(x => x.match(playlist)).ToArray<Playlist>();
        }

        public Playlist update(int id, Playlist p)
        {
            foreach(Track t in p.Tracks)
            {
                db.Set<Track>().Add(t);
            }
            db.SaveChanges();
            return p;
        }

        public Playlist[] getAll()
        {
            DbSet<Playlist> temp = db.Set<Playlist>();
            return temp.ToArray();
        }

        public Playlist remove(int id)
        {
            DbSet<Playlist> table = db.Set<Playlist>();
            Playlist oldPlaylist = getPlaylist(id);
            if (oldPlaylist == null)
                return null;
            oldPlaylist = table.Remove(oldPlaylist);
            db.SaveChanges();
            return oldPlaylist;
        }
    }
}