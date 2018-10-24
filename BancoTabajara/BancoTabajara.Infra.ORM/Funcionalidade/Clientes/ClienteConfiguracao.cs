using BancoTabajara.Dominio.Funcionalidades.Clientes;
using System.Data.Entity.ModelConfiguration;
using System.Diagnostics.CodeAnalysis;

namespace BancoTabajara.Infra.ORM.Funcionalidade.Clientes
{
    [ExcludeFromCodeCoverage]
    public class ClienteConfiguracao : EntityTypeConfiguration<Cliente>
    {
        public ClienteConfiguracao()
        {
            this.ToTable("TBCliente");
            this.HasKey(o => o.Id);
        }
    }
}
