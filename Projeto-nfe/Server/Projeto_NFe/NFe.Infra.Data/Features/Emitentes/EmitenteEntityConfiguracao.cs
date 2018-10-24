using NFe.Dominio.Features.Emitentes;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFe.Infra.Data.Features.Emitentes
{
    public class EmitenteEntityConfiguracao : EntityTypeConfiguration<Emitente>
    {
        public EmitenteEntityConfiguracao()
        {
            this.ToTable("TBEmitente");
            this.HasKey(e => e.Id);
            this.Property(e => e.Nome).IsRequired().HasMaxLength(50);
            this.Property(e => e.RazaoSocial).IsRequired().HasMaxLength(50);
            this.Property(e => e.Cpf.ValorFormatado).IsOptional().HasColumnName("Cpf");
            this.Property(e => e.Cnpj.ValorFormatado).IsOptional().HasColumnName("Cnpj");
            this.Property(e => e.InscricaoEstadual).IsRequired().HasMaxLength(50);
            this.Property(e => e.InscricaoMunicipal).IsRequired().HasMaxLength(50);
            this.Property(p => p.Endereco.Logradouro).IsRequired();
            this.Property(p => p.Endereco.Municipio).IsRequired();
            this.Property(p => p.Endereco.Bairro).IsRequired();
            this.Property(p => p.Endereco.Pais).IsRequired();
            this.Property(p => p.Endereco.Numero).IsRequired();
            this.Property(p => p.Endereco.Estado).IsRequired();
        }
    }
}
