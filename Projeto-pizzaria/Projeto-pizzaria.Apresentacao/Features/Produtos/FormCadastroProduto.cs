using projeto_pizzaria.Servico.Features.Produtos;
using Projeto_pizzaria.Dominio.Features.Adicionais;
using Projeto_pizzaria.Dominio.Features.Bebidas;
using Projeto_pizzaria.Dominio.Features.Calzones;
using Projeto_pizzaria.Dominio.Features.Pizzas;
using Projeto_pizzaria.Dominio.Features.Produtos;
using Projeto_pizzaria.Infra.Data.Context;
using Projeto_pizzaria.Infra.Data.Features.Produtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_pizzaria.Apresentacao.Features.Produtos
{
    public partial class FormCadastroProduto : Form
    {
        Pizza _pizza;
        Adicional _adicional;
        Bebida _bebida;
        Calzone _calzone;
        ProdutoServico _produtoServico;

        public Pizza NovaPizza
        {
            get { return _pizza; }
            set { _pizza = value; }
        }

        public Calzone NovoCalzone
        {
            get { return _calzone; }
            set { _calzone = value; }
        }

        public Adicional NovoAdicional
        {
            get { return _adicional; }
            set { _adicional = value; }
        }

        public Bebida NovaBebida
        {
            get { return _bebida; }
            set { _bebida = value; }
        }

        public FormCadastroProduto(PizzariaContext contexto, ProdutoServico servico)
        {
            InitializeComponent();
            CarregaValoresComboBox();
            _produtoServico = servico;
        }

        public void CarregaValoresComboBox()
        {
            foreach (var item in Enum.GetValues(typeof(TipoProdutoEnum)))
                cbTipo.Items.Add(item);
            // carrega os dados do comboBox de tamanho
            foreach (var item in Enum.GetValues(typeof(TamanhoEnum)))
                cbTamanho.Items.Add(item);
        }

        public void AtualizaProduto(Produto produtoSelecionado)
        {
            if (produtoSelecionado.Tipo == TipoProdutoEnum.Pizza)
                PopulaProdutoTipoPizza(produtoSelecionado);
            else if (produtoSelecionado.Tipo == TipoProdutoEnum.Calzone)
                PopulaProdutoTipoCalzone(produtoSelecionado);
            else if (produtoSelecionado.Tipo == TipoProdutoEnum.Bebida)
                PopulaProdutoTipoBebida(produtoSelecionado);
            else if (produtoSelecionado.Tipo == TipoProdutoEnum.Adicional)
                PopulaProdutoTIpoAdicional(produtoSelecionado);
        }

        private void PopulaProdutoTIpoAdicional(Produto produtoSelecionado)
        {
            _adicional = (Adicional)produtoSelecionado;
            txtId.Text = _adicional.Id.ToString();
            txtNome.Text = _adicional.Nome;
            txtPreco.Text = _adicional.ValorProduto.ToString();
            cbTamanho.SelectedItem = _adicional.Tamanho;
            cbTipo.SelectedItem = _adicional.Tipo;
        }

        private void PopulaProdutoTipoBebida(Produto produtoSelecionado)
        {
            _bebida = (Bebida)produtoSelecionado;
            txtId.Text = _bebida.Id.ToString();
            txtNome.Text = _bebida.Nome;
            txtPreco.Text = _bebida.ValorProduto.ToString();
            cbTamanho.SelectedItem = _bebida.Tamanho;
            cbTipo.SelectedItem = _bebida.Tipo;
        }

        private void PopulaProdutoTipoCalzone(Produto produtoSelecionado)
        {
            _calzone = (Calzone)produtoSelecionado;
            txtId.Text = _calzone.Id.ToString();
            txtNome.Text = _calzone.Nome;
            txtPreco.Text = _calzone.ValorProduto.ToString();
            cbTamanho.SelectedItem = _calzone.Tamanho;
            cbTipo.SelectedItem = _calzone.Tipo;
        }

        private void PopulaProdutoTipoPizza(Produto produtoSelecionado)
        {
            _pizza = (Pizza)produtoSelecionado;
            txtId.Text = _pizza.Id.ToString();
            txtNome.Text = _pizza.Nome;
            txtPreco.Text = _pizza.ValorProduto.ToString();
            cbTamanho.SelectedItem = _pizza.Tamanho;
            cbTipo.SelectedItem = _pizza.Tipo;
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (cbTipo.SelectedItem == null)
            {
                MessageBox.Show("Selecione o tipo de produto para realizar o cadastro");
                DialogResult = DialogResult.None;
            }
            else if ((TipoProdutoEnum)cbTipo.SelectedItem == TipoProdutoEnum.Pizza)
                CadastraPizza();
            else if ((TipoProdutoEnum)cbTipo.SelectedItem == TipoProdutoEnum.Calzone)
                CadastraCalzone();
            else if ((TipoProdutoEnum)cbTipo.SelectedItem == TipoProdutoEnum.Bebida)
                CadastraBebida();
            else if ((TipoProdutoEnum)cbTipo.SelectedItem == TipoProdutoEnum.Adicional)
                CadastraAdicional();
            else
            {
                MessageBox.Show("O item selecionado não pertence aos tipos de produtos");
                DialogResult = DialogResult.None;
            }
        }

        private void CadastraAdicional()
        {
            try
            {
                if (string.IsNullOrEmpty(txtPreco.Text))
                    throw new Exception("Informe o valor do produto");

                if (!string.IsNullOrEmpty(txtId.Text))
                    _adicional = (Adicional)_produtoServico.PegarPorId(Convert.ToInt64(txtId.Text));
                else
                    _adicional = new Adicional(txtNome.Text);

                _adicional.Nome = txtNome.Text;
                _adicional.ValorProduto = Convert.ToDouble(txtPreco.Text);

                if (cbTamanho.SelectedItem != null)
                    _adicional.Tamanho = (TamanhoEnum)cbTamanho.SelectedItem;
                else
                    throw new Exception("Informe o tamanho do produto");

                if (cbTipo.SelectedItem != null)
                    _adicional.Tipo = (TipoProdutoEnum)cbTipo.SelectedItem;

                _adicional.Validar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                DialogResult = DialogResult.None;
            }
        }

        private void CadastraBebida()
        {
            try
            {
                if (string.IsNullOrEmpty(txtPreco.Text))
                    throw new Exception("Informe o valor do produto");

                if (!string.IsNullOrEmpty(txtId.Text))
                    _bebida = (Bebida)_produtoServico.PegarPorId(Convert.ToInt64(txtId.Text));
                else
                    _bebida = new Bebida(txtNome.Text, Convert.ToDouble(txtPreco.Text));

                _bebida.Nome = txtNome.Text;
                _bebida.ValorProduto = Convert.ToDouble(txtPreco.Text);

                if (cbTamanho.SelectedItem != null)
                    _bebida.Tamanho = (TamanhoEnum)cbTamanho.SelectedItem;
                else
                    throw new Exception("Informe o tamanho do produto");

                if (cbTipo.SelectedItem != null)
                    _bebida.Tipo = (TipoProdutoEnum)cbTipo.SelectedItem;

                _bebida.Validar();                    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                DialogResult = DialogResult.None;
            }
        }

        private void CadastraCalzone()
        {
            try
            {
                if (string.IsNullOrEmpty(txtPreco.Text))
                    throw new Exception("Informe o valor do produto");

                if (!string.IsNullOrEmpty(txtId.Text))
                    _calzone = (Calzone)_produtoServico.PegarPorId(Convert.ToInt64(txtId.Text));
                else
                    _calzone = new Calzone(txtNome.Text, Convert.ToDouble(txtPreco.Text));

                _calzone.Nome = txtNome.Text;
                _calzone.ValorProduto = Convert.ToDouble(txtPreco.Text);

                if (cbTamanho.SelectedItem != null)
                    _calzone.Tamanho = (TamanhoEnum)cbTamanho.SelectedItem;
                else
                    throw new Exception("Informe o tamanho do produto");

                if (cbTipo.SelectedItem != null)
                    _calzone.Tipo = (TipoProdutoEnum)cbTipo.SelectedItem;

                _calzone.Validar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                DialogResult = DialogResult.None;
            }
        }

        private void CadastraPizza()
        {
            try
            {
                if (string.IsNullOrEmpty(txtPreco.Text))
                    throw new Exception("Informe o valor do produto");

                if (!string.IsNullOrEmpty(txtId.Text))
                    _pizza = (Pizza)_produtoServico.PegarPorId(Convert.ToInt64(txtId.Text));
                else
                    _pizza = new Pizza();

                _pizza.Nome = txtNome.Text;
                _pizza.ValorProduto = Convert.ToDouble(txtPreco.Text);
                 
                if (cbTamanho.SelectedItem != null)
                    _pizza.Tamanho = (TamanhoEnum)cbTamanho.SelectedItem;
                else
                    throw new Exception("Informe o tamanho do produto");

                if (cbTipo.SelectedItem != null)
                    _pizza.Tipo = (TipoProdutoEnum)cbTipo.SelectedItem;

                _pizza.Validar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                DialogResult = DialogResult.None;
            }
        }

        #region Mascara Preço
        private void txtPreco_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled=true;
        }

        string str = "";
        private void txtPreco_KeyDown(object sender, KeyEventArgs e)
        {
            int KeyCode = e.KeyValue;

            if (!IsNumeric(KeyCode))
            {
                e.Handled = true;
                return;
            }
            else
                e.Handled = true;

            if (((KeyCode == 8) || (KeyCode == 46)) && (str.Length > 0))
                str = str.Substring(0, str.Length - 1);
            else if (!((KeyCode == 8) || (KeyCode == 46)))
                str = str + Convert.ToChar(KeyCode);
            if (str.Length == 0)
                txtPreco.Text = "";
            if (str.Length == 1)
                txtPreco.Text = "0.0" + str;
            else if (str.Length == 2)
                txtPreco.Text = "0." + str;
            else if (str.Length > 2)
                txtPreco.Text = str.Substring(0, str.Length - 2) + "." + str.Substring(str.Length - 2);
        }

        private bool IsNumeric(int Val)
        {
            return ((Val >= 48 && Val <= 57) || (Val == 8) || (Val == 46));
        }

        #endregion

        private void cbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((TipoProdutoEnum)cbTipo.SelectedItem == TipoProdutoEnum.Pizza)
                cbTamanho.Enabled = true;
            else if ((TipoProdutoEnum)cbTipo.SelectedItem == TipoProdutoEnum.Calzone)
            {
                foreach (var item in Enum.GetValues(typeof(TamanhoEnum)))
                    cbTamanho.Items.Remove(item);
                cbTamanho.Items.Add(TamanhoEnum.Unico);
                cbTamanho.Enabled = true;
            }
            else if ((TipoProdutoEnum)cbTipo.SelectedItem == TipoProdutoEnum.Bebida)
            {
                foreach (var item in Enum.GetValues(typeof(TamanhoEnum)))
                    cbTamanho.Items.Remove(item);
                cbTamanho.Items.Add(TamanhoEnum.Unico);
                cbTamanho.Enabled = true;
            }
            else if ((TipoProdutoEnum)cbTipo.SelectedItem == TipoProdutoEnum.Adicional)
            {
                foreach (var item in Enum.GetValues(typeof(TamanhoEnum)))
                    cbTamanho.Items.Remove(item);
                cbTamanho.Items.Add(TamanhoEnum.Unico);
                cbTamanho.Enabled = true;
            }
        }
    }
}
