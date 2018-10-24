using iTextSharp.text.pdf;
using NFe.Dominio.Features.Valores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFe.Infra.PDF.Features.Notas_Fiscais.Mapeadores
{
    public static class ValorNotaMap
    {
        public static void SetarCamposValorNota(ValorNota valorNota, string chaveAcesso, string naturezaOperacao, AcroFields fields)
        {
            fields.SetField("IDE_NATOP", naturezaOperacao);
            fields.SetField("IDE_REFNFEMASK", chaveAcesso);
            fields.SetField("TOTAL_ICMSTOT_VFRETE", valorNota.Frete.ToString());
            fields.SetField("TOTAL_ICMSTOT_VICMS", valorNota.ICMS.ToString());
            fields.SetField("TOTAL_ICMSTOT_VPROD", valorNota.TotalProdutos.ToString());
            fields.SetField("TOTAL_ICMSTOT_VIPI", valorNota.Ipi.ToString());
            fields.SetField("TOTAL_ICMSTOT_VNF", valorNota.TotalNota.ToString());
        }
    }
}
