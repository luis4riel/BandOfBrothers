using Projeto_pizzaria.Dominio.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Projeto_pizzaria.Dominio.Features.Pedidos
{
    [ExcludeFromCodeCoverage]
    public class PedidoPessoaJuridicaSemDepartamentoException : BusinessException
    {
        public PedidoPessoaJuridicaSemDepartamentoException() : base("O departamento deve ser preenchido para pedido com pessoa juridica")
        {
        }
    }
}