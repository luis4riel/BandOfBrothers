using NFe.Dominio.Features.Transportadores;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFe.Infra.Data.Features.Transportadores
{
    public class TransportadorEntityConfiguracao : EntityTypeConfiguration<Transportador>
    {
        public TransportadorEntityConfiguracao()
        {
            this.ToTable("TBTransportador");
            this.HasKey(t => t.Id);
            this.Property(t => t.Nome).IsOptional().HasMaxLength(50);
            this.Property(t => t.Cnpj.ValorFormatado).IsOptional().HasColumnName("Cnpj");
            this.Property(t => t.Cpf.ValorFormatado).IsOptional().HasColumnName("Cpf");
            this.Property(t => t.RazaoSocial).IsOptional().HasMaxLength(50);
            this.Property(t => t.InscricaoEstadual).IsRequired().HasMaxLength(50);
            this.Property(t => t.ResponsabilidadeFrete).IsRequired();
            this.Property(p => p.Endereco.Logradouro).IsRequired();
            this.Property(p => p.Endereco.Municipio).IsRequired();
            this.Property(p => p.Endereco.Bairro).IsRequired();
            this.Property(p => p.Endereco.Pais).IsRequired();
            this.Property(p => p.Endereco.Numero).IsRequired();
            this.Property(p => p.Endereco.Estado).IsRequired();
        }
    }
}
