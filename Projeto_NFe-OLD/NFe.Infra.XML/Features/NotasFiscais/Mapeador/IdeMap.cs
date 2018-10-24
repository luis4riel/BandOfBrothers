using NFe.Dominio.Features.Notas_Fiscais;
using NFe.Infra.XML.Features.NotasFiscais.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFe.Infra.XML.Features.NotasFiscais.Mapeador
{
    public static class IdeMap
    {
        public static IdeConfiguracao GeraValoresParaIdeNFeXml(NotaFiscal _notaFiscal)
        {
            IdeConfiguracao InfNFeConfiguracao = new IdeConfiguracao();

            InfNFeConfiguracao.NaturezaOperacao = _notaFiscal.NaturezaOperacao;
            if (_notaFiscal.DataEmissao == null)
                _notaFiscal.DataEmissao = DateTime.Now;
            InfNFeConfiguracao.DataEmissao = (DateTime)_notaFiscal.DataEmissao;

            return InfNFeConfiguracao;
        }
    }
}
