using Projeto_pizzaria.Dominio.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Projeto_pizzaria.Dominio.Features.Pedidos
{
    [ExcludeFromCodeCoverage]
    public class PedidoPessoaJuridicaSemResponsavelException : BusinessException
    {
        public PedidoPessoaJuridicaSemResponsavelException() : base("O responsável deve ser preenchido para pedido com pessoa juridica")
        {
        }
    }
}