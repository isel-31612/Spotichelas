using Utils;
using WebGarten2.Html;
using System.Linq;

namespace Views
{
    public class AlbumView : HtmlDoc
    {
        public AlbumView(ViewAlbum album)
            : base(album.Name,
                H1(Text(string.Format("Album : {0}",album.Name))),
                P(Label("name", "Name"), Text(album.Name)),
                P(Label("year","Release"),Text(album.Year)),
                H2(Text("Artists")),
                Ul(
                    album.Artist.Select( a => Li(A(ResolveUri.ForArtist(a.Key),a.Value))).ToArray()
                ),
                H2(Text("Tracks")),
                Ul(
                    album.Tracks.Select(trc => Li( A(ResolveUri.ForTrack(trc.Key), trc.Value))).ToArray()
                ),
                PlaylistPlayer(album.Name,album.Tracks.Select(track => track.Key).ToArray()),
                Ul(
                    Li(A(ResolveUri.ForPlaylist(), "Playlists")),
                    Li(A(ResolveUri.ForSearch(), "Search"))
                )
            )
        { }
    }
}
