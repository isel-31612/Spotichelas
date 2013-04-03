using System.Collections.Generic;
using WebGarten2.Html;

namespace SpotiChelas.Views
{
    //TODO: rever tipo e retorno de content(evitar cópia)
    class HomeView : HtmlDoc
    {
        public HomeView()
            : base("Home SpotiChelas",
                H1(Text("Home")),
                Ul(
                    Li(A(ResolveUri.ForPlaylist(), "Playlists")),
                    Li(A(ResolveUri.ForSearch(), "Search"))
                )
            )
        {}

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
