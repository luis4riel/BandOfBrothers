using BancoTabajara.Dominio.Base;

namespace BancoTabajara.Dominio.Funcionalidades.Usuarios
{
	public class Usuario : Entidade
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
	}
}