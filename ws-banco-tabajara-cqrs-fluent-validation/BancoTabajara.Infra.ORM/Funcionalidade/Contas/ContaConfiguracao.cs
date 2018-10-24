using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BancoTabajara.Dominio.Funcionalidades.Contas;

namespace BancoTabajara.Infra.ORM.Funcionalidade.Contas
{
	[ExcludeFromCodeCoverage]
	public class ContaConfiguracao: EntityTypeConfiguration<Conta>
	{
		public ContaConfiguracao()
		{
			this.ToTable( "TBConta" );
			this.Property( o => o.Limite ).IsRequired();
			this.Property( o => o.Estado ).IsRequired();
            this.Property( o => o.NumeroConta).HasColumnType("VARCHAR").IsRequired();

            this.HasMany(c => c.Movimentacoes).WithRequired(p => p.Conta);

            this.HasKey( o => o.Id );
		}
	}
}
