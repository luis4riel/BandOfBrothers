using FluentAssertions;
using FluentValidation.Results;
using Microsoft.AspNet.OData;
using Moq;
using NFe.Aplicacao.Features.Produtos;
using NFe.Aplicacao.Features.Produtos.Commands;
using NFe.Aplicacao.Features.Produtos.Queries;
using NFe.Common.Testes.Features;
using NFe.Controller.Testes.Initializer;
using NFe.Dominio.Features.Produtos;
using NFe.WebApi.Controllers.Produtos;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace NFe.Controller.Tests.Features.Produtos
{
    [TestFixture]
    public class ProdutosControllerTests : TestControllerBase
    {
        private ProdutosController _produtosController;
        private Mock<IProdutoServico> _produtoServicoMock;

        private Mock<ProdutoUpdateCommand> _produtoUpdateCmd;
        private Mock<ProdutoRegisterCommand> _produtoRegisterCmd;
        private Mock<ProdutoRemoveCommand> _produtoRemoveCmd;
        private Mock<Produto> _produto;
        private Mock<ProdutoQuery> _produtoQuery;
        private Mock<ValidationResult> _validator;

        [SetUp]
        public void Initialize()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.SetConfiguration(new HttpConfiguration());
            _produtoServicoMock = new Mock<IProdutoServico>();
            _produtosController = new ProdutosController(_produtoServicoMock.Object)
            {
                Request = request,
            };
            _validator = new Mock<ValidationResult>();
            _produtoUpdateCmd = new Mock<ProdutoUpdateCommand>();
            _produtoUpdateCmd.Setup(cmd => cmd.Validate()).Returns(_validator.Object);
            _produtoRegisterCmd = new Mock<ProdutoRegisterCommand>();
            _produtoRegisterCmd.Setup(cmd => cmd.Validate()).Returns(_validator.Object);
            _produtoRemoveCmd = new Mock<ProdutoRemoveCommand>();
            _produtoRemoveCmd.Setup(cmd => cmd.Validate()).Returns(_validator.Object);
            _produto = new Mock<Produto>();
            _produtoQuery = new Mock<ProdutoQuery>();

            var isValid = true;
            _validator.Setup(v => v.IsValid).Returns(isValid);
        }

        #region GET

        [Test]
        public void Produtos_Controller_Get_ShouldOk()
        {
            // Arrange
            var produto = ObjectMother.ObtemProdutoValido();
            var response = new List<Produto>() { produto }.AsQueryable();
            _produtoServicoMock.Setup(s => s.PegarTodos()).Returns(response);
            var id = 1;
            _produtoUpdateCmd.Setup(p => p.Id).Returns(id);
            var odataOptions = GetOdataQueryOptions<Produto>(_produtosController);
            // Action
            var callback = _produtosController.Get(odataOptions);

            //Assert
            _produtoServicoMock.Verify(s => s.PegarTodos(), Times.Once);
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<PageResult<ProdutoQuery>>>().Subject;
            httpResponse.Content.Should().NotBeNullOrEmpty();
            httpResponse.Content.First().Id.Should().Be(id);
        }

        [Test]
        public void Produtos_Controller_PegarPorId_ShouldBeOk()
        {
            // Arrange
            var id = 1;
            _produtoUpdateCmd.Setup(p => p.Id).Returns(id);
            _produtoQuery.Setup(p => p.Id).Returns(id);
            _produtoServicoMock.Setup(c => c.PegarPorId(id)).Returns(_produtoQuery.Object);
            // Action
            IHttpActionResult callback = _produtosController.GetById(id);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<ProdutoQuery>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            httpResponse.Content.Id.Should().Be(id);
            _produtoServicoMock.Verify(s => s.PegarPorId(id), Times.Once);
        }

        #endregion

        #region POST

        [Test]
        public void Produtos_Controller_Post_ShouldBeOk()
        {
            // Arrange
            var id = 1;
            _produtoServicoMock.Setup(c => c.Adicionar(_produtoRegisterCmd.Object)).Returns(id);
            // Action
            IHttpActionResult callback = _produtosController.Post(_produtoRegisterCmd.Object);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<int>>().Subject;
            httpResponse.Content.Should().Be(id);
            _produtoServicoMock.Verify(s => s.Adicionar(_produtoRegisterCmd.Object), Times.Once);
        }

        [Test]
        public void produtos_Controller_Post_ShouldBeHandleValidationErrors()
        {
            //Arrange
            var isValid = false;
            _validator.Setup(v => v.IsValid).Returns(isValid);
            // Action
            var callback = _produtosController.Post(_produtoRegisterCmd.Object);
            //Assert
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            _produtoRegisterCmd.Verify(cmd => cmd.Validate(), Times.Once);
            _produtoRegisterCmd.VerifyNoOtherCalls();
        }

        #endregion

        #region PUT

        [Test]
        public void Produtos_Controller_Put_ShouldBeOk()
        {
            // Arrange
            var isUpdated = true;
            _produtoServicoMock.Setup(c => c.Atualizar(_produtoUpdateCmd.Object)).Returns(isUpdated);
            // Action
            IHttpActionResult callback = _produtosController.Update(_produtoUpdateCmd.Object);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
            _produtoServicoMock.Verify(s => s.Atualizar(_produtoUpdateCmd.Object), Times.Once);
        }

        [Test]
        public void produtos_Controller_Update_ShouldBeHandleValidationErrors()
        {
            //Arrange
            var isValid = false;
            _validator.Setup(v => v.IsValid).Returns(isValid);
            // Action
            var callback = _produtosController.Update(_produtoUpdateCmd.Object);
            //Assert
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            _produtoUpdateCmd.Verify(cmd => cmd.Validate(), Times.Once);
            _produtoUpdateCmd.VerifyNoOtherCalls();
        }

        #endregion

        #region DELETE

        [Test]
        public void Produtos_Controller_Delete_ShouldBeOk()
        {
            // Arrange
            var isUpdated = true;
            _produtoServicoMock.Setup(c => c.Deletar(_produtoRemoveCmd.Object)).Returns(isUpdated);
            // Action
            IHttpActionResult callback = _produtosController.Delete(_produtoRemoveCmd.Object);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            _produtoServicoMock.Verify(s => s.Deletar(_produtoRemoveCmd.Object), Times.Once);
            httpResponse.Content.Should().BeTrue();
        }

        [Test]
        public void Produtos_Controller_Delete_ShouldBeHandleValidationErrors()
        {
            //Arrange
            var isValid = false;
            _validator.Setup(v => v.IsValid).Returns(isValid);
            // Action
            var callback = _produtosController.Delete(_produtoRemoveCmd.Object);
            //Assert
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            _produtoRemoveCmd.Verify(cmd => cmd.Validate(), Times.Once);
            _produtoRemoveCmd.VerifyNoOtherCalls();
        }

        #endregion

    }
}
