﻿using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using BancoTabajara.Aplicacao.Funcionalidades.Contas;
using BancoTabajara.Aplicacao.Funcionalidades.Contas.Command;
using BancoTabajara.Common.Tests.Funcionalidades;
using BancoTabajara.Controller.Tests.Inicializador;
using BancoTabajara.Dominio.Excecoes;
using BancoTabajara.Dominio.Funcionalidades.Contas;
using BancoTabajara.Dominio.Funcionalidades.Extrato;
using BancoTabajara.WebApi.Controllers;
using BancoTabajara.WebApi.Excessoes;
using BancoTabajara.WebApi.Models.Contas.ViewModel;
using BancoTabajara.WebApi.Models.Mapeador;
using FluentAssertions;
using Microsoft.AspNet.OData;
using Moq;
using NUnit.Framework;

namespace BancoTabajara.Controller.Tests.Funcionalidades.Contas
{
    [TestFixture]
    public class ContasControllerTests : TestControllerBase
    {
        private ContasController _contasController;
        private Mock<IContaServico> _contaServicoMock;
        private Mock<Conta> _conta;
        private Mock<Extrato> _extrato;
        private Mock<Conta> _contaDestino;
        private CommandRegistrarConta _commandRegistrarConta;
        private CommandAtualizarConta _commandAtualizarConta;
        private CommandDeletarConta _commandDeletarConta;
        private ViewModelConta _viewModelConta;

        [SetUp]
        public void Initialize()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.SetConfiguration(new HttpConfiguration());
            _contaServicoMock = new Mock<IContaServico>();
            _contasController = new ContasController(_contaServicoMock.Object)
            {
                Request = request,
                _contaServico = _contaServicoMock.Object,
            };
            _extrato = new Mock<Extrato>();
            _conta = new Mock<Conta>();
            _contaDestino = new Mock<Conta>();

            _commandRegistrarConta = new CommandRegistrarConta();
            _commandAtualizarConta = new CommandAtualizarConta();
            _commandDeletarConta = new CommandDeletarConta();
            _viewModelConta = new ViewModelConta();


            InicializadorAutoMapper.Reset();
            InicializadorAutoMapper.Inicializador();
        }

        #region GET

        [Test]
        public void Conta_Controller_PegarTodos_Deve_Funcionar()
        {
			// Arrange
			int? limite = null;
			var conta = ObjectMother.ObtemContaValida();
			var response = new List<Conta>() { conta }.AsQueryable();

			_contaServicoMock.Setup( s => s.PegarTodos( limite ) ).Returns( response );
			var odataOptions = GetOdataQueryOptions<Conta>( _contasController );
			// Action
			var result = _contasController.PegarTodos( odataOptions );
			//Assert
			_contaServicoMock.Verify( s => s.PegarTodos( limite ), Times.Once );
			var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<PageResult<ViewModelConta>>>().Subject;
			httpResponse.Content.Should().NotBeNullOrEmpty();
			httpResponse.Content.First().Id.Should().Be( conta.Id );
        }

        [Test]
        public void Conta_Controller_PegarPorId_DeveFuncionar()
        {
            var id = 1;
            _conta.Setup(p => p.Id).Returns(id);
            _contaServicoMock.Setup(c => c.PegarPorId(id)).Returns(_conta.Object);

            IHttpActionResult callback = _contasController.PegarPorId(id);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<ViewModelConta>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            httpResponse.Content.Id.Should().Be(id);
            _contaServicoMock.Verify(s => s.PegarPorId(id), Times.Once);
            _conta.Verify(p => p.Id, Times.Once);
        }

        [Test]
        public void Conta_Controller_GerarExtrato_DeveFuncionar()
        {
            var id = 1;
            _conta.Setup(p => p.Id).Returns(id);
            _contaServicoMock.Setup(c => c.GerarExtrato(id)).Returns(_extrato.Object);

            IHttpActionResult callback = _contasController.GerarExtrato(id);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<Extrato>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            _contaServicoMock.Verify(s => s.GerarExtrato(id), Times.Once);
        }

        [Test]
        public void Conta_Controller_GerarExtrato_id_invalido_deve_falhar()
        {
            _contaServicoMock.Setup(c => c.GerarExtrato(_conta.Object.Id)).Throws<NaoEncontradoException>();

            IHttpActionResult callback = _contasController.GerarExtrato(_conta.Object.Id);

            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<ExceptionPayload>>().Subject;
            httpResponse.Content.ErrorCode.Should().Be((int)CodigoErros.NotFound);

            _contaServicoMock.Verify(s => s.GerarExtrato(_conta.Object.Id), Times.Once);
        }
        #endregion

        #region POST

        [Test]
        public void Conta_Controller_Post_DeveFuncionar()
        {
            int id = 1;
            _contaServicoMock.Setup(c => c.Inserir(_commandRegistrarConta)).Returns(id);

            IHttpActionResult callback = _contasController.Inserir(_commandRegistrarConta);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<int>>().Subject;
            httpResponse.Content.Should().Be(id);
            _contaServicoMock.Verify(c => c.Inserir(_commandRegistrarConta), Times.Once);
        }

        #endregion

        #region PUT

        [Test]
        public void Conta_Controller_Put_DeveFuncionar()
        {
            var atualizado = true;
            _contaServicoMock.Setup(c => c.Atualizar(_commandAtualizarConta)).Returns(atualizado);

            IHttpActionResult callback = _contasController.Atualizar(_commandAtualizarConta);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
            _contaServicoMock.Verify(s => s.Atualizar(_commandAtualizarConta), Times.Once);
        }

        [Test]
        public void Conta_Controller_Put_Id_nao_encontrado_deve_retornar_excecao()
        {

            _contaServicoMock.Setup(c => c.Atualizar(_commandAtualizarConta)).Throws<NaoEncontradoException>();

            IHttpActionResult callback = _contasController.Atualizar(_commandAtualizarConta);

            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<ExceptionPayload>>().Subject;
            httpResponse.Content.ErrorCode.Should().Be((int)CodigoErros.NotFound);

            _contaServicoMock.Verify(s => s.Atualizar(_commandAtualizarConta), Times.Once);
        }

        [Test]
        public void Conta_Controller_Movimentacoes_Deposito_deve_funcionar()
        {
            var atualizado = true;
            var deposito = 100;
            _contaServicoMock.Setup(c => c.Deposito(_conta.Object.Id, deposito)).Returns(atualizado);

            IHttpActionResult callback = _contasController.Deposito(_conta.Object.Id, deposito);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            _contaServicoMock.Verify(s => s.Deposito(_conta.Object.Id, deposito), Times.Once);
            httpResponse.Content.Should().BeTrue();
        }

        [Test]
        public void Conta_Controller_Movimentacoes_Deposito_id_invalido_deve_falhar()
        {
            _contaServicoMock.Setup(c => c.Deposito(_conta.Object.Id, 1000)).Throws<NaoEncontradoException>();

            IHttpActionResult callback = _contasController.Deposito(_conta.Object.Id, 1000);

            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<ExceptionPayload>>().Subject;
            httpResponse.Content.ErrorCode.Should().Be((int)CodigoErros.NotFound);

            _contaServicoMock.Verify(s => s.Deposito(_conta.Object.Id, 1000), Times.Once);
        }

        [Test]
        public void Conta_Controller_Movimentacoes_Saque_deve_funcionar()
        {
            var atualizado = true;
            var saque = 100;
            _contaServicoMock.Setup(c => c.Saque(_conta.Object.Id, saque)).Returns(atualizado);

            IHttpActionResult callback = _contasController.Saque(_conta.Object.Id, saque);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            _contaServicoMock.Verify(s => s.Saque(_conta.Object.Id, saque), Times.Once);
            httpResponse.Content.Should().BeTrue();
        }

        [Test]
        public void Conta_Controller_Movimentacoes_Saque_id_invalido_deve_falhar()
        {
            _contaServicoMock.Setup(c => c.Saque(_conta.Object.Id, 1000)).Throws<NaoEncontradoException>();

            IHttpActionResult callback = _contasController.Saque(_conta.Object.Id, 1000);

            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<ExceptionPayload>>().Subject;
            httpResponse.Content.ErrorCode.Should().Be((int)CodigoErros.NotFound);

            _contaServicoMock.Verify(s => s.Saque(_conta.Object.Id, 1000), Times.Once);
        }

        [Test]
        public void Conta_Controller_Movimentacoes_Transferecia_deve_funcionar()
        {
            var atualizado = true;
            var valor = 100;
            _contaServicoMock.Setup(c => c.Transferencia(_conta.Object.Id, _contaDestino.Object.Id, valor)).Returns(atualizado);

            IHttpActionResult callback = _contasController.Transferencia(_conta.Object.Id, _contaDestino.Object.Id, valor);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            _contaServicoMock.Verify(s => s.Transferencia(_conta.Object.Id, _contaDestino.Object.Id, valor), Times.Once);
            httpResponse.Content.Should().BeTrue();
        }

        [Test]
        public void Conta_Controller_Movimentacoes_Transferencia_id_invalido_deve_falhar()
        {
            _contaServicoMock.Setup(c => c.Transferencia(_conta.Object.Id, _conta.Object.Id, 1000)).Throws<NaoEncontradoException>();

            IHttpActionResult callback = _contasController.Transferencia(_conta.Object.Id, _conta.Object.Id, 1000);

            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<ExceptionPayload>>().Subject;
            httpResponse.Content.ErrorCode.Should().Be((int)CodigoErros.NotFound);

            _contaServicoMock.Verify(s => s.Transferencia(_conta.Object.Id, _conta.Object.Id, 1000), Times.Once);
        }

        #endregion

        #region DELETE

        [Test]
        public void Conta_Controller_Delete_DeveFuncionar()
        {
            var atualizado = true;
            _contaServicoMock.Setup(c => c.Deletar(_commandDeletarConta)).Returns(atualizado);

            IHttpActionResult callback = _contasController.Deletar(_commandDeletarConta);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            _contaServicoMock.Verify(s => s.Deletar(_commandDeletarConta), Times.Once);
            httpResponse.Content.Should().BeTrue();
        }

        #endregion

        #region PATCH
        [Test]
        public void Conta_Controller_Atualizar_Status_DeveFuncionar()
        {
            var atualizado = true;
            _contaServicoMock.Setup(c => c.AtualizarStatus(_conta.Object.Id)).Returns(atualizado);

            IHttpActionResult callback = _contasController.AtualizarStatus(_conta.Object.Id);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            _contaServicoMock.Verify(s => s.AtualizarStatus(_conta.Object.Id), Times.Once);
            httpResponse.Content.Should().BeTrue();
        }
        #endregion


    }
}
