using BancoTabajara.Aplicacao.Base;
using BancoTabajara.Dominio.Funcionalidades.Contas;

namespace BancoTabajara.Aplicacao.Funcionalidades.Contas
{
	public interface IContaServico : IServico<Conta>
	{
		bool AtualizarStatus( Conta conta ); 
	}
}
