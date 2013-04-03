using SpotiChelas.Views.Playlist;
using System.Net.Http;
using System.Net;
using WebGarten2;
using WebGarten2.Html;
using SpotiChelas.DomainModel.Data;
using System.Collections.Generic;
using System.Collections.Specialized;
using System;


namespace SpotiChelas.Controller
{
    class PlaylistController
    {
        //REPOSITORIO APENAS PARA TESTES
        private List<Playlist> _repo;
        public PlaylistController()
        {   
            _repo = new List<Playlist>{
                new Playlist("PL 1", "desc da playlist 1"),
                new Playlist("PL_rockalhada", "description da playlist nr2")
            };
            int i = 1; foreach (Playlist p in _repo)
            {
                p.setId(i++);
                p.Tracks = new List<Track>();
                p.Tracks.Add(new Track("Track 1", 2344));
                p.Tracks.Add(new Track("Track 2", 6552));
                p.Tracks.Add(new Track("Track 3", 85152));
            }
        }
        
        //Apenas para testar as views, podem alterar a vontade
        [HttpMethod("GET", "/playlist")]
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage
            {
                Content = new PlaylistView(_repo).AsHttpContent("text/html")
            };
        }


        [HttpMethod("GET", "/playlist/new")]
        public HttpResponseMessage New()
        {
            return new HttpResponseMessage
            {
                Content = new PlaylistNewView().AsHttpContent("text/html")
            };
        }


        [HttpMethod("POST", "/playlist")]
        public HttpResponseMessage Post(NameValueCollection content)
        {
            //verificacoes
            //if (!name.HasValue)
            //{
            //    return new HttpResponseMessage(HttpStatusCode.BadRequest)
            //    {
            //    };
            //}

            //adicionar no repositorio
            //MODO DE ADICAO TEMPORARIO
            Playlist tmp = new Playlist(content["name"], content["desc"]);
            tmp.setId(_repo.Count+1);
            tmp.Tracks = new List<Track>();
            tmp.Tracks.Add(new Track("Track 7", 5689));
            _repo.Add(tmp);

            //retornar resposta
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new PlaylistView(_repo).AsHttpContent("text/html")
            };
        }


        [HttpMethod("GET", "/playlist/{id}")]
        public HttpResponseMessage Get(int id)
        {
            Playlist pl = _repo.Find(p => p.getId() == id);
            return new HttpResponseMessage
            {
                Content = new PlaylistDetailView(pl).AsHttpContent("text/html")
            };
        }

    }
}
