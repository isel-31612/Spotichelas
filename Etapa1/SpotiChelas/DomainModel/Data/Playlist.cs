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
        private String _name;
        [DBField]
        private String _description;

        public String Name { get { return _name; } }
        public String Description { get { return _description; } }
        public List<Track> Tracks {get; set;}
        

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
