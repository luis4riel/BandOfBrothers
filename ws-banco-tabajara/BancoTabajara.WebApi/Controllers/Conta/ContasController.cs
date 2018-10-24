using BancoTabajara.Aplicacao.Funcionalidades.Contas;
using BancoTabajara.Dominio.Funcionalidades.Contas;
using BancoTabajara.Infra.Contexto;
using BancoTabajara.Infra.ORM.Funcionalidade.Clientes;
using BancoTabajara.Infra.ORM.Funcionalidade.Contas;
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

		public ContasController() : base()
		{
            var contexto = new BancoTabajaraDbContext();
            var contaRepositorio = new ContaRepositorio(contexto);
            var clienteRespositorio = new ClienteRepositorio(contexto);
            _contaServico = new ContaServico(contaRepositorio, clienteRespositorio);
		}

		#region HttpGet

		[HttpGet]
		public IHttpActionResult PegarTodos()
		{
			var chaveValor = this.Request.GetQueryNameValuePairs();

			IQueryable<Conta> query = HttpGetAllExtensions<Conta>.GeraQueryParaGetAll( _contaServico, chaveValor );

			return HandleQuery<Conta>( query );
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
		public IHttpActionResult Inserir( Conta conta )
		{
            return HandleCallback( () => _contaServico.Inserir( conta ) );
		}

        #endregion HttpPost

        #region HttpPut
        [HttpPut]
        public IHttpActionResult Atualizar(Conta conta)
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
        public IHttpActionResult Deletar(Conta conta)
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
