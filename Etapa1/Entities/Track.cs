using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Track : Identity
    {
        public String Name { get; set; }
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
            return ((t.Name==null)||t.Name.Equals(Name)) &&
                    ((t.Duration == 0) || t.Duration.Equals(Duration));
        }
    }
}
