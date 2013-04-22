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
                P(Label("artist", "Artist"), Text(album.Artist.Value),A(ResolveUri.ForArtist(album.Artist.Key),"View")),
                H2(Text("Tracks")),
                Ul(
                    album.Tracks.Select(trc => Li( A(ResolveUri.ForTrack(trc.Key), trc.Value))).ToArray()
                ),
                Ul(
                    Li(A(ResolveUri.ForPlaylist(), "Playlists")),
                    Li(A(ResolveUri.ForSearch(), "Search"))
                )
            )
        { }
    }
}
