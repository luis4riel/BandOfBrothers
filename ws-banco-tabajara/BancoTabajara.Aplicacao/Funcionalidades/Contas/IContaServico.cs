using BancoTabajara.Aplicacao.Base;
using BancoTabajara.Dominio.Funcionalidades.Contas;
using BancoTabajara.Dominio.Funcionalidades.Extrato;

namespace BancoTabajara.Aplicacao.Funcionalidades.Contas
{
	public interface IContaServico : IServico<Conta>
	{
		bool AtualizarStatus( int id );
        bool Deposito(int id, decimal quantia);
        bool Saque(int id, decimal quantia);
        bool Transferencia(int idOrigem, int idDestino, decimal quantia);
        Extrato GerarExtrato(int id);
    }
}
