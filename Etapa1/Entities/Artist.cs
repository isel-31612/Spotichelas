using SpotiChelas.DomainModel.Data.DBAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotiChelas.DomainModel.Data
{
    public class Artist : Identity
    {
        [DBField]
        [DBNotNull]
        public String Name { get; set; }
        List<Album> Albuns { get; set; }

        public Artist(String name)
        {
            Name = name;
            Albuns = new List<Album>();
        }

        public override bool match(object o)
        {
            Artist ar = o as Artist;
            if (o == null)
                throw new InvalidCastException();
            return this.Name.Equals(ar.Name) || this.Albuns.Equals(ar.Albuns);
        }
    }
}
