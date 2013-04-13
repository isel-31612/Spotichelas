namespace Utils
{
    public static class ResolveUri
    {
        public static string ForHome() { return "/home"; }
        public static string ForPlaylist() { return "/playlist"; }
        public static string ForSearch() { return "/search"; }
        public static string ForPlaylistNew() { return "/playlist/new"; }
        public static string For(ViewAlbum alb) { return "/album/" + alb.Id; }
        public static string For(ViewArtist art) { return "/artist/" + art.Id; }
        public static string For(ViewPlaylist p) { return "/playlist/" + p.Id; }
        public static string For(ViewTrack t) { return "/track/" + t.Id; }
        public static string ForPlaylistRemove(ViewPlaylist p) { return "/playlist/" + p.Id + "/remove"; }
        public static string ForPlaylistEdit(ViewPlaylist p) { return "/playlist/" + p.Id + "/edit"; }

        //return string.Format("/playlist/{0}", p.getId());
    }
}