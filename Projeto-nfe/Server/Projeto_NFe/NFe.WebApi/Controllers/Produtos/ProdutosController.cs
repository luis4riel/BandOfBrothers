using Microsoft.AspNet.OData.Query;
using NFe.Aplicacao.Features.Produtos;
using NFe.Aplicacao.Features.Produtos.Commands;
using NFe.Aplicacao.Features.Produtos.Queries;
using NFe.Dominio.Features.Produtos;
using NFe.WebApi.Controllers.Common;
using NFe.WebApi.Filters;
using System.Web.Http;

namespace NFe.WebApi.Controllers.Produtos
{
    [RoutePrefix("api/produtos")]
    public class ProdutosController : ApiControllerBase
    {
        private readonly IProdutoServico _produtosServico;

        public ProdutosController(IProdutoServico produtosServico) : base()
        {
            _produtosServico = produtosServico;
        }

        #region HttpGet
        [HttpGet]
        [ODataQueryOptionsValidate]
        public IHttpActionResult Get(ODataQueryOptions<Produto> queryOptions)
        {
            var query = _produtosServico.PegarTodos();
            return HandleQuery<Produto, ProdutoQuery>(query, queryOptions);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            return HandleCallback(() => _produtosServico.PegarPorId(id));
        }
        #endregion HttpGet

        #region HttpPost
        [HttpPost]
        public IHttpActionResult Post(ProdutoRegisterCommand orderCmd)
        {
            var validator = orderCmd.Validate();
            if (!validator.IsValid)
                return HandleValidationFailure(validator.Errors);

            return HandleCallback(() => _produtosServico.Adicionar(orderCmd));
        }

        #endregion HttpPost

        #region HttpPut
        [HttpPut]
        public IHttpActionResult Update(ProdutoUpdateCommand command)
        {
            var validator = command.Validate();

            if (!validator.IsValid)
                return HandleValidationFailure(validator.Errors);

            return HandleCallback(() => _produtosServico.Atualizar(command));
        }

        #endregion HttpPut

        #region HttpDelete
        [HttpDelete]
        public IHttpActionResult Delete(ProdutoRemoveCommand command)
        {
            var validator = command.Validate();

            if (!validator.IsValid)
                return HandleValidationFailure(validator.Errors);

            return HandleCallback(() => _produtosServico.Deletar(command));
        }

        #endregion HttpDelete
    }
}