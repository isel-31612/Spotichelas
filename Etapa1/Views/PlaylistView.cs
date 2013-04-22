using System.Collections.Generic;
using System.Linq;
using Utils;
using WebGarten2.Html;

namespace Views
{
    //TODO: rever metodos de acesso as propriedades do modelo de dados
    //talvez colocar propriedades (playlist._name => playlist.Name)
    public class PlaylistListView : HtmlDoc
    {
        public PlaylistListView(IEnumerable<ViewPlaylist> p) 
            : base("Playlists",
                H1(Text("Playlist list")),
                Ul(
                    p.Select(pl => Li(A(ResolveUri.For(pl), pl.Name))).ToArray()
                ),
                A(ResolveUri.ForPlaylistNew(), "New")
            ) { }
    }


    public class PlaylistNewView : HtmlDoc
    {
        public PlaylistNewView(ViewPlaylist p)
            : base("New Playlist",
                H1(Text("Create a new playlist")),
                Form("post", ResolveUri.ForPlaylist(),
                    Label("name", "Name: "), InputText("name", p==null ? "" : p.Name),
                    Label("desc", "Description: "), InputText("desc", p==null ? "" : p.Description),
                    P(InputSubmit("Submit"))
                )
            ){ }
    }


    public class PlaylistDetailView : HtmlDoc
    {
        public PlaylistDetailView(ViewPlaylist p)
            : base("Playlist Detail",
                H1(Text("Playlist Detail - "+p.Name)),
                H3(Text("Name: "+p.Name)),
                H3(Text("Description: "+p.Description)),
                H3(Text("Track List")),
                Ul(
                    p.Tracks.Select(track => Li(A(ResolveUri.ForTrack(track.Key), track.Value))).ToArray()
                ),
                Form("post", ResolveUri.ForPlaylistRemove(p), P(InputSubmit("Delete"))),
                Form("get", ResolveUri.ForPlaylistEdit(p), P(InputSubmit("Edit")))
            ) { }
    }
}
