using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace WebGarten2
{
    public class FrontHandler : DelegatingHandler
    {
        private readonly string _baseAddress;
        private readonly IDictionary<HttpMethod, UriTemplateTable> _tables = new Dictionary<HttpMethod, UriTemplateTable>();
        /*
         * OK. Basicamente é um dicionario de tipo de comando(GET/POST/PUT/HEAD/etc) e uma tabela com URIs e respectivos comandos.
         * Um UriTemplateTable aceita URIs ambiguos, ate ser trancado com MakeReadOnly. E verificado se nao ha conflitos e nao permite inserir URIs ambiguos
         * Ex. de conflicto: URI1 = /xpto/{id}, URI2 = /xpto/{NIF}. Se for feito um pedido com URI = .../xpto/300 é um conflicto pois nao sabe qual dos URIs escolher
         */
        public FrontHandler(string baseAddress)
        {
            _baseAddress = baseAddress;
        }

        public void Add(params CommandBind[] binds)
        {
            foreach (var b in binds)
            {
                UriTemplateTable t;
                if (!_tables.TryGetValue(b.HttpMethod, out t))
                {
                    t = new UriTemplateTable(new Uri(_baseAddress));
                    _tables.Add(b.HttpMethod, t);
                }
                t.KeyValuePairs.Add(new KeyValuePair<UriTemplate, object>(b.UriTemplate, b.Command));
            }
        }
        /*
         * Este metodo recebe pedidos e retorna a resposta
         */ 
        public HttpResponseMessage Handle(HttpRequestMessage req)
        {
            UriTemplateTable t;
            if (!_tables.TryGetValue(req.Method, out t))
            {// verifica o tipo de metodo (GET/POST/etc)
                return new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
            }

            var match = t.MatchSingle(req.RequestUri);
            if (match == null)
            {// verifica se existe um URI que coincida com o pedido
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            req.SetUriTemplateMatch(match);
            try
            {// obtem o comando que foi guardado e retorna o resultado da sua execução
                return (match.Data as ICommand).Execute(req);
            }
            catch(Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}