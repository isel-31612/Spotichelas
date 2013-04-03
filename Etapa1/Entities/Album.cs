using Entities.DBAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Album : Identity
    {
        [DBField]
        [DBNotNull]
        public String Name { get; set; }
        [DBField]
        public uint Year { get; set; }
        List<Track> _tracks
        {
            get{ return _tracks; }
            set{ _tracks = value; }
        }

        public Album(String name, uint year)
        {
            Name = name;
            Year = year;
        }

        public override bool match(Object o)
        {
            Album al = o as Album;
            if (al == null)
                throw new InvalidCastException();
            return this.Name.Equals(al.Name) || this._tracks.Equals(al._tracks) || this.Year.Equals(al.Year);
        }
    }
}
