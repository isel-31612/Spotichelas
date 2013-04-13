using Utils;
using WebGarten2.Html;

namespace Views
{
    //TODO: rever tipo e retorno de content(evitar cópia)
    public class ArtistView : HtmlDoc
    {
        public ArtistView(ViewArtist alb)
            : base("Home SpotiChelas",
                H1(Text("Home")),
                Ul(
                    Li(A(ResolveUri.ForPlaylist(), "Playlists")),
                    Li(A(ResolveUri.ForSearch(), "Search"))
                )
            )
        { }

        //private static IWritable[] content()
        //{
        //    List<IWritable> cont = new List<IWritable>();
        //    cont.Add(H1(Text("Home")));
        //    cont.Add(
        //        Ul(
        //            Li(A(ResolveUri.ForPlaylist(), "Playlists")),
        //            Li(A(ResolveUri.ForSearch(), "Search"))
        //        )
        //    );
        //    return cont.ToArray();
        //}
    }
}
