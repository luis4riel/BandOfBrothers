using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Projeto_pizzaria.Infra.Data.Context;
using projeto_pizzaria.Servico.Features.Produtos;
using Projeto_pizzaria.Dominio.Features.Produtos;
using Projeto_pizzaria.Infra.Data.Features.Produtos;

namespace Projeto_pizzaria.Apresentacao.Features.Produtos
{
    public partial class ProdutoControl : UserControl
    {
        private IProdutoRepositorio _produtoRepositorio;
        private ProdutoServico _produtoServico;
        PizzariaContext _contexto;

        public ProdutoControl(PizzariaContext contexto)
        {
            InitializeComponent();
            _contexto = contexto;

            _produtoRepositorio = new ProdutoRepositorio(_contexto);
            _produtoServico = new ProdutoServico(_produtoRepositorio);
        }
        public void PopularListaProduto(IEnumerable<Produto> listaProdutos)
        {
            listProdutos.Items.Clear();

            foreach (var produto in listaProdutos)
                listProdutos.Items.Add(produto);
        }

        public Produto ProdutoSelecionado() => (Produto)listProdutos.SelectedItem;

    }
}
