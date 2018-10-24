using Projeto_pizzaria.Dominio.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Projeto_pizzaria.Dominio.Features.Pedidos
{
    [ExcludeFromCodeCoverage]
    public class PedidoSemItemException : BusinessException
    {
        public PedidoSemItemException() : base("Não pode existir pedido sem itens")
        {
        }
    }
}