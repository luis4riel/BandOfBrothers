using NFe.Aplicacao.Features.Destinatarios.Commands;
using NFe.Aplicacao.Features.Destinatarios.Queries;
using NFe.Dominio.Features.Destinatarios;
using System.Linq;

namespace NFe.Aplicacao.Features.Destinatarios
{
    public interface IDestinatarioServico
    {
        int Adicionar(DestinatarioRegisterCommand destinatarioCmd);

        bool Atualizar(DestinatarioUpdateCommand destinatarioCmd);

        DestinatarioQuery PegarPorId(int id);

        IQueryable<Destinatario> PegarTodos();

        bool Deletar(DestinatarioRemoveCommand destinatarioCmd);
    }
}
