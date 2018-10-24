using AutoMapper;
using BancoTabajara.Aplicacao.Funcionalidades.Clientes;
using BancoTabajara.Aplicacao.Funcionalidades.Clientes.Command;
using BancoTabajara.Dominio.Funcionalidades.Clientes;
using BancoTabajara.WebApi.Controllers.Common;
using BancoTabajara.WebApi.Extensions;
using BancoTabajara.WebApi.Models.Clientes.ViewModel;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace BancoTabajara.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/clientes")]
    public class ClientesController : ApiControllerBase
    {
        public IClienteServico _clienteServico;

        public ClientesController(IClienteServico clienteServico) : base()
        {
            _clienteServico = clienteServico;
        }

        #region HttpGet
		 
       [HttpGet]
        public IHttpActionResult Get()
        {
			var chaveValor = this.Request.GetQueryNameValuePairs();

			IQueryable<Cliente> query = HttpGetAllExtensions<Cliente>.GeraQueryParaGetAllCliente( _clienteServico, chaveValor );

			return HandleQuery<Cliente>( query );
		}

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            var cliente = _clienteServico.PegarPorId(id);
            
            return HandleCallback(() => Mapper.Map<Cliente, ViewModelCliente>(cliente));
        }
        #endregion HttpGet

        #region HttpPost
        [HttpPost]
        public IHttpActionResult Post(CommandRegistrarCliente clienteCmd)
        {
            var validador = clienteCmd.Validate();
            if (!validador.IsValid)
                return HandleValidationFailure(validador.Errors);

            return HandleCallback(() => _clienteServico.Inserir(clienteCmd));
        }

        #endregion HttpPost

        #region HttpPut
        [HttpPut]
        public IHttpActionResult Update(CommandAtualizarCliente clienteCmd)
        {
            var validador = clienteCmd.Validate();
            if (!validador.IsValid)
                return HandleValidationFailure(validador.Errors);

            return HandleCallback(() => _clienteServico.Atualizar(clienteCmd));
        }

        #endregion HttpPut

        #region HttpDelete
        [HttpDelete]
        public IHttpActionResult Delete(CommandDeletarCliente cliente)
        {
            var validador = cliente.Validate();
            if (!validador.IsValid)
                return HandleValidationFailure(validador.Errors);

            return HandleCallback(() => _clienteServico.Deletar(cliente));
        }

        #endregion HttpDelete
    }
}
