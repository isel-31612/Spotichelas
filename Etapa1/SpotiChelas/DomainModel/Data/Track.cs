using SpotiChelas.DomainModel.Data.DBAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotiChelas.DomainModel.Data
{
    public class Track : Identity
    {
        [DBField]
        [DBNotNull]
        public String _name;
        [DBField]
        public uint _duration; // in seconds, [0...]

        public Track(String name, uint duration)
        {
            _name = name;
            _duration = duration;
        }

        public override bool match(Object o)
        {
            Track t = o as Track;
            if (t == null)
                throw new InvalidCastException();
            return this._name.Equals(t._name) || this._duration.Equals(t._duration);
        }
    }
}
