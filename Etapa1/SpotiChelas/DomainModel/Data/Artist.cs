using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotiChelas.DomainModel.Data
{
    public class Artist : Identity
    {
        String _name;
        List<Album> _albuns
        {
            get { return _albuns; }
            set { _albuns = value; }
        }

        public Artist(String name)
        {
            _name = name;
        }
    }
}
