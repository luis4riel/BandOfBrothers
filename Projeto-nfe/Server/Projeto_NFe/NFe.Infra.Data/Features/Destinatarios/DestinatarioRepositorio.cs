using NFe.Dominio.Exceptions;
using NFe.Dominio.Features.Destinatarios;
using NFe.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFe.Infra.Data.Features.Destinatarios
{
    public class DestinatarioRepositorio : IDestinatarioRepositorio
    {
        NFeDBContext _context;

        public DestinatarioRepositorio(NFeDBContext context)
        {
            _context = context;
        }
        public bool Atualizar(Destinatario entidade)
        {
            _context.Entry(entidade).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }

        public bool Deletar(int id)
        {
            var destinatario = _context.Destinatarios.Where(d => d.Id == id).FirstOrDefault();
            if (destinatario == null)
                throw new NotFoundException();
            _context.Entry(destinatario).State = EntityState.Deleted;
            return _context.SaveChanges() > 0;
        }

        public Destinatario PegarPorId(long id)
        {
            return _context.Destinatarios.FirstOrDefault(d => d.Id == id);

        }

        public IQueryable<Destinatario> PegarTodos()
        {
            return _context.Destinatarios;
        }

        public Destinatario Salvar(Destinatario entidade)
        {
            var novoDestinatario = _context.Destinatarios.Add(entidade);
            _context.SaveChanges();
            return novoDestinatario;
        }
    }
}
