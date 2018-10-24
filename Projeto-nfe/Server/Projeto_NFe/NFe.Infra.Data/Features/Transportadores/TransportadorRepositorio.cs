using NFe.Dominio.Exceptions;
using NFe.Dominio.Features.Transportadores;
using NFe.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFe.Infra.Data.Features.Transportadores
{
    public class TransportadorRepositorio : ITransportadorRepositorio
    {
        NFeDBContext _context;

        public TransportadorRepositorio(NFeDBContext context)
        {
            _context = context;
        }
        public bool Atualizar(Transportador entidade)
        {
            _context.Entry(entidade).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }

        public bool Deletar(int id)
        {
            var transportador = _context.Transportadores.Where(p => p.Id == id).FirstOrDefault();
            if (transportador == null)
                throw new NotFoundException();
            _context.Entry(transportador).State = EntityState.Deleted;
            return _context.SaveChanges() > 0;
        }

        public Transportador PegarPorId(long id)
        {
            return _context.Transportadores.FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<Transportador> PegarTodos()
        {
            return _context.Transportadores;
        }

        public Transportador Salvar(Transportador entidade)
        {
            var novoTransportador = _context.Transportadores.Add(entidade);
            _context.SaveChanges();
            return novoTransportador;

        }
    }
}
