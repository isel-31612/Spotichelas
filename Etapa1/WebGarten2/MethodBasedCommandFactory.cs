using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;

namespace WebGarten2
{
    public class MethodBasedCommandFactory : ICommandFactory
    {
        private readonly Type[] _types;
        private readonly IParameterBinderProvider _binder;

        public MethodBasedCommandFactory(IParameterBinderProvider binder, params Type[] types)
        {
            _types = types;
            _binder = binder;
        }

        public IEnumerable<CommandBind> Create()
        {
            return
                _types.SelectMany(t => t.GetMethods()) // Selecionamos todos os metodos de todos os tipos
                    .Where(mi => mi.ReturnType == typeof(HttpResponseMessage)) // Filtramos os metodos cujo retorno e HttpResponseMessage
                    .Select(mi => new   // Por cada metodo crio um novo tipo(anonimo) que contem o metodo e os seus atributos se forem HttpMethodAttribute
                                      {
                                          MethodInfo = mi,
                                          Attributes = mi.GetCustomAttributes(typeof(HttpMethodAttribute), false) as HttpMethodAttribute[]
                                      })
                    .Where(x => x.Attributes.Length == 1) // Filtro apenas os novos tipos(anonimo) pelos que tenham 1 e so 1 atributo
                    .Select(x => new MethodBasedCommand(x.MethodInfo, x.Attributes[0], ResolveBindersFor(x.MethodInfo, x.Attributes[0])).Bind);
                    // Crio um MethodBasedCommand com o metodo, o atributo HttpMethodAttribute, e os parametros do metodo.
                    // Faço bind para que? Bind retorna um novo CommandBind com o MethodBasedCommand, o seu metodo e os atributos
        }

        private IEnumerable<Func<HttpRequestMessage, object>> ResolveBindersFor(MethodInfo mi, HttpMethodAttribute attr)
        {
            return mi.GetParameters().Select(p => GetBinder(p, attr));
        }

        private Func<HttpRequestMessage, object> GetBinder(ParameterInfo pi, HttpMethodAttribute attr)
        {
            var b = _binder.TryGetBinder(pi, attr);
            if (b == null) throw new Exception("Unable to find binder to " + pi.Name);
            return b;
        }
    }
}