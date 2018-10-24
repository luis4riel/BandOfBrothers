using Projeto_pizzaria.Dominio.Base;
using Projeto_pizzaria.Dominio.Features.Clientes;
using Projeto_pizzaria.Dominio.Features.Enderecos;
using Projeto_pizzaria.Dominio.Features.ItensPedido;
using Projeto_pizzaria.Dominio.Features.Pizzas;
using Projeto_pizzaria.Dominio.Features.Produtos;
using Projeto_pizzaria.Infra.Features.Cnpj;
using Projeto_pizzaria.Infra.Features.Cpf;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Projeto_pizzaria.Dominio.Features.Pedidos
{
	public class Pedido : Entidade
	{
		public Pedido()
		{
			ItensPedido = new List<ItemPedido>();
		}

		public string Departamento { get; set; }
		public string Responsavel { get; set; }
		public int Quantidade { get; set; }
		public bool EmitirNotaFiscal { get; set; }
		public virtual List<ItemPedido> ItensPedido { get; set; }
		public virtual Cliente Cliente { get; set; }
		public StatusPedidoEnum StatusPedido { get; set; }
		public FormaPagamentoEnum FormaPagamento { get; set; }
		public virtual Localizacao Endereco { get; set; }
		public double ValorTotal { get { return ItensPedido.Sum( x => x.ValorParcial ); } }

		public void AdicionarItem( params ItemPedido[] itensPedido )
		{
			foreach (var item in itensPedido)
				ItensPedido.Add( item );

			Quantidade = ItensPedido.Count;
		}

        public void RemoverItem(params ItemPedido[] itensPedido)
        {
            foreach (var item in itensPedido)
                ItensPedido.Remove(item);

            Quantidade = ItensPedido.Count;
        }

        public override void Validar()
        {
            if (Cliente == null)
                throw new PedidoClienteVazioException();
            if (ItensPedido.Count < 1)
                throw new PedidoSemItemException();

            ValidaNotaFiscal();
            ValidaPessoaJuridica();
            Endereco.Validar();
        }

        private void ValidaPessoaJuridica()
        {
            if (Cliente.TipoCliente == TipoClienteEnum.Juridica)
            {
                if (string.IsNullOrEmpty(Departamento))
                    throw new PedidoPessoaJuridicaSemDepartamentoException();
                if (string.IsNullOrEmpty(Responsavel))
                    throw new PedidoPessoaJuridicaSemResponsavelException();
            }
        }

        private void ValidaNotaFiscal()
        {
            if (EmitirNotaFiscal == true)
            {
                if (!string.IsNullOrEmpty(Cliente.Cnpj.ValorFormatado))
                {
                    Cliente.Cnpj = Cliente.Cnpj.ValorFormatado;
                    if (Cliente.Cnpj.EhValido == false)
                        throw new CnpjInvalidoException();
                }
                if (!string.IsNullOrEmpty(Cliente.Cpf.ValorFormatado))
                {
                    Cliente.Cpf = Cliente.Cpf.ValorFormatado;
                    if (Cliente.Cpf.EhValido == false)
                        throw new CpfInvalidoException();
                }
                if (string.IsNullOrEmpty(Cliente.Cnpj.ValorFormatado) && string.IsNullOrEmpty(Cliente.Cpf.ValorFormatado))
                    throw new PedidoCnpjECpfVazioException();
            }
        }

        [ExcludeFromCodeCoverage]
		public static string VerificarStatus( StatusPedidoEnum status )
		{
			switch (status)
			{
				case StatusPedidoEnum.AguardandoMontagem:
					return "Aguardando Montagem";
				case StatusPedidoEnum.Montagem:
					return "Em Montagem";
				case StatusPedidoEnum.AguardandoEntrega:
					return "Aguardando Entrega";
				case StatusPedidoEnum.Entrega:
					return "Em Entrega";
				case StatusPedidoEnum.Entregue:
					return "Entregue";
				default:
					return "Não verificado";
			}
		}

		[ExcludeFromCodeCoverage]
		public override string ToString()
		{
			return "Status Pedido: " + VerificarStatus( StatusPedido )
					+ " - Cliente: " + Cliente.Nome
					+ " - Telefone: " + Cliente.Telefone
					+ " - Quantidade de itens: " + Quantidade;
		}
	}
}