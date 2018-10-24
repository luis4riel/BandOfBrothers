using Projeto_pizzaria.Dominio.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Projeto_pizzaria.Dominio.Features.Clientes
{
    [ExcludeFromCodeCoverage]
    public class ClienteEmptyCpfCnpjException : BusinessException
    {

        public ClienteEmptyCpfCnpjException() : base("Cliente com cpf ou cpnj vazios")
        {
        }
    }
}