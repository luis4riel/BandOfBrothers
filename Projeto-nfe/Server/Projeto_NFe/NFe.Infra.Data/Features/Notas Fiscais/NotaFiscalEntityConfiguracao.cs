using NFe.Dominio.Features.Notas_Fiscais;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFe.Infra.Data.Features.Notas_Fiscais
{
    public class NotaFiscalEntityConfiguracao : EntityTypeConfiguration<NotaFiscal>
    {
        public NotaFiscalEntityConfiguracao()
        {
            this.ToTable("TBNotaFiscal");
            this.HasKey(n => n.Id);
            this.Property(n => n.NaturezaOperacao).IsRequired().HasMaxLength(50);
            this.Property(n => n.DataEmissao).IsOptional();
            this.Property(n => n.DataEntrada).IsRequired();
            this.Property(n => n.ChaveAcesso).IsOptional().HasMaxLength(50);
            this.Property(n => n.Emitido).IsRequired();
            this.Property(n => n.Valor.Frete).IsOptional().HasColumnName("ValorFrete");
            this.Property(n => n.Valor.TotalProdutos).IsOptional().HasColumnName("TotalProdutos");
            this.Property(n => n.Valor.TotalNota).IsOptional().HasColumnName("TotalNota");
            this.Property(n => n.Valor.ICMS).IsOptional().HasColumnName("ImpostoICMS");
            this.Property(n => n.Valor.Ipi).IsOptional().HasColumnName("ImpostoIPI");
            this.HasRequired(n => n.Destinatario).WithMany().HasForeignKey(n => n.IdDestinatario);

            this.HasRequired(n => n.Emitente).WithMany().HasForeignKey(n => n.IdEmitente);
    
            this.HasRequired(n => n.Transportador).WithMany().HasForeignKey(n => n.IdTransportador);
            this.Property(n => n.NotaFiscalXml).IsRequired().IsMaxLength();

        }
    }
}
