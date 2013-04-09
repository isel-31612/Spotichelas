using System.Net.Http;
using System.Net;
using System.Collections.Generic;
using System.Collections.Specialized;
using WebGarten2;
using Entities;
using Views;
using WebGarten2.Html;


namespace Controllers
{
    public class PlaylistController
    {
        List<Playlist> _repo = new List<Playlist>();
        //Apenas para testar as views, podem alterar a vontade
        [HttpMethod("GET", "/playlist")]
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage
            {
                Content = new PlaylistListView(_repo).AsHttpContent("text/html")
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
            tmp.id = (_repo.Count+1);
            tmp.Tracks = new List<Track>();
            tmp.Tracks.Add(new Track("Track 7", 5689));
            _repo.Add(tmp);

            //retornar resposta
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new PlaylistListView(_repo).AsHttpContent("text/html")
            };
        }


        [HttpMethod("GET", "/playlist/{id}")]
        public HttpResponseMessage Get(int id)
        {
            Playlist pl = _repo.Find(p => p.id == id);
            return new HttpResponseMessage
            {
                Content = new PlaylistDetailView(pl).AsHttpContent("text/html")
            };
        }


        //tipo de pedido http deveria ser delete?
        [HttpMethod("POST", "/playlist/{id}/remove")]
        public HttpResponseMessage Delete(int id)
        {
            //verificacoes

            //remover do repositorio
            _repo.RemoveAt(id-1);

            //retornar resposta
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new PlaylistListView(_repo).AsHttpContent("text/html")
            };
        }

        
        [HttpMethod("GET", "/playlist/{id}/edit")]
        public HttpResponseMessage Edit(int id)
        {
            //verificar
            Playlist pl = _repo.Find(p => p.id == id);

            //retornar resposta
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new PlaylistNewView(pl).AsHttpContent("text/html")
            };
        }
        

    }
}
