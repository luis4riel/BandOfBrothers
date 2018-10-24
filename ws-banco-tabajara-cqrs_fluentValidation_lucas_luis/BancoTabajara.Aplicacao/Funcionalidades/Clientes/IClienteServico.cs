using BancoTabajara.Aplicacao.Funcionalidades.Clientes.Command;
using BancoTabajara.Dominio.Funcionalidades.Clientes;
using System.Linq;

namespace BancoTabajara.Aplicacao.Funcionalidades.Clientes
{
    public interface IClienteServico
	{
        int Inserir(CommandRegistrarCliente entidade);
        bool Atualizar(CommandAtualizarCliente entidade);
        bool Deletar(CommandDeletarCliente entidade);
        IQueryable<Cliente> PegarTodos(int? limite = null);
        Cliente PegarPorId(int id);
    }
}
