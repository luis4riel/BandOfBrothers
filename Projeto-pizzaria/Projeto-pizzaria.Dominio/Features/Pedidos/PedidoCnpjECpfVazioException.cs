using Projeto_pizzaria.Dominio.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Projeto_pizzaria.Dominio.Features.Pedidos
{
    [ExcludeFromCodeCoverage]
    public class PedidoCnpjECpfVazioException : BusinessException
    {
        public PedidoCnpjECpfVazioException() : base("Para emissão de notas, o Cnpj ou Cpf devem ser preenchidos.")
        {
        }
    }
}