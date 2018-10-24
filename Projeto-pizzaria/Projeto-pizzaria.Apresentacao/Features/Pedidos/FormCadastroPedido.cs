using MosaicoSolutions.ViaCep;
using MosaicoSolutions.ViaCep.Modelos;
using projeto_pizzaria.Servico.Features.Produtos;
using Projeto_pizzaria.Apresentacao.Features.ItensPedido;
using Projeto_pizzaria.Dominio;
using Projeto_pizzaria.Dominio.Features.Adicionais;
using Projeto_pizzaria.Dominio.Features.Bebidas;
using Projeto_pizzaria.Dominio.Features.Calzones;
using Projeto_pizzaria.Dominio.Features.Clientes;
using Projeto_pizzaria.Dominio.Features.Enderecos;
using Projeto_pizzaria.Dominio.Features.ItensPedido;
using Projeto_pizzaria.Dominio.Features.Pedidos;
using Projeto_pizzaria.Dominio.Features.Pizzas;
using Projeto_pizzaria.Dominio.Features.Produtos;
using Projeto_pizzaria.Infra.Data.Context;
using Projeto_pizzaria.Infra.Data.Features.Produtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Projeto_pizzaria.Apresentacao.Features.Pedidos
{
	public partial class FormCadastroPedido : Form
	{
		Cliente _cliente;
		Localizacao _endereco;
		Pedido _pedido;
		PizzariaContext _contexto;
		TamanhoEnum _tamanho;

		ProdutoServico _servicoProduto;

		private ItensPedidoControl _userControlItensPedido;

		public Pedido NovoPedido
		{
			get { return _pedido; }
			set { _pedido = value; }
		}

		public FormCadastroPedido( PizzariaContext contexto, ProdutoServico servico)
		{
			InitializeComponent();
			_pedido = new Pedido();
			_contexto = contexto;
			_endereco = new Localizacao();

			_servicoProduto = servico;

			_userControlItensPedido = new ItensPedidoControl();
			_userControlItensPedido.Dock = DockStyle.Fill;
			panelItensPedido.Controls.Add( _userControlItensPedido );

			_tamanho = TamanhoEnum.Grande;
			LimparCamposSabores();
			CarregarSabores( _tamanho );
		}

		private void CarregarSabores( TamanhoEnum tamanho )
		{
			_tamanho = tamanho;
			IEnumerable<Produto> produtos = _servicoProduto.PegarTodos();
			IEnumerable<Produto> produtoPizza = _servicoProduto.PegarTodasPizzas();
			IEnumerable<Produto> produtoPizzaTamanho = _servicoProduto.PegarTodasPizzasPorTamanho( tamanho );
			IEnumerable<Produto> produtoCalzone = _servicoProduto.PegarTodosCalzones();
			IEnumerable<Produto> produtoBebida = _servicoProduto.PegarTodasBebidas();
			cbPrimeiroSabor.Items.Clear();
			cbSaborCalzone.Items.Clear();
			cbBebidas.Items.Clear();
			foreach (var Pizza in produtoPizzaTamanho)
				cbPrimeiroSabor.Items.Add( Pizza );
			foreach (var Calzone in produtoCalzone)
				cbSaborCalzone.Items.Add( Calzone );
			foreach (var Bebida in produtoBebida)
				cbBebidas.Items.Add( Bebida );
			CarregarSegundoSabor( tamanho );
		}

        private void CarregarSegundoSabor(TamanhoEnum tamanho)
        {

            cbSegundoSabor.Text = String.Empty;
            if (cbPrimeiroSabor.SelectedItem != null)
            {
                cbSegundoSabor.Items.Clear();

                var pizzas = _servicoProduto.PegarTodasPizzasPorTamanho(tamanho);

                foreach (var pizza in pizzas)
                    if (pizza != cbPrimeiroSabor.SelectedItem)
                        cbSegundoSabor.Items.Add(pizza);
            }
        }

        private void PopularCamposEndereco( Cliente result )
		{
			txtLogradouro.Text = result.Endereco.Logradouro;
			txtBairro.Text = result.Endereco.Bairro;
			txtCidade.Text = result.Endereco.Municipio;
			txtComplemento.Text = result.Endereco.Complemento;
			txtNumero.Text = result.Endereco.Numero;
		}

		private void PopularCamposCliente( Cliente result )
		{
			//campos pesquisa cliente
			lblNome.Text = result.Nome;
			lblTelefone.Text = result.Telefone;
			lblLogradouro.Text = result.Endereco.Logradouro;
			lblCep.Text = result.Endereco.Cep;
			lblBairro.Text = result.Endereco.Bairro;

			if (result.TipoCliente == TipoClienteEnum.Fisica)
				txtDocumento.Text = result.Cpf.ValorFormatado;
			else if (result.TipoCliente == TipoClienteEnum.Juridica)
				txtDocumento.Text = result.Cnpj.ValorFormatado;

			//campos fechar pedido
			lblNomeCliente.Text = result.Nome;
			lblTelefoneCliente.Text = result.Telefone;
			lblLogradouroCliente.Text = result.Endereco.Logradouro;
			lblCepCliente.Text = result.Endereco.Cep;
			lblBairroCliente.Text = result.Endereco.Bairro;
			lblComplementoCliente.Text = result.Endereco.Complemento;
			lblNumeroCliente.Text = result.Endereco.Numero;
		}

		private void HabilitarRadioButtons()
		{
			rbNotaFiscalSim.Enabled = true;
			rbNotaFiscalNao.Enabled = true;
			rbNotaFiscalNao.Checked = true;

			rbFisica.Enabled = true;
			rbJuridica.Enabled = true;

			rbEnderecoNao.Enabled = true;
			rbEnderecoSim.Enabled = true;
			rbEnderecoNao.Checked = true;
		}

		private void BuscaCep()
		{
			try
			{
				Cep cep = txtBuscaCep.Text;
				var viaCepService = ViaCepService.Default();
				var endereco = viaCepService.ObterEndereco( cep );

				if (!string.IsNullOrEmpty( endereco.ToString() ))
				{
					txtLogradouro.Text = endereco.Logradouro;
					txtBairro.Text = endereco.Bairro;
					txtCidade.Text = endereco.Localidade;
					txtComplemento.Text = "";
					txtNumero.Text = "";
				}
			} catch (Exception e) { MessageBox.Show( e.Message ); }
		}

        private void PreencherEndereco()
        {
            _endereco.Logradouro = lblLogradouroCliente.Text;
            _endereco.Bairro = lblBairroCliente.Text;
            _endereco.Cep = lblCepCliente.Text;
            _endereco.Numero = lblNumeroCliente.Text;
            _endereco.Complemento = lblComplementoCliente.Text;
            _endereco.Municipio = "Lages";
        }

        private void LimparCamposSabores()
		{
			cbPrimeiroSabor.Items.Clear();
			cbPrimeiroSabor.Text = String.Empty;
			cbSegundoSabor.Items.Clear();
			cbSegundoSabor.Text = String.Empty;
			cbAdicionais.Items.Clear();
			cbAdicionais.Text = String.Empty;
            listAdicionais.Items.Clear();
            cbAdicionais.Items.Clear();

			chkDoisSabores.Enabled = false;
			chkDoisSabores.Checked = false;
			cbAdicionais.Enabled = false;
			cbSegundoSabor.Enabled = false;

		}

		private void CarregarAdicionais()
		{
			cbAdicionais.Enabled = true;
			cbAdicionais.Items.Clear();

			var adicionais = _servicoProduto.PegarTodosAdicionais();

			foreach (var adicional in adicionais)
				cbAdicionais.Items.Add( adicional );
		}
        
        private void VerificarCpfCnpj()
        {
            if (rbNotaFiscalSim.Checked == true)
            {
                _pedido.EmitirNotaFiscal = true;
                string doc = txtDocumento.Text;
                //verifica se é uma pessoa fisica
                if (doc.Length == 11 || doc.Length == 14 && rbFisica.Checked == true)
                    _pedido.Cliente.Cpf = txtDocumento.Text;
                else
                    _pedido.Cliente.Cnpj = txtDocumento.Text;
            }
            else
                _pedido.EmitirNotaFiscal = false;
        }

        private void DefinirTipoCliente()
        {
            if (rbJuridica.Checked == true)
            {
                _pedido.Departamento = txtDepartamento.Text;
                _pedido.Responsavel = txtResponsavel.Text;
                _pedido.Cliente.TipoCliente = TipoClienteEnum.Juridica;
            }
            else
                _pedido.Cliente.TipoCliente = TipoClienteEnum.Fisica;
        }
        
        private void AdicionaItemAoPedido(ItemPedido itemPedido)
        {
            if (itemPedido != null)
            {
                btnRemoveResponse.Enabled = true;
                _pedido.AdicionarItem(itemPedido);
                _userControlItensPedido.PopularListagemItensPedido(_pedido.ItensPedido);
            }
            else
            {
                MessageBox.Show("Selecione um item para adicionar a lista");
                DialogResult = DialogResult.None;
            }
        }

        private ItemPedido AdicionarBordaEmPizza(ItemPedido itemPedido)
        {
            if (rbCatupiry.Checked)
            {
                Adicional borda = new Adicional("Catupiry");
                itemPedido.InsereAdicional(borda);
            }
            else if (rbCheddar.Checked) 
            {
                Adicional borda = new Adicional("Cheddar");
                itemPedido.InsereAdicional(borda);
            }

            return itemPedido;
        }

        public void AtualizaValorTotalPedido()
        {
            lblValorFinal.Text = _pedido.ValorTotal.ToString();
        }
        
        #region Eventos
        private void buttonPesquisar_Click( object sender, EventArgs e )
		{
			try
			{
				var query = from c in _contexto.Clientes
							where ( c.Telefone.Contains( cmbPesquisarCliente.Text ) || c.Nome.Contains( cmbPesquisarCliente.Text ) )
							select c;

				IList<Cliente> result = query.ToList();

				foreach (var cliente in result)
				{
					cmbPesquisarCliente.Items.Clear();
					cmbPesquisarCliente.Items.Add( cliente );
				}

				cmbPesquisarCliente.DroppedDown = true;

			} catch (Exception) { MessageBox.Show( "Cliente não encontrado" ); }
		}

		private void radioButton1_CheckedChanged( object sender, EventArgs e )
		{
			txtDepartamento.Enabled = false;
			txtResponsavel.Enabled = false;
		}

		private void rbJuridica_CheckedChanged( object sender, EventArgs e )
		{
			txtDepartamento.Enabled = true;
			txtResponsavel.Enabled = true;
		}

		private void cbNotaFiscalNao_CheckedChanged( object sender, EventArgs e )
		{
			txtDocumento.Enabled = false;
		}

		private void rbNotaFiscalSim_CheckedChanged( object sender, EventArgs e )
		{
			txtDocumento.Enabled = true;

			if (_cliente.Cnpj != null)
				txtDocumento.Text = _cliente.Cnpj.ValorFormatado;

			if (_cliente.Cpf != null)
				txtDocumento.Text = _cliente.Cpf.ValorFormatado;
		}

		private void cmbPesquisarCliente_SelectedIndexChanged( object sender, EventArgs e )
		{
			if (cmbPesquisarCliente.SelectedItem != null)
			{
				_cliente = (Cliente) cmbPesquisarCliente.SelectedItem;
				PopularCamposCliente( _cliente );

				if (_cliente.TipoCliente == TipoClienteEnum.Fisica)
					rbFisica.Checked = true;

				if (_cliente.TipoCliente == TipoClienteEnum.Juridica)
				{
					rbJuridica.Checked = true;
					txtDepartamento.Enabled = true;
					txtResponsavel.Enabled = true;
				}

				PopularCamposCliente( _cliente );
				PopularCamposEndereco( _cliente );
				HabilitarRadioButtons();
			}
		}

		private void rbEnderecoSim_CheckedChanged_1( object sender, EventArgs e )
		{
			txtBuscaCep.Enabled = true;

			txtBairro.Enabled = true;
			txtLogradouro.Enabled = true;
			txtCidade.Enabled = true;
			txtComplemento.Enabled = true;
			txtNumero.Enabled = true;
		}

		private void rbEnderecoNao_CheckedChanged_1( object sender, EventArgs e )
		{
			txtBuscaCep.Enabled = false;

			txtBairro.Enabled = false;
			txtLogradouro.Enabled = false;
			txtCidade.Enabled = false;
			txtComplemento.Enabled = false;
			txtNumero.Enabled = false;

			PopularCamposEndereco( _cliente );
		}

		private void txtBuscaCep_TextChanged( object sender, EventArgs e )
		{
			Regex regex = new Regex( @"\d+(?=\-)" );
			if (regex.IsMatch( txtBuscaCep.Text ) && txtBuscaCep.Text.Length == 9)
				BuscaCep();
			if (txtBuscaCep.Text.Length == 8 && !regex.IsMatch( txtBuscaCep.Text ))
				BuscaCep();
		}

		private void chkDoisSabores_CheckedChanged( object sender, EventArgs e )
		{
			if (chkDoisSabores.Checked)
			{
				cbSegundoSabor.Enabled = true;
				if (tamanhoGrande.Checked)
					CarregarSegundoSabor( TamanhoEnum.Grande );
				else if (tamanhoMedio.Checked)
					CarregarSegundoSabor( TamanhoEnum.Media );
				else if (tamanhoPequeno.Checked)
					CarregarSegundoSabor( TamanhoEnum.Pequena );
			} else
			{
				cbSegundoSabor.Enabled = false;
				cbSegundoSabor.Items.Clear();
				cbSegundoSabor.Text = String.Empty;
			}

		}

		private void tamanhoGrande_CheckedChanged( object sender, EventArgs e )
		{
			_tamanho = TamanhoEnum.Grande;
			LimparCamposSabores();
			CarregarSabores( _tamanho );
			cbPrimeiroSabor.Enabled = true;
		}

		private void tamanhoMedio_CheckedChanged( object sender, EventArgs e )
		{
			_tamanho = TamanhoEnum.Media;
			LimparCamposSabores();
			CarregarSabores( _tamanho );
			cbPrimeiroSabor.Enabled = true;
		}

		private void tamanhoPequeno_CheckedChanged( object sender, EventArgs e )
		{
			_tamanho = TamanhoEnum.Pequena;
			LimparCamposSabores();
			CarregarSabores( TamanhoEnum.Pequena );
			cbPrimeiroSabor.Enabled = true;
		}

		private void cbPrimeiroSabor_SelectedIndexChanged( object sender, EventArgs e )
		{
			chkDoisSabores.Enabled = true;
			CarregarSegundoSabor( _tamanho );
			CarregarAdicionais();
		}

		private void btnAddAdicional_Click( object sender, EventArgs e )
		{
			if (!listAdicionais.Items.Contains( cbAdicionais.SelectedItem ))
				listAdicionais.Items.Add( cbAdicionais.SelectedItem );
			else
				MessageBox.Show( "Adicional já inserido" );
		}

		private void cbAdicionais_SelectedIndexChanged( object sender, EventArgs e )
		{
			btnAddAdicional.Enabled = true;
		}

		private void listAdicionais_SelectedIndexChanged( object sender, EventArgs e )
		{
			btnRemoveAdicional.Enabled = true;
		}

		private void btnRemoveAdicional_Click( object sender, EventArgs e )
		{
			listAdicionais.Items.Remove( listAdicionais.SelectedItem );
			btnRemoveAdicional.Enabled = false;
		}

        private void btnGravarPizza_Click(object sender, EventArgs e)
        {
            try
            {
                ItemPedido itemPedido = ObtemPizzaAdicionada();

                AdicionaItemAoPedido(itemPedido);
                AtualizaValorTotalPedido();
                LimparCamposSabores();
                EstadoInicialCadastroPizza();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                DialogResult = DialogResult.None;
            }
        }

        private ItemPedido ObtemPizzaAdicionada()
        {
            Pizza primeiroSabor;
            Pizza segundoSabor;
            ItemPedido itemPedido;

            if (cbSegundoSabor.SelectedItem != null)
            {
                primeiroSabor = (Pizza)cbPrimeiroSabor.SelectedItem;
                segundoSabor = (Pizza)cbSegundoSabor.SelectedItem;
                itemPedido = new ItemPedido(primeiroSabor, segundoSabor);
                itemPedido = AdicionarBordaEmPizza(itemPedido);

                if (listAdicionais.Items.Count != 0)
                    foreach (var item in listAdicionais.Items)
                        AdicionaItemAoPedido(new ItemPedido((Adicional)item));
            }
            else
            {
                primeiroSabor = (Pizza)cbPrimeiroSabor.SelectedItem;
                itemPedido = new ItemPedido(primeiroSabor);
                itemPedido = AdicionarBordaEmPizza(itemPedido);

                if (listAdicionais.Items.Count != 0)
                    foreach (var item in listAdicionais.Items)
                        AdicionaItemAoPedido(new ItemPedido((Adicional)item));
            }

            return itemPedido;
        }

        private void EstadoInicialCadastroPizza()
        {
            tamanhoGrande.Checked = false;
            tamanhoMedio.Checked = false;
            tamanhoPequeno.Checked = false;
            rbCatupiry.Checked = false;
            rbCheddar.Checked = false;
            rbSemBorda.Checked = true;
            btnAddAdicional.Enabled = false;
            btnRemoveAdicional.Enabled = false;
        }

        private void btnGravarCalzone_Click(object sender, EventArgs e)
        {
            try
            {
                Calzone novoCalzone = (Calzone)cbSaborCalzone.SelectedItem;
                ItemPedido itemPedido = new ItemPedido(novoCalzone);

                if (novoCalzone != null)
                {
                    btnRemoveResponse.Enabled = true;
                    _pedido.AdicionarItem(itemPedido);
                    _userControlItensPedido.PopularListagemItensPedido(_pedido.ItensPedido);
                }
                else
                {
                    MessageBox.Show("Selecione um item para adicionar a lista");
                    DialogResult = DialogResult.None;
                }

                cbSaborCalzone.Text = null as string;
                AtualizaValorTotalPedido();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                DialogResult = DialogResult.None;
            }
        }

        private void btnGravarBebida_Click(object sender, EventArgs e)
        {
            try
            {
                Bebida novaBebida = (Bebida)cbBebidas.SelectedItem;
                ItemPedido itemPedido = new ItemPedido(novaBebida);

                if (novaBebida != null)
                {
                    btnRemoveResponse.Enabled = true;
                    _pedido.AdicionarItem(itemPedido);
                    _userControlItensPedido.PopularListagemItensPedido(_pedido.ItensPedido);
                }
                else
                {
                    MessageBox.Show("Selecione um item para adicionar a lista");
                    DialogResult = DialogResult.None;
                }

                cbBebidas.Text = null as string;
                AtualizaValorTotalPedido();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                DialogResult = DialogResult.None;
            }
        }

        private void btnRemoveResponse_Click(object sender, EventArgs e)
        {
            ItemPedido item = _userControlItensPedido.GetItensPedido();

            if (item == null)
                MessageBox.Show("Selecione um item do pedido para remover");

            if (_pedido.ItensPedido.Count < 1)
                btnRemoveResponse.Enabled = false;

            _pedido.RemoverItem(item);
            _userControlItensPedido.PopularListagemItensPedido(_pedido.ItensPedido);
            AtualizaValorTotalPedido();
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            try
            {
                VerificaFormaPagamento();

                _pedido.Cliente = _cliente;
                VerificarCpfCnpj();

                PreencherEndereco();
                _pedido.Endereco = _endereco;

                DefinirTipoCliente();

                _pedido.StatusPedido = StatusPedidoEnum.AguardandoMontagem;

                _pedido.Validar();
            }
            catch (Exception ex)
            {
                DialogResult = DialogResult.None;
                MessageBox.Show(ex.Message);
            }
        }

        private void VerificaFormaPagamento()
        {
            if (rbDinheiro.Checked == true)
                _pedido.FormaPagamento = FormaPagamentoEnum.Dinheiro;
            else if (rbVisa.Checked == true)
                _pedido.FormaPagamento = FormaPagamentoEnum.Visa;
            else
                _pedido.FormaPagamento = FormaPagamentoEnum.Master;
        }
        #endregion

        private void tabControl1_Click(object sender, EventArgs e)
        {
            if (rbEnderecoSim.Checked)
            {
                lblLogradouroCliente.Text = txtLogradouro.Text;
                lblBairroCliente.Text = txtBairro.Text;
                lblNumeroCliente.Text = txtNumero.Text;
                lblCepCliente.Text = txtBuscaCep.Text;
                lblComplementoCliente.Text = txtComplemento.Text;
            }
            else
            {
                lblLogradouroCliente.Text = lblLogradouro.Text;
                lblBairroCliente.Text = lblBairro.Text;
                lblCepCliente.Text = lblCep.Text;
            }
        }
    }
}
