using NFe.Aplicacao.Features.Emitentes.Commands;
using NFe.Aplicacao.Features.Emitentes.Queries;
using NFe.Dominio.Features.Emitentes;
using System.Linq;

namespace NFe.Aplicacao.Features.Emitentes
{
    public interface IEmitenteServico
    {
        int Adicionar(EmitenteRegisterCommand emitenteCmd);

        bool Atualizar(EmitenteUpdateCommand emitenteCmd);

        EmitenteQuery PegarPorId(int id);

        IQueryable<Emitente> PegarTodos();

        bool Deletar(EmitenteRemoveCommand emitenteCmd);
    }
}
