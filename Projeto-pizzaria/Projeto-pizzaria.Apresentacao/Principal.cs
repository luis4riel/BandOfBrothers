using projeto_pizzaria.Servico.Features.Clientes;
using projeto_pizzaria.Servico.Features.Pedidos;
using projeto_pizzaria.Servico.Features.Produtos;
using Projeto_pizzaria.Apresentacao.Features.Clientes;
using Projeto_pizzaria.Apresentacao.Features.Pedidos;
using Projeto_pizzaria.Dominio.Features.Clientes;
using Projeto_pizzaria.Dominio.Features.Produtos;
using Projeto_pizzaria.Features.Pedidos;
using Projeto_pizzaria.Infra.Data.Context;
using Projeto_pizzaria.Infra.Data.Features.Clientes;
using Projeto_pizzaria.Infra.Data.Features.Pedidos;
using Projeto_pizzaria.Infra.Data.Features.Produtos;
using System;
using System.Windows.Forms;

namespace Projeto_pizzaria.Apresentacao
{
    public partial class Principal : Form
    {
        private IPedidoRepositorio _pedidoRepositorio;
        private IClienteRepositorio _clienteRepositorio;
        private IProdutoRepositorio _produtoRepositorio;

        private PedidoServico _pedidoServico;
        private ClienteServico _clienteServico;
        private ProdutoServico _produtoServico;

        private GerenciadorFormulario _gerenciador;
        private GerenciadorFormularioPedido _pedidoGerenciadorFormulario;
        private GerenciadorFormularioCliente _clienteGerenciadorFormulario;
        private GerenciadorFormularioProduto _produtoGerenciadorFormulario;

        private PizzariaContext _contexto;

        public Principal()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.FixedSingle;
			_contexto = new PizzariaContext();

			_pedidoRepositorio = new PedidoRepositorio( _contexto );
            _clienteRepositorio = new ClienteRepositorio( _contexto );
            _produtoRepositorio = new ProdutoRepositorio(_contexto);

            _pedidoServico = new PedidoServico(_pedidoRepositorio);
            _clienteServico = new ClienteServico(_clienteRepositorio);
            _produtoServico = new ProdutoServico(_produtoRepositorio);
        }

        private void CarregarCadastro(GerenciadorFormulario gerenciador)
        {
            _gerenciador = gerenciador;

            lblStatus.Text = _gerenciador.ObtemTipoCadastro();

            UserControl listagem = _gerenciador.CarregarListagem();

            listagem.Dock = DockStyle.Fill;

            panControl.Controls.Clear();

            panControl.Controls.Add(listagem);

            _gerenciador.AtualizarLista();

            if (tbPesquisar.Visible == false)
            {
                tbPesquisar.Enabled = true;
                tbPesquisar.Visible = true;
            }

            btnAdd.Enabled = true;
            btnDelete.Enabled = false;
            btnSet.Enabled = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                _gerenciador.Adicionar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atenção");
                _gerenciador.Adicionar();
            }
        }
        
        private void pedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_pedidoGerenciadorFormulario == null)
                _pedidoGerenciadorFormulario = new GerenciadorFormularioPedido(_pedidoServico, _contexto, _produtoServico);

            CarregarCadastro(_pedidoGerenciadorFormulario);

            tbPesquisar.Visible = true;
            tbPesquisar.Enabled = true;
            btnPesquisar.Enabled = true;
        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_clienteGerenciadorFormulario == null)
                _clienteGerenciadorFormulario = new GerenciadorFormularioCliente(_clienteServico, _contexto);

            CarregarCadastro(_clienteGerenciadorFormulario);

            tbPesquisar.Visible = false;
            tbPesquisar.Enabled = false;
        }

        private void produtoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_produtoGerenciadorFormulario == null)
                _produtoGerenciadorFormulario = new GerenciadorFormularioProduto(_produtoServico, _contexto);

            CarregarCadastro(_produtoGerenciadorFormulario);

            tbPesquisar.Visible = false;
            tbPesquisar.Enabled = false;
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            try
            {
                _gerenciador.Editar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atenção");
                _gerenciador.Editar();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                _gerenciador.Excluir();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atenção");
                _gerenciador.Excluir();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                _gerenciador.Pesquisar(tbPesquisar.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atenção");
            }
        }

    }
}
