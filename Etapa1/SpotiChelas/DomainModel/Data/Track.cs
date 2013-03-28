using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotiChelas.DomainModel.Data
{
    public class Track : Identity
    {
        String _name;
        Artist _artist
        {
            get { return _artist; }
            set { _artist = value; }
        }
        Album _album
        {
            get { return _album; }
            set { _album = value; }
        }
        uint _duration; // in seconds, [0...]

        public Track(String name, uint duration)
        {
            _name = name;
            _duration = duration;
        }
    }
}
