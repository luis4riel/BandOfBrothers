using NFe.Aplicacao.Features.Notas_Fiscais.Commands;
using NFe.Aplicacao.Features.Notas_Fiscais.Queries;
using NFe.Dominio.Features.Notas_Fiscais;
using System.Linq;

namespace NFe.Aplicacao.Features.Notas_Fiscais
{
    public interface INotaFiscalServico
    {
        int Adicionar(NotaFiscalRegisterCommand notaFiscalCmd);

        bool Atualizar(NotaFiscalUpdateCommand notaFiscalCmd);

        NotaFiscalQuery PegarPorId(int id);

        IQueryable<NotaFiscal> PegarTodos();

        bool Deletar(NotaFiscalRemoveCommand notaFiscalCmd);
    }
}
