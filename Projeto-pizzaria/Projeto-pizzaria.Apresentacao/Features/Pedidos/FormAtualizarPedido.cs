using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Projeto_pizzaria.Dominio;
using Projeto_pizzaria.Dominio.Features.Pedidos;

namespace Projeto_pizzaria.Apresentacao.Features.Pedidos
{
	public partial class FormAtualizarPedido : Form
	{
		Pedido _pedido = new Pedido();

		public FormAtualizarPedido()
		{
			InitializeComponent();
			CarregaStatus();
		}

		public void AtualizarPedido( Pedido pedido )
		{
			_pedido = pedido;
			lblDadosPedidos.Text = "Código: " + _pedido.Id
					+ " - Status: " + _pedido.StatusPedido
					+ " - Telefone: " + _pedido.Cliente.Telefone;
		}

		public Pedido RetornaPedido()
		{
			if (cbStatus.SelectedItem != null)
				_pedido.StatusPedido = (StatusPedidoEnum) cbStatus.SelectedItem;

			return _pedido;
		}

		public void CarregaStatus()
		{
			foreach (var item in Enum.GetValues( typeof( StatusPedidoEnum ) ))
				cbStatus.Items.Add( item );
		}
	}
}
