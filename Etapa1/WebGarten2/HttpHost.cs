using System.Threading.Tasks;
using System.Web.Http.SelfHost;

namespace WebGarten2
{
    public class HttpHost
    {
        private readonly HttpSelfHostServer _server;
        private readonly FrontHandler _handler;

        public HttpHost(string baseAddress)
        {
            var config = new HttpSelfHostConfiguration(baseAddress); //Contentor de definições
            _handler = new FrontHandler(baseAddress); //Recebe pedidos, executa os metodos e devolve a resposta
            _server = new HttpSelfHostServer(config,new Bridge(_handler));//Escuta pedidos de http. Porque passar um HttpMessageHandler???
        }                                                                 // Envia-se um HttpMessageHandler para se controlar o modo como se gera respostas. Basicamente chamar o fronthandler e montar a resposta numa nova thread

        public HttpHost Add(params CommandBind[] cmds)
        {
            _handler.Add(cmds);
            return this;
        }

        public void Open()
        {
            _server.OpenAsync().Wait();
        }

        public void Close()
        {
            _server.CloseAsync().Wait();
        }
    }
}