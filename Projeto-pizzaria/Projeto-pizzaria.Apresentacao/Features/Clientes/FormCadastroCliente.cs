using MosaicoSolutions.ViaCep;
using MosaicoSolutions.ViaCep.Modelos;
using Projeto_pizzaria.Dominio.Features.Clientes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_pizzaria.Apresentacao.Features.Clientes
{
    public partial class FormCadastroCliente : Form
    {
        private Cliente cliente;

        public FormCadastroCliente()
        {
            InitializeComponent();
        }

        public Cliente NovaPropriedade
        {
            get
            {
                return cliente;
            }
            set
            {
                cliente = value;

                txtId.Text = cliente.Id.ToString();

                if (cliente.Nome != null)
                {
                    txtNome.Text = cliente.Nome.ToString();
                    txtTelefone.Text = cliente.Telefone.ToString();

                    VerificaCpfCnpj();

                    txtEndereco.Text = cliente.Endereco.Cep.ToString();
                    txtBairro.Text = cliente.Endereco.Bairro;
                    txtCidade.Text = cliente.Endereco.Municipio;
                    txtComplemento.Text = cliente.Endereco.Complemento;
                    txtNumero.Text = cliente.Endereco.Numero;

                    btnCadastrar.Text = "Atualizar";
                }
                else
                {
                    btnCadastrar.Text = "Cadastrar";
                }
            }
        }

        private void checkCpf_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxCpfCnpj.Checked == true)
                txbCpfCnpj.Enabled = true;
            else
                txbCpfCnpj.Enabled = false;
        }

        private void VerificaCpfCnpj()
        {
            if (!string.IsNullOrEmpty(cliente.Cpf.ValorFormatado))
            {
                cbxCpfCnpj.Checked = true;
                txbCpfCnpj.Text = cliente.Cpf.ValorFormatado;
            }
            else if (!string.IsNullOrEmpty(cliente.Cnpj.ValorFormatado))
            {
                cbxCpfCnpj.Checked = true;
                txbCpfCnpj.Text = cliente.Cnpj.ValorFormatado;
            }
        }

        private void BuscaCep()
        {
            try
            {
                Cep cep = txtEndereco.Text;
                var viaCepService = ViaCepService.Default();
                var endereco = viaCepService.ObterEndereco(cep);

                if (!string.IsNullOrEmpty(endereco.ToString()))
                {
                    txtRua.Enabled = true;
                    txtRua.Text = endereco.Logradouro;
                    txtBairro.Enabled = true;
                    txtBairro.Text = endereco.Bairro;
                    txtCidade.Enabled = true;
                    txtCidade.Text = endereco.Localidade;
                }
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }

        private void txtEndereco_TextChanged(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"\d+(?=\-)");
            if (regex.IsMatch(txtEndereco.Text) && txtEndereco.Text.Length == 9)
                BuscaCep();
            if (txtEndereco.Text.Length == 8 && !regex.IsMatch(txtEndereco.Text))
                BuscaCep();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                cliente.Nome = txtNome.Text;
                cliente.Telefone = txtTelefone.Text;

                if (cbxCpfCnpj.Checked == true)
                {
                    if (txbCpfCnpj.Text.Length != 11 && txbCpfCnpj.Text.Length != 14)
                        throw new Exception("O Cpf/Cnpj informado esta incorreto, verifique e tente salvar novamente");
                    else if (txbCpfCnpj.Text.Length == 11)
                    {
                        cliente.Cpf = txbCpfCnpj.Text;
                        cliente.Cnpj = "";
                    }                        
                    else
                    {
                        cliente.Cnpj = txbCpfCnpj.Text;
                        cliente.Cpf = "";
                    }

                    cliente.ValidarCnpjCpf();
                }

                cliente.Endereco.Cep = txtEndereco.Text;
                cliente.Endereco.Municipio = txtCidade.Text;
                cliente.Endereco.Bairro = txtBairro.Text;
                cliente.Endereco.Complemento = txtComplemento.Text;
                cliente.Endereco.Logradouro = txtRua.Text;

                if (string.IsNullOrEmpty(txtNumero.Text))
                    cliente.Endereco.Numero = "s/n";
                else
                    cliente.Endereco.Numero = txtNumero.Text;

                cliente.Validar();
                cliente.Endereco.Validar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                DialogResult = DialogResult.None;
            }
        }
    }
}
