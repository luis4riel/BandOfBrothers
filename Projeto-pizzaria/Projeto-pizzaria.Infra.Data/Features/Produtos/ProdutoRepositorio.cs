using Projeto_pizzaria.Dominio.Features.Produtos;
using Projeto_pizzaria.Infra.Data.Context;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Projeto_pizzaria.Infra.Data.Features.Produtos
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private PizzariaContext _contexto;

        public ProdutoRepositorio( PizzariaContext contexto)
        {
            _contexto = contexto;
        }

        public Produto Atualizar(Produto entidade)
        {
            DbEntityEntry dbEntityEntry = _contexto.Entry(entidade);

            if (dbEntityEntry.State == EntityState.Detached)
                _contexto.Produtos.Attach(entidade);

            _contexto.SaveChanges();

            return entidade;
        }

        public void Deletar(Produto entidade)
        {
            DbEntityEntry dbEntityEntry = _contexto.Entry(entidade);

            if (dbEntityEntry.State == EntityState.Detached)
                _contexto.Produtos.Attach(entidade);

            _contexto.Produtos.Remove(entidade);

            _contexto.SaveChanges();
        }

        public Produto PegarPorId(long id)
        {
            return _contexto.Produtos.Find(id);
        }

        public IEnumerable<Produto> PegarTodasPizzas()
        {
            return _contexto.Produtos.Where(c => c.Tipo == TipoProdutoEnum.Pizza).ToList();
        }
        public IEnumerable<Produto> PegarTodasPizzasPorTamanho(TamanhoEnum tamanho)
        {
            return _contexto.Produtos.Where(p => p.Tamanho == tamanho && p.Tipo == TipoProdutoEnum.Pizza).ToList();
        }
        public IEnumerable<Produto> PegarTodosCalzones()
        {
            return _contexto.Produtos.Where(c => c.Tipo == TipoProdutoEnum.Calzone).ToList();
        }

        public IEnumerable<Produto> PegarTodasAdicionais()
        {
            return _contexto.Produtos.Where(c => c.Tipo == TipoProdutoEnum.Adicional).ToList();
        }

        public IEnumerable<Produto> PegarTodasBebidas()
        {
            return _contexto.Produtos.Where(c => c.Tipo == TipoProdutoEnum.Bebida).ToList();
        }

        public Produto Salvar(Produto entidade)
        {
            _contexto.Produtos.Add(entidade);
            _contexto.SaveChanges();

            return entidade;
        }

        public IEnumerable<Produto> PegarTodos()
        {
            return _contexto.Produtos.ToList();
        }
    }
}
