using System.Diagnostics.CodeAnalysis;

namespace Projeto_pizzaria.Dominio.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class IdentifierUndefinedException : BusinessException
    {
        public IdentifierUndefinedException() : base("O id não pode ser vazio")
        {

        }
    }
}
