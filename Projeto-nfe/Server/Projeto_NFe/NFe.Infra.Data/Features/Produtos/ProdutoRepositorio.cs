using NFe.Dominio.Exceptions;
using NFe.Dominio.Features.Produtos;
using NFe.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFe.Infra.Data.Features.Produtos
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        NFeDBContext _context;

        public ProdutoRepositorio(NFeDBContext context)
        {
            _context = context;
        }
        public bool Atualizar(Produto entidade)
        {
            _context.Entry(entidade).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }

        public bool Deletar(int id)
        {
            var produto = _context.Produtos.Where(p => p.Id == id).FirstOrDefault();
            if (produto == null)
                throw new NotFoundException();
            _context.Entry(produto).State = EntityState.Deleted;
            return _context.SaveChanges() > 0;
        }

        public Produto PegarPorId(long id)
        {
            return _context.Produtos.FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<Produto> PegarTodos()
        {
            return _context.Produtos;
        }

        public Produto Salvar(Produto entidade)
        {
            var novoProtudo = _context.Produtos.Add(entidade);
            return novoProtudo;
        }
    }
}