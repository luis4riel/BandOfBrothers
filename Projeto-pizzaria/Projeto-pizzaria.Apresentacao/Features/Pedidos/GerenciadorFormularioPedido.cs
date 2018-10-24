using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using projeto_pizzaria.Servico.Features.Pedidos;
using projeto_pizzaria.Servico.Features.Produtos;
using Projeto_pizzaria.Dominio.Features.Pedidos;
using Projeto_pizzaria.Infra.Data.Context;

namespace Projeto_pizzaria.Apresentacao.Features.Pedidos
{
    public class GerenciadorFormularioPedido : GerenciadorFormulario
    {
        private PedidoServico _pedidoServico;
        private ProdutoServico _produtoServico;

        private PedidoControl _pedidoControl;
        private PizzariaContext _contexto;

        public GerenciadorFormularioPedido(PedidoServico pedidoServico, PizzariaContext contexto, ProdutoServico produtoServico)
        {
            _pedidoServico = pedidoServico;
            _produtoServico = produtoServico;
            _contexto = contexto;
        }

        public override void Adicionar()
        {
            FormCadastroPedido dialog = new FormCadastroPedido(_contexto, _produtoServico);
            DialogResult resultado = dialog.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                try
                {
                    _pedidoServico.Salvar(dialog.NovoPedido);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    resultado = DialogResult.None;
                }

                ListarPedidos();
            }
        }

        public override void AtualizarLista()
        {
            ListarPedidos();
        }

        public override UserControl CarregarListagem()
        {
            if (_pedidoControl == null)
                _pedidoControl = new PedidoControl(_contexto);

            return _pedidoControl;
        }

        public override void Editar()
        {
            Pedido pedidoSelecionado = _pedidoControl.PedidoSelecionado();
            FormAtualizarPedido dialog = new FormAtualizarPedido();
            dialog.AtualizarPedido(pedidoSelecionado);
            DialogResult resultado = dialog.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                try
                {
                    _pedidoServico.Atualizar(dialog.RetornaPedido());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    resultado = DialogResult.None;
                }

                AtualizarLista();
            }
        }

        public override void Excluir()
        {
            throw new NotImplementedException();
        }

        public override string ObtemTipoCadastro()
        {
            return "Cadastro de Pedidos";
        }

        private void ListarPedidos()
        {
            IEnumerable<Pedido> pedidos = _pedidoServico.PegarTodos();

            _pedidoControl.PopularListaPedidos(pedidos);
        }

        public override void Pesquisar(string pesquisa)
        {
            if (!string.IsNullOrWhiteSpace(pesquisa) || pesquisa != "")
            {
                var query = from c in _contexto.Pedidos
                            where (c.Cliente.Telefone.Contains(pesquisa) || c.Cliente.Nome.Contains(pesquisa))
                            select c;

                IList<Pedido> result = query.ToList();

                _pedidoControl.PopularListaPedidos(result);
            }
        }
    }
}
