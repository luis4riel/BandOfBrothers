using iTextSharp.text.pdf;
using NFe.Dominio.Features.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFe.Infra.PDF.Features.Notas_Fiscais.Mapeadores
{
    public static class ProdutoMap
    {
        public static void SetarCamposProduto(IList<Produto> produtos, AcroFields fields)
        {
            for (int i = 0; i < produtos.Count; i++)
            {
                fields.SetField("DET_PROD_CPROD." + i, produtos[i].CodigoProduto.ToString());
                fields.SetField("DET_PROD_XPROD." + i, produtos[i].Descricao);
                fields.SetField("DET_PROD_QCOM." + i, produtos[i].Quantidade.ToString());
                fields.SetField("DET_PROD_VPROD." + i, produtos[i].ValorProduto.Total.ToString());
                fields.SetField("DET_IMPOSTO_ICMS_ICMS_PICMS." + i, produtos[i].ValorProduto.Aliquota.ICMS.ToString());
                fields.SetField("DET_IMPOSTO_IPI_IPITRIB_PIPI." + i, produtos[i].ValorProduto.Aliquota.Ipi.ToString());
                fields.SetField("DET_IMPOSTO_IPI_IPITRIB_VIPI." + i, produtos[i].ValorProduto.Ipi.ToString());
                fields.SetField("DET_PROD_VUNCOM." + i, produtos[i].ValorProduto.Unitario.ToString());
                fields.SetField("DET_IMPOSTO_ICMS_ICMS_VICMS." + i, produtos[i].ValorProduto.ICMS.ToString());
            }
        }
    }
}
