using Entities;

namespace Utils.View
{
    public class ViewPermission
    {
        public bool CanRead { get; set; }
        public bool CanWrite { get; set; }
        public string User { get; set; }

        public ViewPermission(Permission permission) {
            CanRead = permission.CanRead;
            CanWrite = permission.CanWrite;
            User = permission.User;
        }
    }
}
