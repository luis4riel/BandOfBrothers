using FluentAssertions;
using FluentValidation.Results;
using Microsoft.AspNet.OData;
using Moq;
using NFe.Aplicacao.Features.Notas_Fiscais;
using NFe.Aplicacao.Features.Notas_Fiscais.Commands;
using NFe.Aplicacao.Features.Notas_Fiscais.Queries;
using NFe.Common.Testes.Features;
using NFe.Controller.Testes.Initializer;
using NFe.Dominio.Features.Notas_Fiscais;
using NFe.WebApi.Controllers.NotasFiscais;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace NFe.Controller.Tests.Features.NotasFiscais
{
    [TestFixture]
    public class NotasFiscaisControllerTests : TestControllerBase
    {
        private NotasFiscaisController _notasfiscaisController;
        private Mock<INotaFiscalServico> _destinatarioServicoMock;

        private Mock<NotaFiscalUpdateCommand> _destinatarioUpdateCmd;
        private Mock<NotaFiscalRegisterCommand> _destinatarioRegisterCmd;
        private Mock<NotaFiscalRemoveCommand> _destinatarioRemoveCmd;
        private Mock<NotaFiscal> _destinatario;
        private Mock<NotaFiscalQuery> _destinatarioQuery;
        private Mock<ValidationResult> _validator;

        [SetUp]
        public void Initialize()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.SetConfiguration(new HttpConfiguration());
            _destinatarioServicoMock = new Mock<INotaFiscalServico>();
            _notasfiscaisController = new NotasFiscaisController(_destinatarioServicoMock.Object)
            {
                Request = request,
            };
            _validator = new Mock<ValidationResult>();
            _destinatarioUpdateCmd = new Mock<NotaFiscalUpdateCommand>();
            _destinatarioUpdateCmd.Setup(cmd => cmd.Validate()).Returns(_validator.Object);
            _destinatarioRegisterCmd = new Mock<NotaFiscalRegisterCommand>();
            _destinatarioRegisterCmd.Setup(cmd => cmd.Validate()).Returns(_validator.Object);
            _destinatarioRemoveCmd = new Mock<NotaFiscalRemoveCommand>();
            _destinatarioRemoveCmd.Setup(cmd => cmd.Validate()).Returns(_validator.Object);
            _destinatario = new Mock<NotaFiscal>();
            _destinatarioQuery = new Mock<NotaFiscalQuery>();

            var isValid = true;
            _validator.Setup(v => v.IsValid).Returns(isValid);
        }

        #region GET

        [Test]
        public void NotasFiscais_Controller_Get_ShouldOk()
        {
            // Arrange
            var notafiscal = ObjectMother.ObterNotaEmitidaValida();
            notafiscal.Id = 1;

            var response = new List<NotaFiscal>() { notafiscal }.AsQueryable();
            _destinatarioServicoMock.Setup(s => s.PegarTodos()).Returns(response);
            var id = 1;
            _destinatarioUpdateCmd.Setup(p => p.Id).Returns(id);
            var odataOptions = GetOdataQueryOptions<NotaFiscal>(_notasfiscaisController);
            // Action
            var callback = _notasfiscaisController.Get(odataOptions);

            //Assert
            _destinatarioServicoMock.Verify(s => s.PegarTodos(), Times.Once);
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<PageResult<NotaFiscalQuery>>>().Subject;
            httpResponse.Content.Should().NotBeNullOrEmpty();
            httpResponse.Content.First().Id.Should().Be(id);
        }

        [Test]
        public void NotasFiscais_Controller_PegarPorId_ShouldBeOk()
        {
            // Arrange
            var id = 1;
            _destinatarioUpdateCmd.Setup(p => p.Id).Returns(id);
            _destinatarioQuery.Setup(p => p.Id).Returns(id);
            _destinatarioServicoMock.Setup(c => c.PegarPorId(id)).Returns(_destinatarioQuery.Object);
            // Action
            IHttpActionResult callback = _notasfiscaisController.GetById(id);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<NotaFiscalQuery>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            httpResponse.Content.Id.Should().Be(id);
            _destinatarioServicoMock.Verify(s => s.PegarPorId(id), Times.Once);
        }

        #endregion

        #region POST

        [Test]
        public void NotasFiscais_Controller_Post_ShouldBeOk()
        {
            // Arrange
            var id = 1;
            _destinatarioServicoMock.Setup(c => c.Adicionar(_destinatarioRegisterCmd.Object)).Returns(id);
            // Action
            IHttpActionResult callback = _notasfiscaisController.Post(_destinatarioRegisterCmd.Object);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<int>>().Subject;
            httpResponse.Content.Should().Be(id);
            _destinatarioServicoMock.Verify(s => s.Adicionar(_destinatarioRegisterCmd.Object), Times.Once);
        }

        [Test]
        public void notasfiscais_Controller_Post_ShouldBeHandleValidationErrors()
        {
            //Arrange
            var isValid = false;
            _validator.Setup(v => v.IsValid).Returns(isValid);
            // Action
            var callback = _notasfiscaisController.Post(_destinatarioRegisterCmd.Object);
            //Assert
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            _destinatarioRegisterCmd.Verify(cmd => cmd.Validate(), Times.Once);
            _destinatarioRegisterCmd.VerifyNoOtherCalls();
        }

        #endregion

        #region PUT

        [Test]
        public void NotasFiscais_Controller_Put_ShouldBeOk()
        {
            // Arrange
            var isUpdated = true;
            _destinatarioServicoMock.Setup(c => c.Atualizar(_destinatarioUpdateCmd.Object)).Returns(isUpdated);
            // Action
            IHttpActionResult callback = _notasfiscaisController.Update(_destinatarioUpdateCmd.Object);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
            _destinatarioServicoMock.Verify(s => s.Atualizar(_destinatarioUpdateCmd.Object), Times.Once);
        }

        [Test]
        public void notasfiscais_Controller_Update_ShouldBeHandleValidationErrors()
        {
            //Arrange
            var isValid = false;
            _validator.Setup(v => v.IsValid).Returns(isValid);
            // Action
            var callback = _notasfiscaisController.Update(_destinatarioUpdateCmd.Object);
            //Assert
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            _destinatarioUpdateCmd.Verify(cmd => cmd.Validate(), Times.Once);
            _destinatarioUpdateCmd.VerifyNoOtherCalls();
        }

        #endregion

        #region DELETE

        [Test]
        public void NotasFiscais_Controller_Delete_ShouldBeOk()
        {
            // Arrange
            var isUpdated = true;
            _destinatarioServicoMock.Setup(c => c.Deletar(_destinatarioRemoveCmd.Object)).Returns(isUpdated);
            // Action
            IHttpActionResult callback = _notasfiscaisController.Delete(_destinatarioRemoveCmd.Object);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            _destinatarioServicoMock.Verify(s => s.Deletar(_destinatarioRemoveCmd.Object), Times.Once);
            httpResponse.Content.Should().BeTrue();
        }

        [Test]
        public void NotasFiscais_Controller_Delete_ShouldBeHandleValidationErrors()
        {
            //Arrange
            var isValid = false;
            _validator.Setup(v => v.IsValid).Returns(isValid);
            // Action
            var callback = _notasfiscaisController.Delete(_destinatarioRemoveCmd.Object);
            //Assert
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            _destinatarioRemoveCmd.Verify(cmd => cmd.Validate(), Times.Once);
            _destinatarioRemoveCmd.VerifyNoOtherCalls();
        }

        #endregion

    }
}
