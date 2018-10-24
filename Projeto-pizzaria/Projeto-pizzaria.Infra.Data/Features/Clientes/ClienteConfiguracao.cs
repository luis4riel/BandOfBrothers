using Projeto_pizzaria.Dominio.Features.Clientes;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_pizzaria.Infra.Data.Features.Clientes
{
    public class ClienteConfiguracao : EntityTypeConfiguration<Cliente>
    {
        public ClienteConfiguracao()
        {
            ToTable("TBCliente");
            Property(p => p.TipoCliente).IsOptional();

            Property(p => p.Nome).IsRequired();
            Property(p => p.Endereco.Logradouro).IsRequired();
            Property(p => p.Endereco.Municipio).IsRequired();
            Property(p => p.Endereco.Bairro).IsRequired();

            HasKey(a => a.Id);
        }
    }
}
