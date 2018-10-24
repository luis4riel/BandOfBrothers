using NFe.Aplicacao.Features.Produtos.Commands;
using NFe.Aplicacao.Features.Produtos.Queries;
using NFe.Dominio.Features.Produtos;
using System.Linq;

namespace NFe.Aplicacao.Features.Produtos
{
    public interface IProdutoServico
    {
        int Adicionar(ProdutoRegisterCommand produtoCmd);

        bool Atualizar(ProdutoUpdateCommand produtoCmd);

        ProdutoQuery PegarPorId(int id);

        IQueryable<Produto> PegarTodos();

        bool Deletar(ProdutoRemoveCommand produtoCmd);
    }
}
