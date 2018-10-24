using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using projeto_pizzaria.Servico.Features.Pedidos;
using projeto_pizzaria.Servico.Features.Produtos;
using Projeto_pizzaria.Apresentacao.Features.Produtos;
using Projeto_pizzaria.Dominio.Features.Adicionais;
using Projeto_pizzaria.Dominio.Features.Bebidas;
using Projeto_pizzaria.Dominio.Features.Calzones;
using Projeto_pizzaria.Dominio.Features.Pedidos;
using Projeto_pizzaria.Dominio.Features.Pizzas;
using Projeto_pizzaria.Dominio.Features.Produtos;
using Projeto_pizzaria.Infra.Data.Context;

namespace Projeto_pizzaria.Apresentacao.Features.Pedidos
{
    public class GerenciadorFormularioProduto : GerenciadorFormulario
    {
        private ProdutoServico _produtoServico;
        private ProdutoControl _produtoControl;
        private PizzariaContext _contexto;

        public GerenciadorFormularioProduto(ProdutoServico produtoServico, PizzariaContext contexto)
        {
            _produtoServico = produtoServico;
            _contexto = contexto;
        }

        public override void Adicionar()
        {
            FormCadastroProduto dialog = new FormCadastroProduto(_contexto, _produtoServico);
            DialogResult resultado = dialog.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                try
                {
                    Pizza pizza = dialog.NovaPizza;
                    Bebida bebida = dialog.NovaBebida;
                    Adicional adicional = dialog.NovoAdicional;
                    Calzone calzone = dialog.NovoCalzone;

                    if (pizza != null)
                        _produtoServico.Salvar(pizza);
                    else if (bebida != null)
                        _produtoServico.Salvar(bebida);
                    else if (adicional != null)
                        _produtoServico.Salvar(adicional);
                    else if (calzone != null)
                        _produtoServico.Salvar(calzone);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                ListarProdutos();
            }
        }

        public override void AtualizarLista()
        {
            ListarProdutos();
        }

        public override UserControl CarregarListagem()
        {
            if (_produtoControl == null)
                _produtoControl = new ProdutoControl(_contexto);

            return _produtoControl;
        }

        public override void Editar()
        {
            Produto produtoSelecionado = _produtoControl.ProdutoSelecionado();

            FormCadastroProduto dialog = new FormCadastroProduto(_contexto, _produtoServico);

            dialog.Text = "Atualizar Produto";

            dialog.AtualizaProduto(produtoSelecionado);

            DialogResult resultado = dialog.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                try
                {
                    Pizza pizza = dialog.NovaPizza;
                    Bebida bebida = dialog.NovaBebida;
                    Adicional adicional = dialog.NovoAdicional;
                    Calzone calzone = dialog.NovoCalzone;

                    if (pizza != null)
                        _produtoServico.Atualizar(pizza);
                    else if (bebida != null)
                        _produtoServico.Atualizar(bebida);
                    else if (adicional != null)
                        _produtoServico.Atualizar(adicional);
                    else if (calzone != null)
                        _produtoServico.Atualizar(calzone);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                ListarProdutos();
            }
        }

        public override void Excluir()
        {
            throw new NotImplementedException();
        }

        public override string ObtemTipoCadastro()
        {
            return "Cadastro de Produtos";
        }

        public override void Pesquisar(string pesquisa)
        {
            throw new NotImplementedException();
        }

        private void ListarProdutos()
        {
            IEnumerable<Produto> produtos = _produtoServico.PegarTodos();

            _produtoControl.PopularListaProduto(produtos);
        }
    }
}
