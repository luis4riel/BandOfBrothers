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
            var newcliente = _repositorioCliente.Inserir(cliente);

            return newcliente.Id;
        }

        public IQueryable<Cliente> PegarTodos( int? limite = null )
        {
            return _repositorioCliente.PegarTodos(limite);
        }

        public Cliente PegarPorId(int id)
        {
            return _repositorioCliente.PegarPorId(id);
        }

        public bool Deletar(Cliente cliente)
        {
            return _repositorioCliente.Deletar(cliente.Id);
        }

        public bool Atualizar(Cliente cliente)
        {
            var clienteDb = _repositorioCliente.PegarPorId(cliente.Id);
            if (clienteDb == null)
                throw new NaoEncontradoException();

            clienteDb.DataNascimento = cliente.DataNascimento;
            clienteDb.Cpf = cliente.Cpf;
            clienteDb.Nome = cliente.Nome;
            clienteDb.Rg = cliente.Rg;

            return _repositorioCliente.Atualizar(clienteDb);
        }
    }
}
