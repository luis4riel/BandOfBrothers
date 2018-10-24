using AutoMapper;
using BancoTabajara.Aplicacao.Funcionalidades.Clientes.Commands;
using BancoTabajara.Aplicacao.Funcionalidades.Clientes.Queries;
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

        public int Inserir(ClienteRegistroCommand clienteCmd)
        {

            var cliente = Mapper.Map<ClienteRegistroCommand, Cliente>(clienteCmd);

            var newcliente = _repositorioCliente.Inserir(cliente);

            return newcliente.Id;
        }

        public IQueryable<Cliente> PegarTodos( int? limite = null ) => _repositorioCliente.PegarTodos(limite);
        
        public ClienteQuery PegarPorId(int id)
        {
            var cliente = _repositorioCliente.PegarPorId(id);

            return Mapper.Map<Cliente, ClienteQuery>(cliente);            
        }

        public bool Deletar(ClienteRemoveCommand clienteCmd)
        {
            return _repositorioCliente.Deletar(clienteCmd.Id);
        }

        public bool Atualizar(ClienteAtualizarCommand clienteCmd)
        {
            var cliente = _repositorioCliente.PegarPorId(clienteCmd.Id) ?? throw new NaoEncontradoException();

            Mapper.Map(clienteCmd, cliente);

            return _repositorioCliente.Atualizar(cliente);
        }
    }
}
