using Projeto_pizzaria.Aplicacao.Base;
using Projeto_pizzaria.Dominio.Features.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_pizzaria.Servico.Features.Produtos
{
    public interface IProdutoServico : IServico<Produto>
    {
        IEnumerable<Produto> PegarTodasPizzas();
        IEnumerable<Produto> PegarTodosCalzones();
        IEnumerable<Produto> PegarTodosAdicionais();
        IEnumerable<Produto> PegarTodasBebidas();
    }
}
