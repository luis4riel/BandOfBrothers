using NFe.Infra.XML.Features.NotasFiscais.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFe.Infra.XML.Features.NotasFiscais.Mapeador
{
    public static class InfNfeMap
    {
        public static InfNFeConfiguracao GeraValoresParaInfNFeXml(string chaveAcesso)
        {
            InfNFeConfiguracao InfNFeConfiguracao = new InfNFeConfiguracao();
            InfNFeConfiguracao.ChaveAcesso = "NFe" + chaveAcesso;
            InfNFeConfiguracao.Versao = "3.10";

            return InfNFeConfiguracao;
        }
    }
}
