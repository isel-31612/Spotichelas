using Utils;
using WebGarten2.Html;
using System.Linq;

namespace Views
{
    public class ArtistView : HtmlDoc
    {
        public ArtistView(ViewArtist artist)
            : base(artist.Name,
                H1(Text(string.Format("Artist : {0}", artist.Name))),
                P(Label("name", "Name"), Text(artist.Name)),
                H2(Text("Albuns")),
                Ul(
                    artist.Albuns.Select(alb => Li(A(ResolveUri.ForAlbum(alb.Key), alb.Value))).ToArray()
                ),
                Ul(
                    Li(A(ResolveUri.ForPlaylist(), "Playlists")),
                    Li(A(ResolveUri.ForSearch(), "Search"))
                )
            )
        { }
    }
}