using AutoMapper;
using NFe.Aplicacao.Features.Produtos.Commands;
using NFe.Aplicacao.Features.Produtos.Queries;
using NFe.Dominio.Exceptions;
using NFe.Dominio.Features.Produtos;
using System.Linq;

namespace NFe.Aplicacao.Features.Produtos
{
    public class ProdutoServico : IProdutoServico
    {
        IProdutoRepositorio _repositorioProduto;

        public ProdutoServico(IProdutoRepositorio repositorio)
        {
            _repositorioProduto = repositorio;
        }

        public int Adicionar(ProdutoRegisterCommand produtoCmd)
        {
            var produto = Mapper.Map<ProdutoRegisterCommand, Produto>(produtoCmd);
            var novoProduto = _repositorioProduto.Salvar(produto);

            return (int)novoProduto.Id;
        }

        public bool Atualizar(ProdutoUpdateCommand produtoCmd)
        {
            var produtoDb = _repositorioProduto.PegarPorId(produtoCmd.Id) ?? throw new NotFoundException();
          
            Mapper.Map<ProdutoUpdateCommand, Produto>(produtoCmd, produtoDb);

            return _repositorioProduto.Atualizar(produtoDb);
        }

        public bool Deletar(ProdutoRemoveCommand produtoCmd)
        {
            return _repositorioProduto.Deletar(produtoCmd.Id);
        }

        public ProdutoQuery PegarPorId(int id)
        {
            var produto = _repositorioProduto.PegarPorId(id);

            return Mapper.Map<Produto, ProdutoQuery>(produto);
        }

        IQueryable<Produto> IProdutoServico.PegarTodos()
        {
         return _repositorioProduto.PegarTodos();
        }
    }
}
