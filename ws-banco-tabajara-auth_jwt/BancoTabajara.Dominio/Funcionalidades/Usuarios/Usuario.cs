using BancoTabajara.Dominio.Base;

namespace BancoTabajara.Dominio.Funcionalidades.Usuarios
{
	public class Usuario : Entidade
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}

	public class TokenConfigurations
	{
		public string Audience { get; set; }
		public string Issuer { get; set; }
		public int Seconds { get; set; }
	}
}