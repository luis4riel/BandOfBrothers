using AutoMapper;
using BancoTabajara.Aplicacao.Funcionalidades.Clientes.Command;
using BancoTabajara.Dominio.Excecoes;
using BancoTabajara.Dominio.Funcionalidades.Clientes;
using System.Linq;

namespace BancoTabajara.Aplicacao.Funcionalidades.Clientes
{
    public class ClienteServico : IClienteServico
    {
        private readonly IClienteRepositorio _repositorioCliente;

        public ClienteServico(IClienteRepositorio repositorioCliente)
        {
            _repositorioCliente = repositorioCliente;
        }

        public int Inserir(CommandRegistrarCliente clienteCmd)
        {
            //cliente.Validar();
            var _cliente = Mapper.Map<CommandRegistrarCliente, Cliente>(clienteCmd);

            var newcliente = _repositorioCliente.Inserir(_cliente);

            return newcliente.Id;
        }

        public IQueryable<Cliente> PegarTodos( int? limite = null )
        {
            return _repositorioCliente.PegarTodos(limite);
        }

        public Cliente PegarPorId(int id)
        {
            if (id < 1)
                throw new NaoEncontradoException();

            return _repositorioCliente.PegarPorId(id) ?? throw new NaoEncontradoException();
        }

        public bool Deletar(CommandDeletarCliente clienteCmd)
        {
            var cliente = Mapper.Map<CommandDeletarCliente, Cliente>(clienteCmd);

            if (cliente == null || cliente.Id < 1)
                throw new NaoEncontradoException();

            return _repositorioCliente.Deletar(cliente.Id);
        }

        public bool Atualizar(CommandAtualizarCliente clienteCmd)
        {
            var cliente = Mapper.Map<CommandAtualizarCliente, Cliente>(clienteCmd);

            var clienteDb = _repositorioCliente.PegarPorId(cliente.Id) ?? throw new NaoEncontradoException();

			//clienteDb.Validar();

			return _repositorioCliente.Atualizar(clienteDb);
        }
    }
}
