using SpotiChelas.DomainModel.Data.DBAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotiChelas.DomainModel.Data
{
    public class Playlist : Identity
    {
        [DBField]
        [DBNotNull]
        public String _name;
        [DBField]
        public String _description;
        List<Track> _tracks
        {
            get { return _tracks; }
            set { _tracks = value; }
        }

        public Playlist(String name, String description)
        {
            _name = name;
            _description = description;
        }

        public override bool match(object o)
        {
            Playlist pl = o as Playlist;
            if (pl == null)
                throw new InvalidCastException();
            return this._name.Equals(pl._name) || this._description.Equals(pl._description);
        }
    }
}
