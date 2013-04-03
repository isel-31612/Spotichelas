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
        public String Name { get; set; }
        [DBField]
        public uint Duration { get; set; } // in seconds, [0...]

        public Track(String name, uint duration)
        {
            Name = name;
            Duration = duration;
        }

        public override bool match(Object o)
        {
            Track t = o as Track;
            if (t == null)
                throw new InvalidCastException();
            return this.Name.Equals(t.Name) || this.Duration.Equals(t.Duration);
        }
    }
}
