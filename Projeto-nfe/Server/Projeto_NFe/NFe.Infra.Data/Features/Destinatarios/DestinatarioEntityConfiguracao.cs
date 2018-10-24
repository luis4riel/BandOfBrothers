using NFe.Dominio.Features.Destinatarios;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFe.Infra.Data.Features.Destinatarios
{
    public class DestinatarioEntityConfiguracao : EntityTypeConfiguration<Destinatario>
    {
        public DestinatarioEntityConfiguracao()
        {
            this.ToTable("TBDestinatario");
            this.HasKey(d => d.Id);
            this.Property(d => d.Nome).IsOptional().HasMaxLength(50);
            this.Property(d => d.Cnpj.ValorFormatado).IsOptional().HasColumnName("Cnpj");
            this.Property(d => d.Cpf.ValorFormatado).IsOptional().HasColumnName("Cpf");
            this.Property(d => d.RazaoSocial).IsOptional().HasMaxLength(50);
            this.Property(d => d.InscricaoEstadual).IsRequired().HasMaxLength(50);
            this.Property(p => p.Nome).IsRequired();
            this.Property(p => p.Endereco.Logradouro).IsRequired();
            this.Property(p => p.Endereco.Municipio).IsRequired();
            this.Property(p => p.Endereco.Bairro).IsRequired();
            this.Property(p => p.Endereco.Pais).IsRequired();
            this.Property(p => p.Endereco.Numero).IsRequired();
            this.Property(p => p.Endereco.Estado).IsRequired();
        }
    }
}
