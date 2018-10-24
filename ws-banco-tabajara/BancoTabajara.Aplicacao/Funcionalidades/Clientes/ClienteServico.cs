using BancoTabajara.Dominio.Excecoes;
using BancoTabajara.Dominio.Funcionalidades.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Aplicacao.Funcionalidades.Clientes
{
    public class ClienteServico : IClienteServico
    {
        private readonly IClienteRepositorio _repositorioCliente;

        public ClienteServico(IClienteRepositorio repositorioCliente)
        {
            _repositorioCliente = repositorioCliente;
        }

        public int Inserir(Cliente cliente)
        {
			cliente.Validar();

            var newcliente = _repositorioCliente.Inserir(cliente);

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

        public bool Deletar(Cliente cliente)
        {
            if (cliente == null || cliente.Id < 1)
                throw new NaoEncontradoException();

            return _repositorioCliente.Deletar(cliente.Id);
        }

        public bool Atualizar(Cliente cliente)
        {
            var clienteDb = _repositorioCliente.PegarPorId(cliente.Id) ?? throw new NaoEncontradoException();

            clienteDb.DataNascimento = cliente.DataNascimento;
            clienteDb.Cpf = cliente.Cpf;
            clienteDb.Nome = cliente.Nome;
            clienteDb.Rg = cliente.Rg;

			clienteDb.Validar();

			return _repositorioCliente.Atualizar(clienteDb);
        }
    }
}
