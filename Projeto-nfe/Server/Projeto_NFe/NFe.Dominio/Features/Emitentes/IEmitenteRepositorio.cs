using NFe.Dominio.Base;
using System.Linq;

namespace NFe.Dominio.Features.Emitentes
{
    public interface IEmitenteRepositorio
    {
        Emitente Salvar(Emitente emitente);
        bool Atualizar(Emitente emitente);
        bool Deletar(int id);
        IQueryable<Emitente> PegarTodos();
        Emitente PegarPorId(long id);
    }
}
