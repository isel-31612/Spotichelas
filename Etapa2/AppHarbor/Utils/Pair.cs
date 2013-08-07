namespace Utils
{
    public abstract class Pair<T, E>
    {
        protected T Write { get; set; }
        protected E Read { get; set; }

        protected Pair(T w, E r)
        {
            this.Write = w;
            this.Read = r;
        }
    }

    public class Permission : Pair<bool, bool>
    {
        public bool CanRead { get { return base.Read; } set { base.Read = value; } }
        public bool CanWrite { get { return base.Write; } set { base.Write = value; } }

        public Permission(bool read, bool write)
            : base(write, read)
        { }
    }
}