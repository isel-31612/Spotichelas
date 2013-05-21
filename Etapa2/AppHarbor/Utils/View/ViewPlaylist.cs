using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Utils
{
    public class ViewPlaylist
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string Owner { get; set; }
        public Dictionary<string, Pair<bool, bool>> Shared { get; set; }
        public Dictionary<string,string> Tracks { get; set; }

        public ViewPlaylist(int id, string name, string description, string user, Dictionary<string, string> tracks = null)
        {
            Id = id;
            Name = name;
            Description = description;
            Owner = user;
            Shared = new Dictionary<string, Pair<bool, bool>>();
            Tracks = tracks != null ? tracks : new Dictionary<string, string>();
        }

        public class Pair<T, E>
        {
            public T Write { get; set; }
            public E Read { get; set; }

            public Pair(T t, E e)
            {
                this.Write = t;
                this.Read = e;
            }
        }
    }
}
