using Microsoft.AspNet.OData.Query;
using NFe.Aplicacao.Features.Notas_Fiscais;
using NFe.Aplicacao.Features.Notas_Fiscais.Commands;
using NFe.Aplicacao.Features.Notas_Fiscais.Queries;
using NFe.Dominio.Features.Notas_Fiscais;
using NFe.WebApi.Controllers.Common;
using NFe.WebApi.Filters;
using System.Web.Http;

namespace NFe.WebApi.Controllers.NotasFiscais
{
    [RoutePrefix("api/notasFiscais")]
    public class NotasFiscaisController : ApiControllerBase
    {
        private readonly INotaFiscalServico _notasFiscaisServico;

        public NotasFiscaisController(INotaFiscalServico notasFiscaisServico) : base()
        {
            _notasFiscaisServico = notasFiscaisServico;
        }

        #region HttpGet
        [HttpGet]
        [ODataQueryOptionsValidate]
        public IHttpActionResult Get(ODataQueryOptions<NotaFiscal> queryOptions)
        {
            var query = _notasFiscaisServico.PegarTodos();
            return HandleQuery<NotaFiscal, NotaFiscalQuery>(query, queryOptions);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            return HandleCallback(() => _notasFiscaisServico.PegarPorId(id));
        }
        #endregion HttpGet

        #region HttpPost
        [HttpPost]
        public IHttpActionResult Post(NotaFiscalRegisterCommand orderCmd)
        {
            var validator = orderCmd.Validate();
            if (!validator.IsValid)
                return HandleValidationFailure(validator.Errors);

            return HandleCallback(() => _notasFiscaisServico.Adicionar(orderCmd));
        }

        #endregion HttpPost

        #region HttpPut
        [HttpPut]
        public IHttpActionResult Update(NotaFiscalUpdateCommand command)
        {
            var validator = command.Validate();

            if (!validator.IsValid)
                return HandleValidationFailure(validator.Errors);

            return HandleCallback(() => _notasFiscaisServico.Atualizar(command));
        }

        #endregion HttpPut

        #region HttpDelete
        [HttpDelete]
        public IHttpActionResult Delete(NotaFiscalRemoveCommand command)
        {
            var validator = command.Validate();

            if (!validator.IsValid)
                return HandleValidationFailure(validator.Errors);

            return HandleCallback(() => _notasFiscaisServico.Deletar(command));
        }

        #endregion HttpDelete
    }
}