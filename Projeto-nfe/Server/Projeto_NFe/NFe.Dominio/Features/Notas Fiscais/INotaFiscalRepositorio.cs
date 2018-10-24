using NFe.Dominio.Base;
using System.Linq;

namespace NFe.Dominio.Features.Notas_Fiscais
{
    public interface INotaFiscalRepositorio
    {
        NotaFiscal Salvar(NotaFiscal notaFiscal);
        bool Atualizar(NotaFiscal notaFiscal);
        bool Deletar(int id);
        IQueryable<NotaFiscal> PegarTodos();
        NotaFiscal PegarPorId(long id);
    }
}
