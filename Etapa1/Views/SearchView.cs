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
                //SearchFrame(info),
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
                //SearchFrame(info),
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
                //SearchFrame(info),
                Ul(
                    Li(A(ResolveUri.ForHome(), "Home")),
                    Li(A(ResolveUri.ForPlaylist(), "Playlists"))
                )
            )
        { }

        public static IWritable SearchFrame(SearchInfo info)
        {
            int lastPage = info.Count/info.Max;
            lastPage += info.Count % info.Max == 0 ? 0 : 1;
            int prevPage = info.Page == 1 ? 1 : info.Page - 1;
            string firsHref = string.Format("/search/{0}/{1}&page={2}", info.Type, info.Query, 1);
            string prevHref = string.Format("/search/{0}/{1}&page={2}", info.Type, info.Query, prevPage);
            string nextHref = string.Format("/search/{0}/{1}&page={2}", info.Type, info.Query, info.Page + 1);
            string lastHref = string.Format("/search/{0}/{1}&page={2}", info.Type, info.Query, lastPage);
            return P(   Form("GET",firsHref,InputSubmit("First")),
                        Form("GET",prevHref,InputSubmit("Prev")),
                        Form("GET",nextHref,InputSubmit("Next")),
                        Form("GET",lastHref,InputSubmit("Last"))
                );
        }
    }
}