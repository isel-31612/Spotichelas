using System.Collections.Generic;

namespace WebGarten2
{
    public interface ICommandFactory
    {
        IEnumerable<CommandBind> Create(); // Note-se que cria um enumaravel de CommandBind e nao de Command
    }
}