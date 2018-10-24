using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Projeto_pizzaria.Dominio.Features.ItensPedido;

namespace Projeto_pizzaria.Apresentacao.Features.ItensPedido
{
	public partial class ItensPedidoControl : UserControl
	{
		public ItensPedidoControl()
		{
			InitializeComponent();
		}

		public void PopularListagemItensPedido( List<ItemPedido> produtos )
		{
			listItensPedido.Items.Clear();

			foreach (ItemPedido produto in produtos)
				listItensPedido.Items.Add( produto );
		}

		public ItemPedido GetItensPedido()
		{
			return listItensPedido.SelectedItem as ItemPedido;
		}
	}
}
