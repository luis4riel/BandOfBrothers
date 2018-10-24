using AutoMapper;
using BancoTabajara.Aplicacao.Funcionalidades.Contas;
using BancoTabajara.Aplicacao.Funcionalidades.Contas.Command;
using BancoTabajara.Dominio.Funcionalidades.Contas;
using BancoTabajara.WebApi.Controllers.Common;
using BancoTabajara.WebApi.Extensions;
using BancoTabajara.WebApi.Models.Contas.ViewModel;
using Microsoft.AspNet.OData.Query;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace BancoTabajara.WebApi.Controllers
{
    [Authorize]
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
		public IHttpActionResult PegarTodos(ODataQueryOptions<Conta> queryOptions)
		{
            var query = _contaServico.PegarTodos();
            return HandleQuery<Conta, ViewModelConta>(query, queryOptions);
        }

		[HttpGet]
		[Route( "{id:int}" )]
		public IHttpActionResult PegarPorId( int id )
		{
            var conta = _contaServico.PegarPorId(id);

			return HandleCallback( () => Mapper.Map<Conta, ViewModelConta>(conta) );
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
		public IHttpActionResult Inserir( CommandRegistrarConta conta )
		{
            return HandleCallback( () => _contaServico.Inserir( conta ) );
		}

        #endregion HttpPost

        #region HttpPut
        [HttpPut]
        public IHttpActionResult Atualizar(CommandAtualizarConta conta)
        {
            return HandleCallback(() => _contaServico.Atualizar(conta));
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
        public IHttpActionResult Deletar(CommandDeletarConta conta)
        {
            return HandleCallback(() => _contaServico.Deletar(conta));
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
