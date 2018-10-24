using NFe.Dominio.Exceptions;
using NFe.Dominio.Features.Emitentes;
using NFe.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFe.Infra.Data.Features.Emitentes
{
    public class EmitenteRepositorio : IEmitenteRepositorio
    {
        NFeDBContext _context;

        public EmitenteRepositorio(NFeDBContext context)
        {
            _context = context;
        }
        public bool Atualizar(Emitente entidade)
        {
            _context.Entry(entidade).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }

        public bool Deletar(int id)
        {
            var emitente = _context.Emitentes.Where(e => e.Id == id).FirstOrDefault();
            if (emitente == null)
                throw new NotFoundException();
            _context.Entry(emitente).State = EntityState.Deleted;
            return _context.SaveChanges() > 0;
        }

        public Emitente PegarPorId(long id)
        {
            return _context.Emitentes.FirstOrDefault(e => e.Id == id);
        }

        public IQueryable<Emitente> PegarTodos()
        {
            return _context.Emitentes;
        }

        public Emitente Salvar(Emitente entidade)
        {
            var novoEmitente = _context.Emitentes.Add(entidade);
            _context.SaveChanges();
            return novoEmitente;
        }
    }
}
