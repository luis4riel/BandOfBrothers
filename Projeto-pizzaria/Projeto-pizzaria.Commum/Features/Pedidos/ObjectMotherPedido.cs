using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto_pizzaria.Dominio;
using Projeto_pizzaria.Dominio.Features.Pedidos;
using Projeto_pizzaria.Dominio.Features.Pizzas;

namespace Projeto_pizzaria.Commum.Features
{
	public static partial class ObjectMother
	{
		public static Pedido ObterPedidoValidoPessoaFisica()
		{
			return new Pedido
			{
				Id = 1,
				Cliente = ObjectMother.ObtemClienteValidoFisico(),
				EmitirNotaFiscal = false,
				FormaPagamento = FormaPagamentoEnum.Dinheiro,
				StatusPedido = StatusPedidoEnum.AguardandoMontagem,
				Endereco = ObterEnderecoValido()
			};
		}

		public static Pedido ObterPedidoValidoPessoaJuridicaSemNota()
		{
			return new Pedido
			{
				Id = 1,
				Cliente = ObjectMother.ObtemClienteValidoJuridico(),
				EmitirNotaFiscal = false,
				FormaPagamento = FormaPagamentoEnum.Visa,
				StatusPedido = StatusPedidoEnum.Montagem,
				Endereco = ObterEnderecoValido()
			};
		}

		public static Pedido ObterPedidoValidoPessoaJuridicaComNota()
		{
			return new Pedido
			{
				Id = 1,
				Cliente = ObjectMother.ObtemClienteValidoJuridico(),
				EmitirNotaFiscal = true,
				FormaPagamento = FormaPagamentoEnum.Visa,
				StatusPedido = StatusPedidoEnum.Montagem,
				Departamento = "Desenvolvimento Pizzaria",
				Responsavel = "Thiagão",
				Endereco = ObterEnderecoValido()
			};
		}

		public static Pedido ObterPedidoSemCliente()
		{
			return new Pedido
			{
				Id = 1,
				EmitirNotaFiscal = false,
				FormaPagamento = FormaPagamentoEnum.Visa,
				StatusPedido = StatusPedidoEnum.Montagem,
				Endereco = ObterEnderecoValido()
			};
		}

		public static Pedido ObterPedidoValidoPessoaJuridicaComNotaSemDepartamento()
		{
			return new Pedido
			{
				Id = 1,
				Cliente = ObjectMother.ObtemClienteValidoJuridico(),
				EmitirNotaFiscal = true,
				FormaPagamento = FormaPagamentoEnum.Visa,
				StatusPedido = StatusPedidoEnum.Montagem,
				Departamento = "",
				Responsavel = "Thiagão",
				Endereco = ObterEnderecoValido()
			};
		}

		public static Pedido ObterPedidoValidoPessoaJuridicaComNotaSemResponsavel()
		{
			return new Pedido
			{
				Id = 1,
				Cliente = ObjectMother.ObtemClienteValidoJuridico(),
				EmitirNotaFiscal = true,
				FormaPagamento = FormaPagamentoEnum.Visa,
				StatusPedido = StatusPedidoEnum.Montagem,
				Departamento = "Desenvolvimento Pizzaria",
				Responsavel = "",
				Endereco = ObterEnderecoValido()
			};
		}

		public static Pedido ObterPedidoValidoPessoaJuridicaComNotaECnpjInvalido()
		{
			return new Pedido
			{
				Id = 1,
				Cliente = ObjectMother.ObtemClienteCnpjInvalidoUltimoDigitoECpfVazio(),
				EmitirNotaFiscal = true,
				FormaPagamento = FormaPagamentoEnum.Visa,
				StatusPedido = StatusPedidoEnum.Montagem,
				Departamento = "Desenvolvimento Pizzaria",
				Responsavel = "Thiagão",
				Endereco = ObterEnderecoValido()
			};
		}

		public static Pedido ObterPedidoValidoPessoaJuridicaComNotaECpfInvalido()
		{
			return new Pedido
			{
				Id = 1,
				Cliente = ObjectMother.ObtemClienteCnpjVazioECpfInvalidoUltimoDigito(),
				EmitirNotaFiscal = true,
				FormaPagamento = FormaPagamentoEnum.Visa,
				StatusPedido = StatusPedidoEnum.Montagem,
				Departamento = "Desenvolvimento Pizzaria",
				Responsavel = "Thiagão",
				Endereco = ObterEnderecoValido()
			};
		}

		public static Pedido ObterPedidoValidoPessoaJuridicaComNotaECpfeCnpjInvalido()
		{
			return new Pedido
			{
				Id = 1,
				Cliente = ObjectMother.ObtemClienteCnpjECpfVazio(),
				EmitirNotaFiscal = true,
				FormaPagamento = FormaPagamentoEnum.Visa,
				StatusPedido = StatusPedidoEnum.Montagem,
				Departamento = "Desenvolvimento Pizzaria",
				Responsavel = "Thiagão",
				Endereco = ObterEnderecoValido()
			};
		}
	}
}
