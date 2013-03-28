using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebGarten2
{
    public class Bridge : DelegatingHandler
    {   /*
         * Este metodo provavelmente e usado pelo servidor que escuta pedidos http.
         * Quando recebe um pedido executa o metodo SendAsync.
         */ 
        private readonly FrontHandler _handler;

        public Bridge(FrontHandler handler)
        {
            _handler = handler;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {   /*
             * Chama o Fronthandler para resolver o pedido
             * O objecto request contem os headers, body e outra informação do pedido http
             */ 
            var tcs = new TaskCompletionSource<HttpResponseMessage>();
            var resp = _handler.Handle(request);
            tcs.SetResult(resp); //Apos receber a resposta inicia a Thread com a resposta
            return tcs.Task;     //E devolve a Thread pronta a iniciar. O resultado da Thread e a resposta do servidor ao pedido do cliente
        }
    }
}
