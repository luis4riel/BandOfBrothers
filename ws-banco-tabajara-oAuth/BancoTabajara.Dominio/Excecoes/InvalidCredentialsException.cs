using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Dominio.Excecoes
{
	public class InvalidCredentialsException : ExcecaoDeNegocio
	{
		public InvalidCredentialsException() : base( CodigoErros.Unauthorized, "O usuário e/ou senha estão incorretos" ) { }
	}
}