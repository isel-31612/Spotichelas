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
        public String _name;
        List<Album> _albuns
        {
            get { return _albuns; }
            set { _albuns = value; }
        }

        public Artist(String name)
        {
            _name = name;
        }

        public override bool match(object o)
        {
            Artist ar = o as Artist;
            if (o == null)
                throw new InvalidCastException();
            return this._name.Equals(ar._name) || this._albuns.Equals(ar._albuns);
        }
    }
}
