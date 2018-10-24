using Projeto_pizzaria.Dominio.Features.Pedidos;
using System.Data.Entity.ModelConfiguration;

namespace Projeto_pizzaria.Infra.Data.Features.Pedidos
{
    public class PedidoConfiguracao : EntityTypeConfiguration<Pedido>
    {
        public PedidoConfiguracao()
        {
            ToTable("TBPedido");

            Property(p => p.Departamento).IsOptional();
            Property(p => p.Responsavel).IsOptional();

			HasKey(a => a.Id);
        }
    }
}
