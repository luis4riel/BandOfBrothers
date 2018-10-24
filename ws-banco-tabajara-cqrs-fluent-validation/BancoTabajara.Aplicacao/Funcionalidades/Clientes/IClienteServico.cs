using BancoTabajara.Aplicacao.Funcionalidades.Clientes.Commands;
using BancoTabajara.Aplicacao.Funcionalidades.Clientes.Queries;
using BancoTabajara.Dominio.Funcionalidades.Clientes;
using System.Linq;

namespace BancoTabajara.Aplicacao.Funcionalidades.Clientes
{
    public interface IClienteServico 
	{
        int Inserir(ClienteRegistroCommand clienteCmd);
        bool Atualizar(ClienteAtualizarCommand clienteCmd);
        bool Deletar(ClienteRemoveCommand clienteCmd);
        IQueryable<Cliente> PegarTodos(int? limite = null);
        ClienteQuery PegarPorId(int id);
    }
}
