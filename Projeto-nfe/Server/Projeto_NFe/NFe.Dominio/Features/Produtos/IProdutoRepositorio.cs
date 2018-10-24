using NFe.Dominio.Base;
using System.Linq;

namespace NFe.Dominio.Features.Produtos
{
    public interface IProdutoRepositorio 
    {
        Produto Salvar(Produto produto);
        bool Atualizar(Produto produto);
        bool Deletar(int id);
        IQueryable<Produto> PegarTodos();
        Produto PegarPorId(long id);
    }
}
