namespace Utils
{
    public abstract class Pair<K, V>
    {
        protected K Key { get; set; }
        protected V Value { get; set; }

        protected Pair(K k, V v)
        {
            this.Key = k;
            this.Value = v;
        }
    }

    public class Permission : Pair<bool, bool>
    {
        public bool CanRead { get { return base.Key; } set { base.Key = value; } }
        public bool CanWrite { get { return base.Value; } set { base.Value = value; } }

        public Permission(bool read, bool write)
            : base(read, write)
        { }
    }

    public class Music : Pair<string, string>
    {
        public string Href { get { return base.Key; } set { base.Key = value; } }
        public string Name { get { return base.Value; } set { base.Value = value; } }

        public Music(string href, string name)
            : base(href, name)
        { }
    }
}