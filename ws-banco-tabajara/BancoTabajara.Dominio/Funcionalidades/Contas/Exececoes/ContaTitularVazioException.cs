using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BancoTabajara.Dominio.Excecoes;

namespace BancoTabajara.Dominio.Funcionalidades.Contas.Exececoes
{
	public class ContaTitularVazioException : ExcecaoDeNegocio
	{
		public ContaTitularVazioException() : base( "O titular não pode ser vazio" )
		{
		}
	}
}
