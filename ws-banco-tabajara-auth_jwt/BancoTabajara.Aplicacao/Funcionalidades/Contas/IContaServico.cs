using BancoTabajara.Aplicacao.Base;
using BancoTabajara.Aplicacao.Funcionalidades.Contas.Command;
using BancoTabajara.Dominio.Funcionalidades.Contas;
using BancoTabajara.Dominio.Funcionalidades.Extrato;
using System.Linq;

namespace BancoTabajara.Aplicacao.Funcionalidades.Contas
{
	public interface IContaServico
	{
        int Inserir(CommandRegistrarConta entidade);

        bool Atualizar(CommandAtualizarConta entidade);

        bool Deletar(CommandDeletarConta entidade);

        IQueryable<Conta> PegarTodos(int? limite = null);
        
        Conta PegarPorId(int id);

        bool AtualizarStatus( int id );

        bool Deposito(int id, decimal quantia);

        bool Saque(int id, decimal quantia);

        bool Transferencia(int idOrigem, int idDestino, decimal quantia);

        Extrato GerarExtrato(int id);
    }
}
