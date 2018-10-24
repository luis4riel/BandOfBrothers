using System.Data.Entity.ModelConfiguration;
using System.Diagnostics.CodeAnalysis;
using BancoTabajara.Dominio.Funcionalidades.Usuarios;

namespace BancoTabajara.Infra.ORM.Funcionalidade.Usuarios
{
	[ExcludeFromCodeCoverage]
	public class UsuarioConfiguracao: EntityTypeConfiguration<Usuario>
	{
		public UsuarioConfiguracao()
		{
			this.ToTable( "TBUsuario" );
			this.Property( o => o.Name ).IsRequired();
			this.Property( o => o.Password ).IsRequired();

            this.HasKey( o => o.Id );
		}
	}
}
