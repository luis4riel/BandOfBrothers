using NFe.Dominio.Base;
using System.Linq;

namespace NFe.Dominio.Features.Enderecos
{
    public interface IEnderecoRepositorio
    {
        Localizacao Salvar(Localizacao endereco);
        bool Atualizar(Localizacao endereco);
        bool Deletar(int id);
        IQueryable<Localizacao> PegarTodos();
        Localizacao PegarPorId(long id);
    }
}
