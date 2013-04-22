namespace Utils
{
    public static class ResolveUri
    {
        public static string ForHome() { return "/"; }
        public static string ForPlaylist() { return "/playlist"; }
        public static string ForSearch() { return "/search"; }
        public static string ForPlaylistNew() { return "/playlist/new"; }
        public static string For(ViewAlbum alb) { return "/album/" + alb.Href; }
        public static string For(ViewArtist art) { return "/artist/" + art.Href; }
        public static string For(ViewPlaylist p) { return "/playlist/" + p.Id; }
        public static string For(ViewTrack t) { return "/track/" + t.Href; }
        public static string ForArtist(string href) { return "/artist/" + href; }
        public static string ForAlbum(string href) { return "/album/" + href; }
        public static string ForTrack(string href) { return "/track/" + href; }
        public static string ForPlaylistRemove(ViewPlaylist p) { return "/playlist/" + p.Id + "/remove"; }
        public static string ForPlaylistEdit(ViewPlaylist p) { return "/playlist/" + p.Id + "/edit"; }
        public static string ForAddTrack(string href) { return string.Format("/track/add/{0}",href); }
        public static string ForAddAlbum(string href) { return string.Format("/album/add/{0}", href); }
    }
}