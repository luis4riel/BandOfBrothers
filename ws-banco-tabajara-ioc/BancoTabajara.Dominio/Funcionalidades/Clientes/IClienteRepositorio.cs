using System.Linq;

namespace BancoTabajara.Dominio.Funcionalidades.Clientes
{
    public interface IClienteRepositorio
    {
        IQueryable<Cliente> PegarTodos(int? limite = null);

        Cliente Inserir(Cliente cliente);

        bool Atualizar(Cliente cliente);

        Cliente PegarPorId(int clienteId);

        bool Deletar(int clienteId);
    }
}
