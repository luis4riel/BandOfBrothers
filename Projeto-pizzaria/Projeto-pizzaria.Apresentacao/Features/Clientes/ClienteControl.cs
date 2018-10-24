using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Projeto_pizzaria.Dominio.Features.Clientes;
using projeto_pizzaria.Servico.Features.Clientes;
using Projeto_pizzaria.Infra.Data.Features.Clientes;
using Projeto_pizzaria.Infra.Data.Context;

namespace Projeto_pizzaria.Apresentacao.Features.Clientes
{
    public partial class ClienteControl : UserControl
    {
        private IClienteRepositorio _clienteRepositorio;
        private ClienteServico _clienteServico;
		PizzariaContext _contexto;

		public ClienteControl( PizzariaContext contexto)
        {
            InitializeComponent();

			_contexto = contexto;

			_clienteRepositorio = new ClienteRepositorio(_contexto);
            _clienteServico = new ClienteServico(_clienteRepositorio);
        }

        public void PopularListaClientes(IEnumerable<Cliente> listaClientes)
        {
            listClientes.Items.Clear();

            foreach (var cliente in listaClientes)
            {
                listClientes.Items.Add(cliente);
            }
        }

        public Cliente ClienteSelecionado() => (Cliente)listClientes.SelectedItem;

    }
}
