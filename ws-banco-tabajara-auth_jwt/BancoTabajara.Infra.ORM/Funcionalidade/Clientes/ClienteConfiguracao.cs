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

            Property(x => x.Nome).HasColumnType("Varchar").HasMaxLength(100).IsRequired();
            Property(x => x.Cpf).HasColumnType("Varchar").HasMaxLength(20).IsRequired();
            Property(x => x.DataNascimento).HasColumnType("Datetime2").IsRequired();
            Property(x => x.Rg).HasColumnType("Varchar").HasMaxLength(10).IsRequired();

            this.HasKey(o => o.Id);
        }
    }
}
