namespace Utils
{
    public static class ResolveUri
    {
        public static string ForHome() { return "/"; }
        public static string ForPlaylist() { return "/playlist"; }
        public static string ForSearch() { return "/search"; }
        public static string ForPlaylistNew() { return "/playlist/new"; }
        public static string For(ViewAlbum alb) { return string.Format("/album/{0}", alb.Href); }
        public static string For(ViewArtist art) { return string.Format("/artist/{0}", art.Href); }
        public static string For(ViewPlaylist p) { return string.Format("/playlist/{0}", p.Id); }
        public static string For(ViewTrack t) { return string.Format("/track/{0}", t.Href); }
        public static string ForArtist(string href) { return string.Format("/artist/{0}", href); }
        public static string ForAlbum(string href) { return string.Format("/album/{0}", href); }
        public static string ForTrack(string href) { return string.Format("/track/{0}", href); }
        public static string ForPlaylistRemove(ViewPlaylist p) { return string.Format("/playlist/{0}/delete",p.Id); }
        public static string ForPlaylistEdit(ViewPlaylist p) { return string.Format("/playlist/{0}/edit",p.Id); }
        public static string ForAddTrack(string href) { return string.Format("/track/{0}/add", href); }
        public static string ForAddTrackTo(string href) { return string.Format("/playlist/{0}/add",href); }
        public static string ForRemoveTrack(ViewPlaylist pl,string href) { return string.Format("/playlist/{0}/remove/{1}", href,pl.Id); }
    }
}