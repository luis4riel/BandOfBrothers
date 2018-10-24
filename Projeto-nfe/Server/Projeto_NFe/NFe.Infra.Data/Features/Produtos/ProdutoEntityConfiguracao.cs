using NFe.Dominio.Features.Produtos;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFe.Infra.Data.Features.Produtos
{
    public class ProdutoEntityConfiguracao : EntityTypeConfiguration<Produto>
    {
        public ProdutoEntityConfiguracao()
        {
            this.ToTable("TBProduto");
            this.HasKey(p => p.Id);
            this.Property(p => p.CodigoProduto).IsRequired();
            this.Property(p => p.Descricao).IsRequired().HasMaxLength(50);
            this.Property(p => p.Quantidade).IsRequired();
            this.Property(p => p.ValorProduto.Unitario).IsRequired().HasColumnName("ValorUnitario");
            this.Property(p => p.ValorProduto.Total).IsRequired().HasColumnName("ValorTotal");
            this.Property(p => p.ValorProduto.Ipi).IsRequired().HasColumnName("ImpostoIpi");
            this.Property(p => p.ValorProduto.ICMS).IsRequired().HasColumnName("ImpostoICMS");
        }
    }
}
