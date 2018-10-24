using NFe.Aplicacao.Features.Transportadores.Commands;
using NFe.Aplicacao.Features.Transportadores.Queries;
using NFe.Dominio.Features.Transportadores;
using System.Linq;

namespace NFe.Aplicacao.Features.Transportadores
{
    public interface ITransportadorServico
    {
        int Adicionar(TransportadorRegisterCommand transportadorCmd);

        bool Atualizar(TransportadorUpdateCommand transportadorCmd);

        TransportadorQuery PegarPorId(int id);

        IQueryable<Transportador> PegarTodos();

        bool Deletar(TransportadorRemoveCommand transportadorCmd);
    }
}
