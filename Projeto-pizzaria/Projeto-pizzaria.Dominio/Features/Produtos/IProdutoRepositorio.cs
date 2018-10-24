using Projeto_pizzaria.Dominio.Base;
using System.Collections.Generic;

namespace Projeto_pizzaria.Dominio.Features.Produtos
{
    public interface IProdutoRepositorio : IRepositorio<Produto>
    {
        IEnumerable<Produto> PegarTodasPizzas();
        IEnumerable<Produto> PegarTodasPizzasPorTamanho(TamanhoEnum tamanho);
        IEnumerable<Produto> PegarTodosCalzones();
        IEnumerable<Produto> PegarTodasAdicionais();
        IEnumerable<Produto> PegarTodasBebidas();
    }
}
