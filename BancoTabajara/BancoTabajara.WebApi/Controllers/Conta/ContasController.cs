using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BancoTabajara.Aplicacao.Funcionalidades.Contas;
using BancoTabajara.Dominio.Funcionalidades.Contas;
using BancoTabajara.Infra.Contexto;
using BancoTabajara.Infra.ORM.Funcionalidade.Clientes;
using BancoTabajara.Infra.ORM.Funcionalidade.Contas;
using BancoTabajara.WebApi.Controllers.Common;
using BancoTabajara.WebApi.Extensions;

namespace BancoTabajara.WebApi.Controllers
{
	[RoutePrefix( "api/contas" )]
	public class ContasController : ApiControllerBase
	{
		public IContaServico _contaServico;

		public ContasController() : base()
		{
			var contexto = new BancoTabajaraDbContext();
			var repositorio = new ContaRepositorio( contexto );
			var repositorioCliente = new ClienteRepositorio( contexto );

			_contaServico = new ContaServico( repositorio, repositorioCliente );
		}

		#region HttpGet

		[HttpGet]
		public IHttpActionResult Get()
		{
			var chaveValor = this.Request.GetQueryNameValuePairs();

			IQueryable<Conta> query = HttpGetAllExtensions<Conta>.GeraQueryParaGetAll( _contaServico, chaveValor );

			return HandleQuery<Conta>( query );
		}

		[HttpGet]
		[Route( "{id:int}" )]
		public IHttpActionResult GetById( int id )
		{
			return HandleCallback( () => _contaServico.PegarPorId( id ) );
		}
		#endregion HttpGet

		#region HttpPost
		[HttpPost]
		public IHttpActionResult Post( Conta conta )
		{
			Conta _conta = new Conta(conta.Titular);

			return HandleCallback( () => _contaServico.Inserir( _conta ) );
		}

		#endregion HttpPost

		#region HttpPut
		[HttpPut]
		public IHttpActionResult Update( Conta conta )
		{
			return HandleCallback( () => _contaServico.Atualizar( conta ) );
		}

		#endregion HttpPut

		#region HttpDelete
		[HttpDelete]
		public IHttpActionResult Delete( Conta conta )
		{
			return HandleCallback( () => _contaServico.Deletar( conta ) );
		}

        #endregion HttpDelete
        #region HttpPatch
        [HttpPatch]
        [Route("{id:int}")]
        public IHttpActionResult AtualizarStatus(int id)
        {
            var conta = _contaServico.PegarPorId(id);
            return HandleCallback(() => _contaServico.AtualizarStatus(conta));
        }
        #endregion
    }
}
