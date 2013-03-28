using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotiChelas.DomainModel.Data
{
    public class Playlist : Identity
    {
        String _name;
        String _description;

        public Playlist(String name, String description)
        {
            _name = name;
            _description = description;
        }
    }
}
