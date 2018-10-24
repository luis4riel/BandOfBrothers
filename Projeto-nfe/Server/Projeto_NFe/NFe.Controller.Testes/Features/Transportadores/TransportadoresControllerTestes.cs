using FluentAssertions;
using FluentValidation.Results;
using Microsoft.AspNet.OData;
using Moq;
using NFe.Aplicacao.Features.Transportadores;
using NFe.Aplicacao.Features.Transportadores.Commands;
using NFe.Aplicacao.Features.Transportadores.Queries;
using NFe.Common.Testes.Features;
using NFe.Controller.Testes.Initializer;
using NFe.Dominio.Features.Transportadores;
using NFe.WebApi.Controllers.Transportadores;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace NFe.Controller.Tests.Features.Transportadores
{
    [TestFixture]
    public class TransportadoresControllerTests : TestControllerBase
    {
        private TransportadoresController _transportadoresController;
        private Mock<ITransportadorServico> _transportadorServicoMock;

        private Mock<TransportadorUpdateCommand> _transportadorUpdateCmd;
        private Mock<TransportadorRegisterCommand> _transportadorRegisterCmd;
        private Mock<TransportadorRemoveCommand> _transportadorRemoveCmd;
        private Mock<Transportador> _transportador;
        private Mock<TransportadorQuery> _transportadorQuery;
        private Mock<ValidationResult> _validator;

        [SetUp]
        public void Initialize()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.SetConfiguration(new HttpConfiguration());
            _transportadorServicoMock = new Mock<ITransportadorServico>();
            _transportadoresController = new TransportadoresController(_transportadorServicoMock.Object)
            {
                Request = request,
            };
            _validator = new Mock<ValidationResult>();
            _transportadorUpdateCmd = new Mock<TransportadorUpdateCommand>();
            _transportadorUpdateCmd.Setup(cmd => cmd.Validate()).Returns(_validator.Object);
            _transportadorRegisterCmd = new Mock<TransportadorRegisterCommand>();
            _transportadorRegisterCmd.Setup(cmd => cmd.Validate()).Returns(_validator.Object);
            _transportadorRemoveCmd = new Mock<TransportadorRemoveCommand>();
            _transportadorRemoveCmd.Setup(cmd => cmd.Validate()).Returns(_validator.Object);
            _transportador = new Mock<Transportador>();
            _transportadorQuery = new Mock<TransportadorQuery>();

            var isValid = true;
            _validator.Setup(v => v.IsValid).Returns(isValid);
        }

        #region GET

        [Test]
        public void Transportadores_Controller_Get_ShouldOk()
        {
            // Arrange
            var transportador = ObjectMother.ObterTransportadorValidoComCnpjENome();
            var response = new List<Transportador>() { transportador }.AsQueryable();
            _transportadorServicoMock.Setup(s => s.PegarTodos()).Returns(response);
            var id = 1;
            _transportadorUpdateCmd.Setup(p => p.Id).Returns(id);
            var odataOptions = GetOdataQueryOptions<Transportador>(_transportadoresController);
            // Action
            var callback = _transportadoresController.Get(odataOptions);

            //Assert
            _transportadorServicoMock.Verify(s => s.PegarTodos(), Times.Once);
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<PageResult<TransportadorQuery>>>().Subject;
            httpResponse.Content.Should().NotBeNullOrEmpty();
            httpResponse.Content.First().Id.Should().Be(id);
        }

        [Test]
        public void Transportadores_Controller_PegarPorId_ShouldBeOk()
        {
            // Arrange
            var id = 1;
            _transportadorUpdateCmd.Setup(p => p.Id).Returns(id);
            _transportadorQuery.Setup(p => p.Id).Returns(id);
            _transportadorServicoMock.Setup(c => c.PegarPorId(id)).Returns(_transportadorQuery.Object);
            // Action
            IHttpActionResult callback = _transportadoresController.GetById(id);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<TransportadorQuery>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            httpResponse.Content.Id.Should().Be(id);
            _transportadorServicoMock.Verify(s => s.PegarPorId(id), Times.Once);
        }

        #endregion

        #region POST

        [Test]
        public void Transportadores_Controller_Post_ShouldBeOk()
        {
            // Arrange
            var id = 1;
            _transportadorServicoMock.Setup(c => c.Adicionar(_transportadorRegisterCmd.Object)).Returns(id);
            // Action
            IHttpActionResult callback = _transportadoresController.Post(_transportadorRegisterCmd.Object);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<int>>().Subject;
            httpResponse.Content.Should().Be(id);
            _transportadorServicoMock.Verify(s => s.Adicionar(_transportadorRegisterCmd.Object), Times.Once);
        }

        [Test]
        public void transportadores_Controller_Post_ShouldBeHandleValidationErrors()
        {
            //Arrange
            var isValid = false;
            _validator.Setup(v => v.IsValid).Returns(isValid);
            // Action
            var callback = _transportadoresController.Post(_transportadorRegisterCmd.Object);
            //Assert
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            _transportadorRegisterCmd.Verify(cmd => cmd.Validate(), Times.Once);
            _transportadorRegisterCmd.VerifyNoOtherCalls();
        }

        #endregion

        #region PUT

        [Test]
        public void Transportadores_Controller_Put_ShouldBeOk()
        {
            // Arrange
            var isUpdated = true;
            _transportadorServicoMock.Setup(c => c.Atualizar(_transportadorUpdateCmd.Object)).Returns(isUpdated);
            // Action
            IHttpActionResult callback = _transportadoresController.Update(_transportadorUpdateCmd.Object);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
            _transportadorServicoMock.Verify(s => s.Atualizar(_transportadorUpdateCmd.Object), Times.Once);
        }

        [Test]
        public void transportadores_Controller_Update_ShouldBeHandleValidationErrors()
        {
            //Arrange
            var isValid = false;
            _validator.Setup(v => v.IsValid).Returns(isValid);
            // Action
            var callback = _transportadoresController.Update(_transportadorUpdateCmd.Object);
            //Assert
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            _transportadorUpdateCmd.Verify(cmd => cmd.Validate(), Times.Once);
            _transportadorUpdateCmd.VerifyNoOtherCalls();
        }

        #endregion

        #region DELETE

        [Test]
        public void Transportadores_Controller_Delete_ShouldBeOk()
        {
            // Arrange
            var isUpdated = true;
            _transportadorServicoMock.Setup(c => c.Deletar(_transportadorRemoveCmd.Object)).Returns(isUpdated);
            // Action
            IHttpActionResult callback = _transportadoresController.Delete(_transportadorRemoveCmd.Object);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            _transportadorServicoMock.Verify(s => s.Deletar(_transportadorRemoveCmd.Object), Times.Once);
            httpResponse.Content.Should().BeTrue();
        }

        [Test]
        public void Transportadores_Controller_Delete_ShouldBeHandleValidationErrors()
        {
            //Arrange
            var isValid = false;
            _validator.Setup(v => v.IsValid).Returns(isValid);
            // Action
            var callback = _transportadoresController.Delete(_transportadorRemoveCmd.Object);
            //Assert
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            _transportadorRemoveCmd.Verify(cmd => cmd.Validate(), Times.Once);
            _transportadorRemoveCmd.VerifyNoOtherCalls();
        }

        #endregion

    }
}
