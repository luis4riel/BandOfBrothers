using NFe.Aplicacao.Features.Produtos.Commands;
using NFe.Dominio.Features.Produtos;
using NFe.Dominio.Features.Valores;
using System.Collections.Generic;

namespace NFe.Common.Testes.Features
{
    public static partial class ObjectMother
    {
        public static ProdutoRegisterCommand ObtemProdutoValidoAdicionar()
        {
            return new ProdutoRegisterCommand()
            {
                CodigoProduto = 1,
                Descricao = "Produto X",
                Quantidade = 1,
                ValorProdutoId = 1,
            };
        }

        public static ProdutoRemoveCommand ObtemProdutoValidoRemover()
        {
            return new ProdutoRemoveCommand()
            {
                Id = 1,
            };
        }

        public static ProdutoUpdateCommand ObtemProdutoValidoAtualizar()
        {
            return new ProdutoUpdateCommand()
            {
                Id = 1,
                CodigoProduto = 1,
                Descricao = "Produto X",
                Quantidade = 1,
                ValorProdutoId = 1,
            };
        }

        public static Produto ObtemProdutoValido()
        {
            return new Produto()
            {
                Id = 1,
                CodigoProduto = 12345,
                Descricao = "Mouse Optico",
                Quantidade = 1,
                ValorProduto = new ValorProduto()
                {
                    Unitario = 15.0m,
                }
            };
        }

        public static Produto ObtemProdutoValido2()
        {
            return new Produto()
            {
                Id = 2,
                CodigoProduto = 12345,
                Descricao = "Mouse Optico",
                Quantidade = 1,
                ValorProduto = new ValorProduto()
                {
                    Unitario = 15.0m,
                }
            };
        }

        public static List<Produto> ObtemListaProdutoValido()
        {
            List<Produto> lista = new List<Produto>();

            lista.Add(ObtemProdutoValido());
            lista.Add(ObtemProdutoValido2());

            return lista;
        }

        public static Produto ObtemProdutoDescricaoVazio()
        {
            return new Produto()
            {
                CodigoProduto = 12345,
                Quantidade = 1,
                ValorProduto = new ValorProduto()
                {
                    Unitario = 15.0m,
                }
            };
        }

        public static Produto ObtemCodigoProdutoIgualAZero()
        {
            return new Produto()
            {
                CodigoProduto = 0,
                Descricao = "Mouse Optico",
                Quantidade = 1,
                ValorProduto = new ValorProduto()
                {
                    Unitario = 15.0m,
                }
            };
        }

        public static Produto ObtemProdutoQuantidadeIgualAZero()
        {
            return new Produto()
            {
                CodigoProduto = 12345,
                Descricao = "Mouse Optico",
                Quantidade = 0,
                ValorProduto = new ValorProduto()
                {
                    Unitario = 15.0m,
                }
            };
        }

        public static Produto ObtemProdutoValorUnitarioZerado()
        {
            return new Produto()
            {
                CodigoProduto = 12345,
                Descricao = "Mouse Optico",
                Quantidade = 1,
                ValorProduto = new ValorProduto()
                {
                    Unitario = 0m,
                }
            };
        }

    }
}
