using SpotiChelas.DomainModel.Data.DBAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotiChelas.DomainModel.Data
{
    public class Album : Identity
    {
        [DBField]
        [DBNotNull]
        public String _name;
        [DBField]
        public uint   _year;
        List<Track> _tracks
        {
            get{ return _tracks; }
            set{ _tracks = value; }
        }

        public Album(String name, uint year)
        {
            _name = name;
            _year = year;
        }

        public override bool match(Object o)
        {
            Album al = o as Album;
            if (al == null)
                throw new InvalidCastException();
            return this._name.Equals(al._name) || this._tracks.Equals(al._tracks) || this._year.Equals(al._year);
        }
    }
}
