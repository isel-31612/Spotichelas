using System.Collections.Generic;
using Utils;
using WebGarten2.Html;
using System.Linq;

namespace Views
{
    public class SearchView : HtmlDoc
    {
        public SearchView()
            : base("Search Form",
                H1(Text("Search")),
                Form("POST","/search",
                    InputText("search","Search Query"),
                    InputBox("type",
                        Option("artists","Artists"),
                        Option("albuns","Albuns"),
                        Option("tracks","Tracks")
                    ),
                    InputSubmit("Search")
                ),
                Ul(
                    Li(A(ResolveUri.ForHome(), "Home")),
                    Li(A(ResolveUri.ForPlaylist(), "Playlists"))
                )
            )
        { }

        public SearchView(List<ViewArtist> artists, SearchInfo info)
            : base("Search Results",
                H1(Text("Results")),
                H2(Text("Artists")),
                artists.Count>0
                ? Ul(artists.Select(art => Li(Text(art.Name), A(ResolveUri.For(art), "View"))).ToArray())
                : Text("No results found"),
                Ul(
                    Li(A(ResolveUri.ForHome(), "Home")),
                    Li(A(ResolveUri.ForPlaylist(), "Playlists"))
                )
            )
        { }

        public SearchView(List<ViewAlbum> albuns, SearchInfo info)
            : base("Search Results",
                H1(Text("Results")),
                H2(Text("Albuns")),
                albuns.Count>0
                ? Ul(
                    albuns.Select( alb => Li(Text(alb.Name),A(ResolveUri.For(alb),"View"))).ToArray()
                )
                : Text("No results found"),
                Ul(
                    Li(A(ResolveUri.ForHome(), "Home")),
                    Li(A(ResolveUri.ForPlaylist(), "Playlists"))
                )
            )
        { }

        public SearchView(List<ViewTrack> tracks, SearchInfo info)
            : base("Search Results",
                H1(Text("Results")),
                H2(Text("Tracks")),
                tracks.Count>0
                ? Ul(
                    tracks.Select( trc => Li(Text(trc.Name), A(ResolveUri.For(trc),trc.Name))).ToArray()
                )
                : Text("No results found"),
                Ul(
                    Li(A(ResolveUri.ForHome(), "Home")),
                    Li(A(ResolveUri.ForPlaylist(), "Playlists"))
                )
            )
        { }
    }
}