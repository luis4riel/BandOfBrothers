using NFe.Dominio.Features.Notas_Fiscais;
using NFe.Infra.XML.Features.NotasFiscais.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFe.Infra.XML.Features.NotasFiscais.Mapeador
{
    public static class IcmsTotMap
    {
        public static ICMSTotConfiguracao GeraValoresParaIcmsTotalXml(NotaFiscal _notaFiscal)
        {
            ICMSTotConfiguracao ICMSTotConfiguracao = new ICMSTotConfiguracao();

            ICMSTotConfiguracao.ValorFrete = _notaFiscal.Valor.Frete;
            ICMSTotConfiguracao.ValorIcms = _notaFiscal.Valor.ICMS;
            ICMSTotConfiguracao.ValorIpi = _notaFiscal.Valor.Ipi;
            ICMSTotConfiguracao.ValorProdutos = _notaFiscal.Valor.TotalProdutos;
            ICMSTotConfiguracao.ValorTotalNota = _notaFiscal.Valor.TotalNota;

            return ICMSTotConfiguracao;
        }
    }
}
