using Entities;
using System.Data.Entity;
using System.Linq;

namespace DataAccess
{
    public class EFModel : DbContext
    {
        public EFModel(string context)
            : base(string.Format("name={0}",context)){}

        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Playlist>()
                .HasMany(p => p.DbTracks)
                .WithMany();
            modelBuilder.Entity<Permission>()
                .HasRequired<Playlist>(p => p.Playlist);
        }
    }
}
