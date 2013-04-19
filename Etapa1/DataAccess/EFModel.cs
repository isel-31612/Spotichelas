using Entities;
using System.Data.Entity;

namespace DataAccess
{
    public class EFModel : DbContext
    {
        public EFModel(string context)
            : base(string.Format("name={0}",context)){}

        public DbSet<Playlist> Playlists { get; set; }
    }
}
