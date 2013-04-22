using System.Collections.Generic;
using System.Linq;
using Utils;
using WebGarten2.Html;

namespace Views
{
    public class TrackView : HtmlDoc
    {
        public TrackView(ViewTrack track)
            : base(track.Name,
                H1(Text(string.Format("Album : {0}", track.Name))),
                P(Label("name", "Name"), Text(track.Name)),
                P(Label("duration", "Duration"), Text(track.Duration)),
                P(Label("album", "Album"), Text(track.Album.Value), A(ResolveUri.ForAlbum(track.Album.Key),"View")),
                H2(Text("Artists")),
                Ul(
                    track.Artists.Select(art => Li(A(ResolveUri.ForArtist(art.Key), art.Value))).ToArray()
                ),
                TrackPlayer(track.Href),
                Ul(
                    Li(A(ResolveUri.ForAddTrack(track.Href), "Adicionar")),
                    Li(A(ResolveUri.ForPlaylist(), "Playlists")),
                    Li(A(ResolveUri.ForSearch(), "Search"))
                )
            )
        { }
    }
}
