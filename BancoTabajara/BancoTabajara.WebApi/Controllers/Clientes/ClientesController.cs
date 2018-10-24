using BancoTabajara.Aplicacao;
using BancoTabajara.Aplicacao.Funcionalidades.Clientes;
using BancoTabajara.Dominio.Funcionalidades.Clientes;
using BancoTabajara.Infra.Contexto;
using BancoTabajara.Infra.ORM.Funcionalidade.Clientes;
using BancoTabajara.WebApi.Controllers.Common;
using BancoTabajara.WebApi.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace BancoTabajara.WebApi.Controllers
{
    [RoutePrefix("api/clientes")]
    public class ClientesController : ApiControllerBase
    {
        public IClienteServico _clienteServico;

        public ClientesController() : base()
        {
            var contexto = new BancoTabajaraDbContext();
            var repositorio = new ClienteRepositorio(contexto);
            _clienteServico = new ClienteServico(repositorio);
        }

        #region HttpGet
		 
       [HttpGet]
        public IHttpActionResult Get()
        {
			var chaveValor = this.Request.GetQueryNameValuePairs();

			IQueryable<Cliente> query = HttpGetAllExtensions<Cliente>.GeraQueryParaGetAll( _clienteServico, chaveValor );

			return HandleQuery<Cliente>( query );
		}

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            return HandleCallback(() => _clienteServico.PegarPorId(id));
        }
        #endregion HttpGet

        #region HttpPost
        [HttpPost]
        public IHttpActionResult Post(Cliente cliente)
        {
            return HandleCallback(() => _clienteServico.Inserir(cliente));
        }

        #endregion HttpPost

        #region HttpPut
        [HttpPut]
        public IHttpActionResult Update(Cliente cliente)
        {
            return HandleCallback(() => _clienteServico.Atualizar(cliente));
        }

        #endregion HttpPut

        #region HttpDelete
        [HttpDelete]
        public IHttpActionResult Delete(Cliente cliente)
        {
            return HandleCallback(() => _clienteServico.Deletar(cliente));
        }

        #endregion HttpDelete
    }
}
