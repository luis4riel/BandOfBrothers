using FluentAssertions;
using FluentValidation.Results;
using Microsoft.AspNet.OData;
using Moq;
using NFe.Aplicacao.Features.Emitentes;
using NFe.Aplicacao.Features.Emitentes.Commands;
using NFe.Aplicacao.Features.Emitentes.Queries;
using NFe.Common.Testes.Features;
using NFe.Controller.Testes.Initializer;
using NFe.Dominio.Features.Emitentes;
using NFe.WebApi.Controllers.Emitentes;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace NFe.Controller.Tests.Features.Emitentes
{
    [TestFixture]
    public class EmitentesControllerTests : TestControllerBase
    {
        private EmitentesController _emitentesController;
        private Mock<IEmitenteServico> _emitenteServicoMock;

        private Mock<EmitenteUpdateCommand> _emitenteUpdateCmd;
        private Mock<EmitenteRegisterCommand> _emitenteRegisterCmd;
        private Mock<EmitenteRemoveCommand> _emitenteRemoveCmd;
        private Mock<Emitente> _emitente;
        private Mock<EmitenteQuery> _emitenteQuery;
        private Mock<ValidationResult> _validator;

        [SetUp]
        public void Initialize()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.SetConfiguration(new HttpConfiguration());
            _emitenteServicoMock = new Mock<IEmitenteServico>();
            _emitentesController = new EmitentesController(_emitenteServicoMock.Object)
            {
                Request = request,
            };
            _validator = new Mock<ValidationResult>();
            _emitenteUpdateCmd = new Mock<EmitenteUpdateCommand>();
            _emitenteUpdateCmd.Setup(cmd => cmd.Validate()).Returns(_validator.Object);
            _emitenteRegisterCmd = new Mock<EmitenteRegisterCommand>();
            _emitenteRegisterCmd.Setup(cmd => cmd.Validate()).Returns(_validator.Object);
            _emitenteRemoveCmd = new Mock<EmitenteRemoveCommand>();
            _emitenteRemoveCmd.Setup(cmd => cmd.Validate()).Returns(_validator.Object);
            _emitente = new Mock<Emitente>();
            _emitenteQuery = new Mock<EmitenteQuery>();

            var isValid = true;
            _validator.Setup(v => v.IsValid).Returns(isValid);
        }

        #region GET

        [Test]
        public void Emitentes_Controller_Get_ShouldOk()
        {
            // Arrange
            var emitente = ObjectMother.ObtemEmitenteValido();
            var response = new List<Emitente>() { emitente }.AsQueryable();
            _emitenteServicoMock.Setup(s => s.PegarTodos()).Returns(response);
            var id = 1;
            _emitenteUpdateCmd.Setup(p => p.Id).Returns(id);
            var odataOptions = GetOdataQueryOptions<Emitente>(_emitentesController);
            // Action
            var callback = _emitentesController.Get(odataOptions);

            //Assert
            _emitenteServicoMock.Verify(s => s.PegarTodos(), Times.Once);
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<PageResult<EmitenteQuery>>>().Subject;
            httpResponse.Content.Should().NotBeNullOrEmpty();
            httpResponse.Content.First().Id.Should().Be(id);
        }

        [Test]
        public void Emitentes_Controller_PegarPorId_ShouldBeOk()
        {
            // Arrange
            var id = 1;
            _emitenteUpdateCmd.Setup(p => p.Id).Returns(id);
            _emitenteQuery.Setup(p => p.Id).Returns(id);
            _emitenteServicoMock.Setup(c => c.PegarPorId(id)).Returns(_emitenteQuery.Object);
            // Action
            IHttpActionResult callback = _emitentesController.GetById(id);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<EmitenteQuery>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            httpResponse.Content.Id.Should().Be(id);
            _emitenteServicoMock.Verify(s => s.PegarPorId(id), Times.Once);
        }

        #endregion

        #region POST

        [Test]
        public void Emitentes_Controller_Post_ShouldBeOk()
        {
            // Arrange
            var id = 1;
            _emitenteServicoMock.Setup(c => c.Adicionar(_emitenteRegisterCmd.Object)).Returns(id);
            // Action
            IHttpActionResult callback = _emitentesController.Post(_emitenteRegisterCmd.Object);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<int>>().Subject;
            httpResponse.Content.Should().Be(id);
            _emitenteServicoMock.Verify(s => s.Adicionar(_emitenteRegisterCmd.Object), Times.Once);
        }

        [Test]
        public void emitentes_Controller_Post_ShouldBeHandleValidationErrors()
        {
            //Arrange
            var isValid = false;
            _validator.Setup(v => v.IsValid).Returns(isValid);
            // Action
            var callback = _emitentesController.Post(_emitenteRegisterCmd.Object);
            //Assert
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            _emitenteRegisterCmd.Verify(cmd => cmd.Validate(), Times.Once);
            _emitenteRegisterCmd.VerifyNoOtherCalls();
        }

        #endregion

        #region PUT

        [Test]
        public void Emitentes_Controller_Put_ShouldBeOk()
        {
            // Arrange
            var isUpdated = true;
            _emitenteServicoMock.Setup(c => c.Atualizar(_emitenteUpdateCmd.Object)).Returns(isUpdated);
            // Action
            IHttpActionResult callback = _emitentesController.Update(_emitenteUpdateCmd.Object);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
            _emitenteServicoMock.Verify(s => s.Atualizar(_emitenteUpdateCmd.Object), Times.Once);
        }

        [Test]
        public void emitentes_Controller_Update_ShouldBeHandleValidationErrors()
        {
            //Arrange
            var isValid = false;
            _validator.Setup(v => v.IsValid).Returns(isValid);
            // Action
            var callback = _emitentesController.Update(_emitenteUpdateCmd.Object);
            //Assert
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            _emitenteUpdateCmd.Verify(cmd => cmd.Validate(), Times.Once);
            _emitenteUpdateCmd.VerifyNoOtherCalls();
        }

        #endregion

        #region DELETE

        [Test]
        public void Emitentes_Controller_Delete_ShouldBeOk()
        {
            // Arrange
            var isUpdated = true;
            _emitenteServicoMock.Setup(c => c.Deletar(_emitenteRemoveCmd.Object)).Returns(isUpdated);
            // Action
            IHttpActionResult callback = _emitentesController.Delete(_emitenteRemoveCmd.Object);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            _emitenteServicoMock.Verify(s => s.Deletar(_emitenteRemoveCmd.Object), Times.Once);
            httpResponse.Content.Should().BeTrue();
        }

        [Test]
        public void Emitentes_Controller_Delete_ShouldBeHandleValidationErrors()
        {
            //Arrange
            var isValid = false;
            _validator.Setup(v => v.IsValid).Returns(isValid);
            // Action
            var callback = _emitentesController.Delete(_emitenteRemoveCmd.Object);
            //Assert
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            _emitenteRemoveCmd.Verify(cmd => cmd.Validate(), Times.Once);
            _emitenteRemoveCmd.VerifyNoOtherCalls();
        }

        #endregion

    }
}
