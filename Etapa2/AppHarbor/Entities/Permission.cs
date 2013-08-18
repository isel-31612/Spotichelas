using System;
namespace Entities
{
    public class Permission : Identity
    {
        public bool CanRead { get; set; }
        public bool CanWrite { get; set; }
        public string User { get; set; }

        public int PlaylistId { get; set; }
        public virtual Playlist Playlist { get; set; }

        public Permission(string user,bool read, bool write)
        {
            User = user;
            CanRead = read;
            CanWrite = write;
        }

        public Permission()
        {
            User = null;
            CanRead = false;
            CanWrite = false;
        }

        public override bool match(object o)
        {
            Permission p = o as Permission;
            if (p == null)
                throw new InvalidCastException();
            return User.Equals(p.User) && CanRead == p.CanRead && CanWrite == p.CanWrite;
        }
    }
}
