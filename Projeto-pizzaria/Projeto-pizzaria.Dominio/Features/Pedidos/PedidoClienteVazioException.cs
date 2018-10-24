using Projeto_pizzaria.Dominio.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Projeto_pizzaria.Dominio.Features.Pedidos
{
    [ExcludeFromCodeCoverage]
    public class PedidoClienteVazioException : BusinessException
    {
        public PedidoClienteVazioException() : base("O Cliente deve ser preenchido")
        {
        }
    }
}