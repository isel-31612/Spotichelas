﻿using System.Net.Http;
using System.Net;
using System.Collections.Specialized;
using WebGarten2;
using BusinessRules;
using Views;
using WebGarten2.Html;
using Utils;


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


        [HttpMethod("POST", "/playlist/")]
        public HttpResponseMessage Post(NameValueCollection content)
        {
            CreatePlaylist cp = new CreatePlaylist(content["name"], content["desc"]);
            Rules.Create.Playlist(cp);
            //retornar resposta
            return new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = new PlaylistListView(null).AsHttpContent("text/html")
            };
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

        [HttpMethod("POST", "/playlist/remove/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            var p = Rules.Remove.Playlist(id);
            if (p == null)
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new PlaylistListView(null).AsHttpContent("text/html")
            };
        }

        [HttpMethod("GET", "/playlist/edit/{id}")]
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

        [HttpMethod("POST", "/playlist/edit/{id}")]
        public HttpResponseMessage Edit(int id, NameValueCollection content)
        {
            //verificar
            var tracks = content["tracks"]; //TODO: Unused
            var pl  = new EditPlaylist(id,content["name"], content["desc"]);
            var p = Rules.Edit.PlaylistTo(pl);

            //retornar resposta
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new PlaylistNewView(p).AsHttpContent("text/html")
            };
        }
    }
}
