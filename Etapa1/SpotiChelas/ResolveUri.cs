using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotiChelas.DomainModel.Data;

namespace SpotiChelas
{
    static class ResolveUri
    {
        public static string ForHome()          { return "/home";       }
        public static string ForPlaylist()      { return "/playlist";   }
        public static string ForSearch()        { return "/search";     }
        public static string ForPlaylistNew()   { return "/playlist/new"; }
        public static string For(Playlist p)    { return "/playlist/"+p.getId(); }

        
        //return string.Format("/playlist/{0}", p.getId());
    }
}
