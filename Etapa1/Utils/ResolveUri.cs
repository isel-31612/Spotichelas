using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class ResolveUri
    {
        public static string ForHome() { return "/home"; }
        public static string ForPlaylist() { return "/playlist"; }
        public static string ForSearch() { return "/search"; }
        public static string ForPlaylistNew() { return "/playlist/new"; }
        public static string For(Album alb) { return "/album/" + alb.id; }
        public static string For(Artist art) { return "/artist/" + art.id; }
        public static string For(Playlist p) { return "/playlist/" + p.id; }
        public static string For(Track t) { return "/track/" + t.id; }
        public static string ForPlaylistRemove(Playlist p) { return "/playlist/" + p.id + "/remove"; }
        public static string ForPlaylistEdit(Playlist p) { return "/playlist/" + p.id + "/edit"; }

        //return string.Format("/playlist/{0}", p.getId());
    }
}