using NFe.Dominio.Base;
using System.Linq;

namespace NFe.Dominio.Features.Destinatarios
{
    public interface IDestinatarioRepositorio
    { 
        Destinatario Salvar(Destinatario destinatario);
        bool Atualizar(Destinatario destinatario);
        bool Deletar(int id);
        IQueryable<Destinatario> PegarTodos();
        Destinatario PegarPorId(long id);
    }
}
