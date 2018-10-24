using Microsoft.AspNet.OData.Query;
using NFe.Aplicacao.Features.Transportadores;
using NFe.Aplicacao.Features.Transportadores.Commands;
using NFe.Aplicacao.Features.Transportadores.Queries;
using NFe.Dominio.Features.Transportadores;
using NFe.WebApi.Controllers.Common;
using NFe.WebApi.Filters;
using System.Web.Http;

namespace NFe.WebApi.Controllers.Transportadores
{
    [RoutePrefix("api/transportadores")]
    public class TransportadoresController : ApiControllerBase
    {
        private readonly ITransportadorServico _transportadoresServico;

        public TransportadoresController(ITransportadorServico transportadoresServico) : base()
        {
            _transportadoresServico = transportadoresServico;
        }

        #region HttpGet
        [HttpGet]
        [ODataQueryOptionsValidate]
        public IHttpActionResult Get(ODataQueryOptions<Transportador> queryOptions)
        {
            var query = _transportadoresServico.PegarTodos();
            return HandleQuery<Transportador, TransportadorQuery>(query, queryOptions);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            return HandleCallback(() => _transportadoresServico.PegarPorId(id));
        }
        #endregion HttpGet

        #region HttpPost
        [HttpPost]
        public IHttpActionResult Post(TransportadorRegisterCommand orderCmd)
        {
            var validator = orderCmd.Validate();
            if (!validator.IsValid)
                return HandleValidationFailure(validator.Errors);

            return HandleCallback(() => _transportadoresServico.Adicionar(orderCmd));
        }

        #endregion HttpPost

        #region HttpPut
        [HttpPut]
        public IHttpActionResult Update(TransportadorUpdateCommand command)
        {
            var validator = command.Validate();

            if (!validator.IsValid)
                return HandleValidationFailure(validator.Errors);

            return HandleCallback(() => _transportadoresServico.Atualizar(command));
        }

        #endregion HttpPut

        #region HttpDelete
        [HttpDelete]
        public IHttpActionResult Delete(TransportadorRemoveCommand command)
        {
            var validator = command.Validate();

            if (!validator.IsValid)
                return HandleValidationFailure(validator.Errors);

            return HandleCallback(() => _transportadoresServico.Deletar(command));
        }

        #endregion HttpDelete
    }
}