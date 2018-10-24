using projeto_pizzaria.Servico.Features.Pedidos;
using Projeto_pizzaria.Dominio.Features.Pedidos;
using Projeto_pizzaria.Features.Pedidos;
using Projeto_pizzaria.Infra.Data.Context;
using Projeto_pizzaria.Infra.Data.Features.Pedidos;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Projeto_pizzaria.Apresentacao.Features.Pedidos
{
    public partial class PedidoControl : UserControl
    {
        private IPedidoRepositorio _pedidoRepositorio;
        private PedidoServico _pedidoServico;
		PizzariaContext _contexto;

		public PedidoControl( PizzariaContext contexto)
        {
            InitializeComponent();
			_contexto = contexto;

			_pedidoRepositorio = new PedidoRepositorio( _contexto );
            _pedidoServico = new PedidoServico(_pedidoRepositorio);
		}

        public void PopularListaPedidos(IEnumerable<Pedido> listaPedidos)
        {
            listPedidos.Items.Clear();

            foreach (var pedido in listaPedidos)
                listPedidos.Items.Add(pedido);
        }

        public Pedido PedidoSelecionado() => (Pedido)listPedidos.SelectedItem; 
    }
}
