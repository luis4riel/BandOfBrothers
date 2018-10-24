using Microsoft.AspNet.OData.Query;
using NFe.Aplicacao.Features.Destinatarios;
using NFe.Aplicacao.Features.Destinatarios.Commands;
using NFe.Aplicacao.Features.Destinatarios.Queries;
using NFe.Dominio.Features.Destinatarios;
using NFe.WebApi.Controllers.Common;
using NFe.WebApi.Filters;
using System.Web.Http;

namespace NFe.WebApi.Controllers.Destinatarios
{
    [RoutePrefix("api/destinatarios")]
    public class DestinatariosController : ApiControllerBase
    {
        private readonly IDestinatarioServico _destinatariosServico;

        public DestinatariosController(IDestinatarioServico destinatariosServico) : base()
        {
            _destinatariosServico = destinatariosServico;
        }

        #region HttpGet
        [HttpGet]
        [ODataQueryOptionsValidate]
        public IHttpActionResult Get(ODataQueryOptions<Destinatario> queryOptions)
        {
            var query = _destinatariosServico.PegarTodos();
            return HandleQuery<Destinatario, DestinatarioQuery>(query, queryOptions);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            return HandleCallback(() => _destinatariosServico.PegarPorId(id));
        }
        #endregion HttpGet

        #region HttpPost
        [HttpPost]
        public IHttpActionResult Post(DestinatarioRegisterCommand orderCmd)
        {
            var validator = orderCmd.Validate();
            if (!validator.IsValid)
                return HandleValidationFailure(validator.Errors);

            return HandleCallback(() => _destinatariosServico.Adicionar(orderCmd));
        }

        #endregion HttpPost

        #region HttpPut
        [HttpPut]
        public IHttpActionResult Update(DestinatarioUpdateCommand command)
        {
            var validator = command.Validate();

            if (!validator.IsValid)
                return HandleValidationFailure(validator.Errors);

            return HandleCallback(() => _destinatariosServico.Atualizar(command));
        }

        #endregion HttpPut

        #region HttpDelete
        [HttpDelete]
        public IHttpActionResult Delete(DestinatarioRemoveCommand command)
        {
            var validator = command.Validate();

            if (!validator.IsValid)
                return HandleValidationFailure(validator.Errors);

            return HandleCallback(() => _destinatariosServico.Deletar(command));
        }

        #endregion HttpDelete
    }
}