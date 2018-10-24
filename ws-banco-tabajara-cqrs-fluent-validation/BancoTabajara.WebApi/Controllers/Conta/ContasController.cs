using BancoTabajara.Aplicacao.Funcionalidades.Clientes.Commands;
using BancoTabajara.Aplicacao.Funcionalidades.Contas;
using BancoTabajara.Aplicacao.Funcionalidades.Contas.Commands;
using BancoTabajara.Aplicacao.Funcionalidades.Contas.Queries;
using BancoTabajara.Dominio.Funcionalidades.Contas;
using BancoTabajara.WebApi.Controllers.Common;
using BancoTabajara.WebApi.Extensions;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace BancoTabajara.WebApi.Controllers
{
    [RoutePrefix( "api/contas" )]
	public class ContasController : ApiControllerBase
	{
		public IContaServico _contaServico;

		public ContasController(IContaServico contaServico) : base()
		{
            _contaServico = contaServico;
		}

		#region HttpGet

		[HttpGet]
		public IHttpActionResult PegarTodos()
		{
			var chaveValor = this.Request.GetQueryNameValuePairs();

			IQueryable<ContaQuery> query = HttpGetAllExtensions<Conta>.GeraQueryParaGetAllConta( _contaServico, chaveValor );

			return HandleQuery<ContaQuery>( query );
		}

		[HttpGet]
		[Route( "{id:int}" )]
		public IHttpActionResult PegarPorId( int id )
		{
			return HandleCallback( () => _contaServico.PegarPorId( id ) );
		}

        [HttpGet]
        [Route("extrato/{id:int}")]
        public IHttpActionResult GerarExtrato(int id)
        {
            return HandleCallback(() => _contaServico.GerarExtrato(id));
        }
        #endregion HttpGet

        #region HttpPost
        [HttpPost]
		public IHttpActionResult Inserir(ContaRegistroCommand contaCmd)
		{
            var validador = contaCmd.Validate();
            if (!validador.IsValid)
                return HandleValidationFailure(validador.Errors);

            return HandleCallback(() => _contaServico.Inserir(contaCmd));
		}

        #endregion HttpPost

        #region HttpPut
        [HttpPut]
        public IHttpActionResult Atualizar(ContaAtualizarCommand contaCmd)
        { 
            var validador = contaCmd.Validate();
            if (!validador.IsValid)
                return HandleValidationFailure(validador.Errors);

            return HandleCallback(() => _contaServico.Atualizar(contaCmd));
        }

        [HttpPut]
        [Route("deposito/{id:int}")]
        public IHttpActionResult Deposito(int id, [FromBody] decimal quantia)
        {
            return HandleCallback(() => _contaServico.Deposito(id, quantia));
        }

        [HttpPut]
        [Route("saque/{id:int}")]
        public IHttpActionResult Saque(int id, [FromBody] decimal quantia)
        {
            return HandleCallback(() => _contaServico.Saque(id, quantia));
        }

        [HttpPut]
        [Route("transferencia/{idOrigem:int}/{idDestino:int}")]
        public IHttpActionResult Transferencia(int idOrigem, int idDestino, [FromBody] decimal quantia)
        {
            return HandleCallback(() => _contaServico.Transferencia(idOrigem, idDestino, quantia));
        }

        #endregion HttpPut

        #region HttpDelete
        [HttpDelete]
        public IHttpActionResult Deletar(ContaRemoveCommand contaCmd)
        {
            return HandleCallback(() => _contaServico.Deletar(contaCmd));
        }

        #endregion HttpDelete

        #region HttpPatch
        [HttpPatch]
        [Route("{id:int}")]
        public IHttpActionResult AtualizarStatus(int id)
        {
            return HandleCallback(() => _contaServico.AtualizarStatus(id));
        }
        #endregion
    }
}
