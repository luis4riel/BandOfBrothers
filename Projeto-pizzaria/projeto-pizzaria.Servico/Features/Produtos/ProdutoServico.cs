using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto_pizzaria.Dominio.Exceptions;
using Projeto_pizzaria.Dominio.Features.Produtos;

namespace projeto_pizzaria.Servico.Features.Produtos
{
    public class ProdutoServico : IProdutoServico
    {
        IProdutoRepositorio _repositorio;

        public ProdutoServico(IProdutoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public Produto Atualizar(Produto entidade)
        {
            entidade.Validar();

            if (entidade.Id == 0)
                throw new IdentifierUndefinedException();
            else
                return _repositorio.Atualizar(entidade);
        }

        public void Deletar(Produto entidade)
        {
            if (entidade.Id == 0)
                throw new IdentifierUndefinedException();
            else
                _repositorio.Deletar(entidade);
        }

        public Produto PegarPorId(long id)
        {
            if (id == 0)
                throw new IdentifierUndefinedException();
            else
                return _repositorio.PegarPorId(id);
        }

        public IEnumerable<Produto> PegarTodosAdicionais()
        {
            return _repositorio.PegarTodasAdicionais();
        }

        public IEnumerable<Produto> PegarTodasBebidas()
        {
            return _repositorio.PegarTodasBebidas();
        }

        public IEnumerable<Produto> PegarTodasPizzas()
        {
            return _repositorio.PegarTodasPizzas();
        }
        public IEnumerable<Produto> PegarTodasPizzasPorTamanho(TamanhoEnum tamanho)
        {
            return _repositorio.PegarTodasPizzasPorTamanho(tamanho);
        }
        public IEnumerable<Produto> PegarTodos()
        {
            return _repositorio.PegarTodos();
        }

        public IEnumerable<Produto> PegarTodosCalzones()
        {
            return _repositorio.PegarTodosCalzones();
        }

        public Produto Salvar(Produto entidade)
        {
            return _repositorio.Salvar(entidade);
        }
    }
}
