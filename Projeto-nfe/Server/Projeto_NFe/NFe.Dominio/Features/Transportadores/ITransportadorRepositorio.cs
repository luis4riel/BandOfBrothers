using NFe.Dominio.Base;
using System.Linq;

namespace NFe.Dominio.Features.Transportadores
{
    public interface ITransportadorRepositorio
    {
        Transportador Salvar(Transportador transportador);
        bool Atualizar(Transportador transportador);
        bool Deletar(int id);
        IQueryable<Transportador> PegarTodos();
        Transportador PegarPorId(long id);
    }
}
