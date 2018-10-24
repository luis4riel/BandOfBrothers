using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BancoTabajara.Common.Tests.Funcionalidades;
using BancoTabajara.Dominio.Funcionalidades.Contas;
using BancoTabajara.Dominio.Funcionalidades.Contas.Exececoes;
using BancoTabajara.Dominio.Funcionalidades.Extrato;
using BancoTabajara.Dominio.Funcionalidades.Movimentacoes;
using FluentAssertions;
using NUnit.Framework;

namespace BancoTabajara.Dominio.Tests.Funcionalidades.Contas
{
	public class ContaDominioTestes
	{
		private Conta _conta;

		[Test]
		public void Conta_dominio_validar_deve_passar()
		{
			_conta = ObjectMother.ObtemContaValida();

			Action action = () => _conta.Validar();
			action.Should().NotThrow<Exception>();
		}
        
        [Test]
        public void Conta_Dominio_Validar_titular_DeveFalhar()
        {
            Conta conta = ObjectMother.ContaInvalidaSemCliente();

            Action action = () => conta.Validar();

            action.Should().Throw<ContaTitularVazioException>();
        }

        #region Transações bancarias
        [Test]
        public void Conta_dominio_Realizar_Saque_deve_passar()
        {
            decimal ValorSaldoEsperado = 1000;
            _conta = ObjectMother.ObtemContaValida();
            _conta.Deposita(1500, TipoMovimentacao.Credito);
            _conta.Saca(500, TipoMovimentacao.Debito );
			_conta.Saldo.Should().Be(ValorSaldoEsperado);
        }
        [Test]
        public void Conta_dominio_Realizar_Saque_deve_falhar()
        {
            _conta = ObjectMother.ObtemContaValida();
            _conta.Deposita(100, TipoMovimentacao.Credito );

			var resultado = _conta.Saca(1500, TipoMovimentacao.Debito );

			resultado.Should().BeFalse();
        }

        [Test]
        public void Conta_dominio_Realizar_Deposito_deve_passar()
        {
            decimal ValorSaldoEsperado = 2000;
            _conta = ObjectMother.ObtemContaValida();
            _conta.Deposita(1500, TipoMovimentacao.Credito );
			_conta.Deposita(500, TipoMovimentacao.Credito );
			_conta.Saldo.Should().Be(ValorSaldoEsperado);
        }
        
        [Test]
        public void Conta_dominio_Realizar_Transferencia_deve_passar()
        {
            Conta contaTransferida = ObjectMother.ObtemContaValidaParaTransferencia();
            contaTransferida.Deposita(1000, TipoMovimentacao.Credito );

			decimal ValorSaldoEsperadoAposTransferencia = 1000;
            decimal ValorEsperadoSaldoConta2 = 1500;

            _conta = ObjectMother.ObtemContaValida();
            _conta.Deposita(1500, TipoMovimentacao.Credito );

			_conta.Transfere(contaTransferida, 500);

            _conta.Saldo.Should().Be(ValorSaldoEsperadoAposTransferencia);
            contaTransferida.Saldo.Should().Be(ValorEsperadoSaldoConta2);
        }

        [Test]
        public void Conta_dominio_Realizar_Transferencia_deve_falhar()
        {
            Conta contaDestino = ObjectMother.ObtemContaValidaParaTransferencia();
            contaDestino.Deposita(1000, TipoMovimentacao.Credito );
			_conta = ObjectMother.ObtemContaValida();

            var resultado = _conta.Transfere(contaDestino, 2000);

            resultado.Should().BeFalse();
        }

        [Test]
        public void Conta_Dominio_Deve_Somar_Saldo_e_Limite()
        {
            var valorEsperado = 600;
            Conta novaConta = ObjectMother.ObtemContaValida();
            novaConta.Deposita(100, TipoMovimentacao.Credito );

			var result = novaConta.SaldoTotal;

            result.Should().Be(valorEsperado);
        }

        [Test]
        public void Conta_dominio_GerarExtrato_deve_passar()
        {
            var valorEsperado = 0;
            _conta = ObjectMother.ObtemContaValida();

            var result = Extrato.GerarExtrato(_conta);

            result.Should().NotBeNull();
            result.Limite.Should().BeGreaterThan(valorEsperado);

        }
        #endregion

        #region Validações conta inativa
        [Test]
        public void Conta_dominio_Validar_Saque_Conta_Inativa_deve_falhar()
        {
            _conta = ObjectMother.ContaInvalida();

            Action action = () => _conta.Saca(500, TipoMovimentacao.Debito );

            action.Should().Throw<ContaInativaException>();
        }


        [Test]
        public void Conta_dominio_Validar_Deposito_Conta_Inativa_deve_falhar()
        {
            _conta = ObjectMother.ContaInvalida();

            Action action = () => _conta.Deposita(500, TipoMovimentacao.Credito );

			action.Should().Throw<ContaInativaException>();
        }

        [Test]
        public void Conta_dominio_Validar_Transferencia_Conta_Inativa_deve_falhar()
        {
            _conta = ObjectMother.ContaInvalida();
            var _contaDestino = ObjectMother.ObtemContaValida();

            Action action = () => _conta.Transfere(_contaDestino, 500);

            action.Should().Throw<ContaInativaException>();
        }
        #endregion

        #region Mudar Status
        [Test]
        public void Conta_dominio_mudar_status_para_conta_desativada_deve_funcionar()
        {
            var esperado = false;
            var conta = ObjectMother.ObtemContaValida();

            conta.MudarStatus();

            conta.Estado.Should().Be(esperado);
        }
        [Test]
        public void Conta_dominio_mudar_status_para_conta_ativada_deve_funcionar()
        {
            var esperado = true;
            var conta = ObjectMother.ContaInvalida();

            conta.MudarStatus();

            conta.Estado.Should().Be(esperado);
        }
        #endregion

    }
}
