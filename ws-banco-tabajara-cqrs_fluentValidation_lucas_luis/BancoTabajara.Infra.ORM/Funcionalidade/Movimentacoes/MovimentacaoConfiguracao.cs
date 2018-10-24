using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BancoTabajara.Dominio.Funcionalidades.Contas;
using BancoTabajara.Dominio.Funcionalidades.Movimentacoes;

namespace BancoTabajara.Infra.ORM.Funcionalidade.Movimentacoes
{
	[ExcludeFromCodeCoverage]
	public class MovimentacaoConfiguracao: EntityTypeConfiguration<Movimentacao>
	{
		public MovimentacaoConfiguracao()
		{
			this.ToTable( "TBMovimentacao" );
			this.Property( o => o.Data ).IsRequired();
			this.Property( o => o.Tipo ).IsRequired();
			this.Property( o => o.Valor ).IsRequired();

			this.HasRequired( o => o.Conta );

			this.HasKey( o => o.Id );
		}
	}
}
