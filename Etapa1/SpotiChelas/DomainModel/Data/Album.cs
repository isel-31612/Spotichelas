using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotiChelas.DomainModel.Data
{
    public class Album : Identity
    {
        String _name;
        uint   _year;
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
    }
}
