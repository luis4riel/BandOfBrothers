using NFe.Dominio.Exceptions;
using NFe.Dominio.Features.Notas_Fiscais;
using NFe.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFe.Infra.Data.Features.Notas_Fiscais
{
    public class NotaFIscalRepositorio : INotaFiscalRepositorio
    {
        NFeDBContext _context;

        public NotaFIscalRepositorio(NFeDBContext context)
        {
            _context = context;
        }
        public bool Atualizar(NotaFiscal entidade)
        {
            _context.Entry(entidade).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }

        public bool Deletar(int id)
        {
            var notaFiscal = _context.NotasFiscais.Where(p => p.Id == id).FirstOrDefault();
            if (notaFiscal == null)
                throw new NotFoundException();
            _context.NotasFiscais.Remove(notaFiscal);
            _context.Entry(notaFiscal).State = EntityState.Deleted;
            return _context.SaveChanges() > 0;
        }

        public NotaFiscal PegarPorId(long id)
        {
            return _context.NotasFiscais.FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<NotaFiscal> PegarTodos()
        {
            return _context.NotasFiscais;
        }

        public NotaFiscal Salvar(NotaFiscal entidade)
        {
            var novaNota = _context.NotasFiscais.Add(entidade);
            return novaNota;
        }
    }
}
