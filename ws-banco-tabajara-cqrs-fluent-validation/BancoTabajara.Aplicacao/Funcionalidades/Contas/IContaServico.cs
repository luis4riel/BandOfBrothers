using BancoTabajara.Aplicacao.Funcionalidades.Contas.Commands;
using BancoTabajara.Aplicacao.Funcionalidades.Contas.Queries;
using BancoTabajara.Dominio.Funcionalidades.Contas;
using BancoTabajara.Dominio.Funcionalidades.Extrato;
using System.Linq;

namespace BancoTabajara.Aplicacao.Funcionalidades.Contas
{
    public interface IContaServico
	{
        int Inserir(ContaRegistroCommand cliente);
        bool Atualizar(ContaAtualizarCommand cliente);
        bool Deletar(ContaRemoveCommand cliente);
        IQueryable<ContaQuery> PegarTodos(int? limite = null);
        ContaQuery PegarPorId(int id);
        bool AtualizarStatus( int id );
        bool Deposito(int id, decimal quantia);
        bool Saque(int id, decimal quantia);
        bool Transferencia(int idOrigem, int idDestino, decimal quantia);
        Extrato GerarExtrato(int id);
    }
}
