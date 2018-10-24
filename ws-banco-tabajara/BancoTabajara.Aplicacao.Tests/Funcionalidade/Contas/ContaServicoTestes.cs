using BancoTabajara.Aplicacao.Funcionalidades.Contas;
using BancoTabajara.Common.Tests.Funcionalidades;
using BancoTabajara.Dominio.Excecoes;
using BancoTabajara.Dominio.Funcionalidades.Clientes;
using BancoTabajara.Dominio.Funcionalidades.Contas;
using BancoTabajara.Dominio.Funcionalidades.Contas.Exececoes;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BancoTabajara.Aplicacao.Tests.Funcionalidade.Contas
{
	[TestFixture]
	public class ContasServicoTestes
	{
		IContaServico _servico;
		Mock<IContaRepositorio> _repositorio;
		Mock<IClienteRepositorio> _repositorioCliente;

		[SetUp]
		public void SetUp()
		{
			_repositorio = new Mock<IContaRepositorio>();
			_repositorioCliente = new Mock<IClienteRepositorio>();
			_servico = new ContaServico( _repositorio.Object, _repositorioCliente.Object );
		}

		#region inserir
		[Test]
		public void Conta_servico_inserir_deve_funcionar()
		{
			var conta = ObjectMother.ObtemContaValida();
            var cliente = ObjectMother.ClienteValido();
            cliente.Id = 1;
            conta.Titular = cliente;
            _repositorioCliente.Setup(pr => pr.PegarPorId(cliente.Id)).Returns(cliente);
            _repositorio.Setup( pr => pr.Inserir( It.IsAny<Conta>() ) ).Returns( conta );

			var novaContaId = _servico.Inserir( conta );

			_repositorio.Verify( pr => pr.Inserir( It.IsAny<Conta>() ), Times.Once );
			novaContaId.Should().Be( conta.Id );
		}
		#endregion

		#region pegar
		[Test]
		public void Conta_servico_pegarTodos_deve_trazer_todos()
		{
			var limite = 1;
			var conta = ObjectMother.ObtemContaValida();
			var repositoryMockValue = new List<Conta>() { conta }.AsQueryable();
			_repositorio.Setup( pr => pr.PegarTodos( limite ) ).Returns( repositoryMockValue );

			var listaContas = _servico.PegarTodos( limite );

			_repositorio.Verify( pr => pr.PegarTodos( limite ), Times.Once );
			listaContas.Should().NotBeNull();
			listaContas.Count().Should().Be( repositoryMockValue.Count() );

			listaContas.First().Should().Be( repositoryMockValue.First() );
		}

		[Test]
		public void Conta_servico_pegarPorId_deve_trazer_conta()
		{
			var conta = ObjectMother.ObtemContaValida();
			_repositorio.Setup( pr => pr.PegarPorId( conta.Id ) ).Returns( conta );

			var contaSelecionada = _servico.PegarPorId( conta.Id );

			_repositorio.Verify( pr => pr.PegarPorId( conta.Id ), Times.Once );
			contaSelecionada.Should().NotBeNull();
			contaSelecionada.Id.Should().Be( conta.Id );
		}

		[Test]
		public void Conta_servico_pegarPorId_deve_falhar_por_id_nao_encontrado()
		{
			var conta = ObjectMother.ObtemContaValida();
			var exception = new NaoEncontradoException();
			_repositorio.Setup( pr => pr.PegarPorId( conta.Id ) ).Throws( exception );

			Action action = () => _servico.PegarPorId( conta.Id );

			action.Should().Throw<NaoEncontradoException>();

			_repositorio.Verify( pr => pr.PegarPorId( conta.Id ), Times.Once );
		}

        [Test]
        public void Conta_servico_pegarPorId_deve_falhar_por_id_zerado()
        {
            var conta = ObjectMother.ObtemContaValida();
            conta.Id = 0;
            var exception = new NaoEncontradoException();

            Action action = () => _servico.PegarPorId(conta.Id);

            action.Should().Throw<NaoEncontradoException>();
        }

        #endregion

        #region deletar
        [Test]
		public void Conta_servico_deletar_deve_funcionar()
		{
			var conta = ObjectMother.ObtemContaValida();
			_repositorio.Setup( pr => pr.Deletar( conta.Id ) ).Returns( true );

			var contaRemovida = _servico.Deletar( conta );

			_repositorio.Verify( pr => pr.Deletar( conta.Id ), Times.Once );
			contaRemovida.Should().BeTrue();
		}

		[Test]
		public void Conta_servico_deletar_id_nao_encontrado_deve_falhar()
		{
			var conta = ObjectMother.ObtemContaValida();
			_repositorio.Setup( pr => pr.Deletar( conta.Id ) ).Throws<NaoEncontradoException>();

			Action action = () => _servico.Deletar( conta );

			action.Should().Throw<NaoEncontradoException>();

			_repositorio.Verify( pr => pr.Deletar( conta.Id ), Times.Once );
		}

        [Test]
        public void Conta_servico_deletar_id_zerado_deve_falhar()
        {
            var conta = ObjectMother.ObtemContaValida();
            conta.Id = 0;

            Action action = () => _servico.Deletar(conta);

            action.Should().Throw<NaoEncontradoException>();
        }
        #endregion

        #region atualizar
        [Test]
		public void Conta_servico_atualizar_deve_funcionar()
		{
			var conta = ObjectMother.ObtemContaValida();
			var atualizada = true;
			_repositorio.Setup( odr => odr.PegarPorId( conta.Id ) ).Returns( conta );
			_repositorioCliente.Setup( odr => odr.PegarPorId( conta.Titular.Id ) ).Returns( conta.Titular );
			_repositorio.Setup( odr => odr.Atualizar( conta ) ).Returns( atualizada );

			var contaFoiRemovida = _servico.Atualizar( conta );

			_repositorio.Verify( odr => odr.PegarPorId( conta.Id ), Times.Once );
			_repositorio.Verify( odr => odr.Atualizar( conta ), Times.Once );
			contaFoiRemovida.Should().BeTrue();
		}

		[Test]
		public void Conta_servico_atualizar_deve_falhar_por_id_nao_encontrado()
		{
			var conta = ObjectMother.ObtemContaValida();
            conta.Id = 1;
			_repositorio.Setup( pr => pr.PegarPorId( conta.Id ) ).Returns( conta );

			Action action = () => _servico.Atualizar( conta );

			action.Should().Throw<NaoEncontradoException>();

			_repositorio.Verify( pr => pr.Atualizar( It.IsAny<Conta>() ), Times.Never );
		}
        [Test]
        public void Conta_servico_atualizar_status_deve_funcionar()
        {
            var id = 1;
            var conta = ObjectMother.ObtemContaValida();
            _repositorio.Setup(pr => pr.PegarPorId(id)).Returns(conta);
            _repositorio.Setup(pr => pr.Atualizar(conta)).Returns(true);

            var mudarStatus = _servico.AtualizarStatus(id);
            
            _repositorio.Verify(pr => pr.PegarPorId(conta.Id), Times.Once);

            _repositorio.Verify(pr => pr.Atualizar(conta), Times.Once);
            mudarStatus.Should().BeTrue();
        }
        #endregion

        #region Operacoes
        [Test]
        public void Conta_servico_deposito_deve_funcionar()
        {
            var id = 1;
            decimal quantia = 100;
            var conta = ObjectMother.ObtemContaValida();
            _repositorio.Setup(pr => pr.PegarPorId(id)).Returns(conta);
            _repositorio.Setup(pr => pr.Atualizar(conta)).Returns(true);

            var depositoFuncional = _servico.Deposito(id, quantia);

            _repositorio.Verify(pr => pr.PegarPorId(conta.Id), Times.Once);

            _repositorio.Verify(pr => pr.Atualizar(conta), Times.Once);
            depositoFuncional.Should().BeTrue();
        }
        [Test]
        public void Conta_servico_saque_deve_funcionar()
        {
            var id = 1;
            decimal quantia = 100;
            var conta = ObjectMother.ObtemContaValida();
            _repositorio.Setup(pr => pr.PegarPorId(id)).Returns(conta);
            _repositorio.Setup(pr => pr.Atualizar(conta)).Returns(true);

            var saqueFuncional = _servico.Saque(id, quantia);

            _repositorio.Verify(pr => pr.PegarPorId(conta.Id), Times.Once);

            _repositorio.Verify(pr => pr.Atualizar(conta), Times.Once);
            saqueFuncional.Should().BeTrue();
        }
        [Test]
        public void Conta_servico_transferencia_deve_funcionar()
        {
            var id = 1;
            var idDestino = 2;
            decimal quantia = 200;
            var conta = ObjectMother.ObtemContaValida();
            var contaDestino = ObjectMother.ObtemContaValidaParaTransferencia();
            _repositorio.Setup(pr => pr.PegarPorId(id)).Returns(conta);
            _repositorio.Setup(p => p.PegarPorId(idDestino)).Returns(contaDestino);

            _repositorio.Setup(pr => pr.Atualizar(conta)).Returns(true);
            _repositorio.Setup(pr => pr.Atualizar(contaDestino)).Returns(true);

            var transferenciaFuncional = _servico.Transferencia(conta.Id, contaDestino.Id, quantia);

            _repositorio.Verify(pr => pr.PegarPorId(conta.Id), Times.Once);
            _repositorio.Verify(p => p.PegarPorId(contaDestino.Id), Times.Once);

            _repositorio.Verify(pr => pr.Atualizar(conta), Times.Once);
            transferenciaFuncional.Should().BeTrue();
        }
        [Test]
        public void Conta_servico_transferencia_deve_falhar_no_deposito()
        {
            var id = 1;
            var idDestino = 2;
            decimal quantia = 200;
            var conta = ObjectMother.ObtemContaValida();
            var contaDestino = ObjectMother.ObtemContaValidaParaTransferencia();
            _repositorio.Setup(pr => pr.PegarPorId(id)).Returns(conta);
            _repositorio.Setup(p => p.PegarPorId(idDestino)).Returns(contaDestino);

            _repositorio.Setup(pr => pr.Atualizar(conta)).Returns(true);
            _repositorio.Setup(pr => pr.Atualizar(contaDestino)).Returns(false);

            var transferenciaFuncional = _servico.Transferencia(conta.Id, contaDestino.Id, quantia);

            _repositorio.Verify(pr => pr.PegarPorId(conta.Id), Times.Once);
            _repositorio.Verify(p => p.PegarPorId(contaDestino.Id), Times.Once);

            _repositorio.Verify(pr => pr.Atualizar(conta), Times.Once);
            transferenciaFuncional.Should().BeFalse();
        }
        [Test]
        public void Conta_servico_transferencia_deve_falhar_no_saque()
        {
            var id = 1;
            var idDestino = 2;
            decimal quantia = 200;
            var conta = ObjectMother.ObtemContaValida();
            var contaDestino = ObjectMother.ObtemContaValidaParaTransferencia();
            _repositorio.Setup(pr => pr.PegarPorId(id)).Returns(conta);
            _repositorio.Setup(p => p.PegarPorId(idDestino)).Returns(contaDestino);

            _repositorio.Setup(pr => pr.Atualizar(conta)).Returns(false);
            _repositorio.Setup(pr => pr.Atualizar(contaDestino)).Returns(true);

            var transferenciaFuncional = _servico.Transferencia(conta.Id, contaDestino.Id, quantia);

            _repositorio.Verify(pr => pr.PegarPorId(conta.Id), Times.Once);
            _repositorio.Verify(p => p.PegarPorId(contaDestino.Id), Times.Once);

            _repositorio.Verify(pr => pr.Atualizar(conta), Times.Once);
            transferenciaFuncional.Should().BeFalse();
        }

        [Test]
        public void Conta_servico_deposito_id_inexistente_deve_falhar()
        {
            var idInvalido = 990;

            Action action = () => _servico.Deposito(idInvalido, 500);

            action.Should().Throw<OperacaoFalhouException>();
        }

        [Test]
        public void Conta_servico_deposito_id_izerado_deve_falhar()
        {
            var idInvalido = 0;

            Action action = () => _servico.Deposito(idInvalido, 500);

            action.Should().Throw<OperacaoFalhouException>();
        }

        [Test]
        public void Conta_servico_saque_id_inexistente_deve_falhar()
        {
            var idInvalido = 995;

            Action action = () => _servico.Saque(idInvalido, 500);

            action.Should().Throw<OperacaoFalhouException>();
        }

        [Test]
        public void Conta_servico_saque_id_zerado_deve_falhar()
        {
            var idInvalido = 0;

            Action action = () => _servico.Saque(idInvalido, 500);

            action.Should().Throw<OperacaoFalhouException>();
        }

        [Test]
        public void Conta_servico_transferencia_id_inexistente_deve_falhar()
        {
            var idInvalido = 995;

            Action action = () => _servico.Transferencia(idInvalido, 6, 500);

            action.Should().Throw<OperacaoFalhouException>();
        }

        [Test]
        public void Conta_servico_transferencia_id_zerado_deve_falhar()
        {
            var idInvalido = 0;

            Action action = () => _servico.Transferencia(idInvalido, 6, 500);

            action.Should().Throw<OperacaoFalhouException>();
        }

        [Test]
        public void Conta_servico_atualizarStatus_id_inexistente_deve_falhar()
        {
            var idInvalido = 990;

            Action action = () => _servico.AtualizarStatus(idInvalido);
            
            action.Should().Throw<AtualizarStatusException>();
        }

        [Test]
        public void Conta_servico_atualizarStatus_id_zerado_deve_falhar()
        {
            var idInvalido = 0;

            Action action = () => _servico.AtualizarStatus(idInvalido);

            action.Should().Throw<AtualizarStatusException>();
        }

        [Test]
        public void Conta_servico_gerarExtrato_deve_funcionar()
        {
            var conta = ObjectMother.ObtemContaValida();
            var titular = ObjectMother.ClienteValido();
            titular.Id = 1;
            conta.Id = 1;
            conta.Titular = titular;

            _repositorio.Setup(pr => pr.PegarPorId(conta.Id)).Returns(conta);

            var extrato = _servico.GerarExtrato(conta.Id);

            _repositorio.Verify(pr => pr.PegarPorId(conta.Id), Times.Once);
            extrato.Should().NotBeNull();
        }

        [Test]
        public void Conta_servico_gerarExtrato_id_nao_encontrado_deve_falhar()
        {
            var conta = ObjectMother.ObtemContaValida();

            Action action = () => _servico.GerarExtrato(conta.Id);

            action.Should().Throw<NaoEncontradoException>();
        }

        [Test]
        public void Conta_servico_gerarExtrato_id_zerado_deve_falhar()
        {
            var conta = ObjectMother.ObtemContaValida();

            conta.Id = 0;

            Action action = () => _servico.GerarExtrato(conta.Id);

            action.Should().Throw<NaoEncontradoException>();
        }
        #endregion
    }
}