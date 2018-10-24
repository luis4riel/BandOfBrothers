using projeto_pizzaria.Servico.Features.Clientes;
using Projeto_pizzaria.Dominio.Features.Clientes;
using Projeto_pizzaria.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_pizzaria.Apresentacao.Features.Clientes
{
    public class GerenciadorFormularioCliente : GerenciadorFormulario
    {
        private ClienteServico _clienteServico;
        private ClienteControl _clienteControl;
		private PizzariaContext _contexto;

        public GerenciadorFormularioCliente(ClienteServico clienteServico, PizzariaContext contexto )
        {
            _clienteServico = clienteServico;
			_contexto = contexto;
		}

        public override void Adicionar()
        {
            FormCadastroCliente dialog = new FormCadastroCliente();
            dialog.NovaPropriedade = new Cliente();
            DialogResult resultado = dialog.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                try
                {
                    _clienteServico.Salvar(dialog.NovaPropriedade);
                }
                catch (Exception ex)
                {
                    resultado = DialogResult.None;
                    MessageBox.Show(ex.Message);
                }
            }
            AtualizarLista();
        }

        public override void AtualizarLista()
        {
            ListarClientes();
        }

        public override UserControl CarregarListagem()
        {
            if (_clienteControl == null)
                _clienteControl = new ClienteControl(_contexto);

            return _clienteControl;
        }

        public override void Editar()
        {
            Cliente clienteSelecionado = _clienteControl.ClienteSelecionado();
            FormCadastroCliente dialog = new FormCadastroCliente();

            dialog.Text = "Atualizar Cliente";
            dialog.NovaPropriedade = clienteSelecionado;

            DialogResult resultado = dialog.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                try
                {
                    _clienteServico.Atualizar(dialog.NovaPropriedade);
                }
                catch (Exception ex)
                {
                    resultado = DialogResult.None;
                    MessageBox.Show(ex.Message);
                }
                AtualizarLista();
            }
        }

        public override void Excluir()
        {
            Cliente clienteSelecionado = _clienteControl.ClienteSelecionado();
            if(clienteSelecionado.Pedidos == null || clienteSelecionado.Pedidos.Count == 0)
            {
                DialogResult result = MessageBox.Show("Deseja realmente excluir o cliente " + clienteSelecionado.Nome);

                if ( result == DialogResult.OK)
                    _clienteServico.Deletar(clienteSelecionado);

                AtualizarLista();
            }
        }

        public override string ObtemTipoCadastro()
        {
            return "Cadastro de Clientes";
        }
        private void ListarClientes()
        {
            IEnumerable<Cliente> clientes = _clienteServico.PegarTodos();

            _clienteControl.PopularListaClientes(clientes);
        }

        public override void Pesquisar(string pesquisa)
        {
            
        }
    }
}
