using Entities.DBAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Playlist : Identity
    {
        [DBField]
        [DBNotNull]
        public String Name { get; set; }
        [DBField]
        public String Description { get; set; }
        public List<Track> Tracks {get; set;}
        

        public Playlist(String name, String description)
        {
            Name = name;
            Description = description;
            Tracks = new List<Track>();
        }

        public override bool match(object o)
        {
            Playlist pl = o as Playlist;
            if (pl == null)
                throw new InvalidCastException();
            return this.Name.Equals(pl.Name) || this.Description.Equals(pl.Description);
        }
    }
}
