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
                .HasMany(p => p.Tracks)
                .WithRequired(t => t.Playlist);
            modelBuilder.Entity<Permission>()
                .HasRequired<Playlist>(p => p.Playlist);
            modelBuilder.Entity<Track>()
                .HasMany<Artist>(t => t.Artist);
            modelBuilder.Entity<Track>()
                .HasRequired<Album>(t => t.Album);
            modelBuilder.Entity<Artist>()
                .HasMany<Album>(a => a.Albuns)
                .WithMany(a => a.Artists);
            modelBuilder.Entity<Album>()
                .HasMany<Artist>(a => a.Artists)
                .WithMany(a => a.Albuns);
            modelBuilder.Entity<Album>()
                .HasMany<Track>(a => a.Tracks)
                .WithRequired(t => t.Album);
        }
    }
}
