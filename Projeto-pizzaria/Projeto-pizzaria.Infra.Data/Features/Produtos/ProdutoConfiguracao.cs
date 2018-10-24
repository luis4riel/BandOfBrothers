using Projeto_pizzaria.Dominio.Features.Produtos;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_pizzaria.Infra.Data.Features.Produtos
{
    public class ProdutoConfiguracao : EntityTypeConfiguration<Produto>
    {
        public ProdutoConfiguracao()
        {
            ToTable("TBProduto");
        }
    }
}
