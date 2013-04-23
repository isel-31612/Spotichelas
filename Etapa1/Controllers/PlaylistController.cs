using System.Net.Http;
using System.Net;
using System.Collections.Specialized;
using WebGarten2;
using BusinessRules;
using Views;
using WebGarten2.Html;
using Utils;
using System;


namespace Controllers
{
    public class PlaylistController
    {
        private readonly Logic Rules;
        public PlaylistController()
        {
            Rules = Logic.Factory(); //TODO: inject Logic or subclass
        }
        //Apenas para testar as views, podem alterar a vontade
        [HttpMethod("GET", "/playlist")]
        public HttpResponseMessage Get()
        {
            var playlists = Rules.FindAll.Playlists();
            return new HttpResponseMessage
            {
                Content = new PlaylistListView(playlists).AsHttpContent("text/html")
            };
        }


        [HttpMethod("GET", "/playlist/new")]
        public HttpResponseMessage New()
        {
            return new HttpResponseMessage
            {
                Content = new PlaylistNewView(null).AsHttpContent("text/html")
            };
        }


        [HttpMethod("POST", "/playlist")]
        public HttpResponseMessage Post(NameValueCollection content)
        {
            CreatePlaylist cp = new CreatePlaylist(content["name"], content["desc"]);
            var playlist = Rules.Create.Playlist(cp);
            //retornar resposta
            var response = new HttpResponseMessage(HttpStatusCode.SeeOther);//TODO: .Created); would make more sense, but it doesnt redirect...
            response.Headers.Location = new Uri(string.Format("http://localhost:8080/{0}", ResolveUri.For(playlist)));
            return response;
        }


        [HttpMethod("GET", "/playlist/{id}")]
        public HttpResponseMessage Get(int id)
        {
            var pl = Rules.Find.Playlist(id);
            return new HttpResponseMessage
            {
                Content = new PlaylistDetailView(pl).AsHttpContent("text/html")
            };
        }

        [HttpMethod("POST", "/playlist/{id}/delete")]
        public HttpResponseMessage Delete(int id)
        {
            var p = Rules.Remove.Playlist(id);
            if (p == null)
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            var response = new HttpResponseMessage(HttpStatusCode.SeeOther);
            response.Headers.Location = new Uri(string.Format("http://localhost:8080{0}", ResolveUri.ForPlaylist()));
            return response;
        }

        [HttpMethod("GET", "/playlist/{id}/edit")]
        public HttpResponseMessage Edit(int id)
        {
            //verificar
            var pl = Rules.Find.Playlist(id);

            //retornar resposta
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new PlaylistNewView(pl).AsHttpContent("text/html")
            };
        }

        [HttpMethod("POST", "/playlist/{id}/edit")]
        public HttpResponseMessage Edit(int id, NameValueCollection content)
        {
            //verificar
            var pl  = new EditPlaylist(id,content["name"], content["desc"]);
            var p = Rules.Edit.PlaylistTo(pl);

            //retornar resposta
            var response = new HttpResponseMessage(HttpStatusCode.SeeOther);
            response.Headers.Location = new Uri(string.Format("http://localhost:8080{0}", ResolveUri.For(p)));
            return response;
        }

        [HttpMethod("POST", "/playlist/{href}/remove/{id}")]
        public HttpResponseMessage Remove(string href, int id)
        {
            //verificar
            var playlist = Rules.Find.Playlist(id);
            playlist.Tracks.Remove(href); //TODO: what if it does not exist? Use Rules...

            //retornar resposta
            var response = new HttpResponseMessage(HttpStatusCode.SeeOther);
            response.Headers.Location = new Uri(string.Format("http://localhost:8080{0}", ResolveUri.For(playlist)));
            return response;
        }

        [HttpMethod("POST", "/playlist/{href}/add")]//TODO: would look better if playlist was a parameter too
        public HttpResponseMessage AddTrack(string href, NameValueCollection content)
        {
            string playlistId = content["playlist"];
            int id = int.Parse(playlistId);
            var playlist = Rules.Find.Playlist(id);
            var track = Rules.Find.Track(href);
            playlist.Tracks.Add(track.Href, track.Name);//TODO: what if it does already exist? Use Rules...
            var response = new HttpResponseMessage(HttpStatusCode.SeeOther);
            response.Headers.Location = new Uri(string.Format("http://localhost:8080{0}", ResolveUri.For(playlist)));
            return response;
        }
    }
}
