using Microsoft.AspNet.OData.Query;
using NFe.Aplicacao.Features.Emitentes;
using NFe.Aplicacao.Features.Emitentes.Commands;
using NFe.Aplicacao.Features.Emitentes.Queries;
using NFe.Dominio.Features.Emitentes;
using NFe.WebApi.Controllers.Common;
using NFe.WebApi.Filters;
using System.Web.Http;

namespace NFe.WebApi.Controllers.Emitentes
{
    [RoutePrefix("api/emitentes")]
    public class EmitentesController : ApiControllerBase
    {
        private readonly IEmitenteServico _emitentesServico;

        public EmitentesController(IEmitenteServico emitentesServico) : base()
        {
            _emitentesServico = emitentesServico;
        }

        #region HttpGet
        [HttpGet]
        [ODataQueryOptionsValidate]
        public IHttpActionResult Get(ODataQueryOptions<Emitente> queryOptions)
        {
            var query = _emitentesServico.PegarTodos();
            return HandleQuery<Emitente, EmitenteQuery>(query, queryOptions);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            return HandleCallback(() => _emitentesServico.PegarPorId(id));
        }
        #endregion HttpGet

        #region HttpPost
        [HttpPost]
        public IHttpActionResult Post(EmitenteRegisterCommand orderCmd)
        {
            var validator = orderCmd.Validate();
            if (!validator.IsValid)
                return HandleValidationFailure(validator.Errors);

            return HandleCallback(() => _emitentesServico.Adicionar(orderCmd));
        }

        #endregion HttpPost

        #region HttpPut
        [HttpPut]
        public IHttpActionResult Update(EmitenteUpdateCommand command)
        {
            var validator = command.Validate();

            if (!validator.IsValid)
                return HandleValidationFailure(validator.Errors);

            return HandleCallback(() => _emitentesServico.Atualizar(command));
        }

        #endregion HttpPut

        #region HttpDelete
        [HttpDelete]
        public IHttpActionResult Delete(EmitenteRemoveCommand command)
        {
            var validator = command.Validate();

            if (!validator.IsValid)
                return HandleValidationFailure(validator.Errors);

            return HandleCallback(() => _emitentesServico.Deletar(command));
        }

        #endregion HttpDelete
    }
}