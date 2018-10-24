using iTextSharp.text.pdf;
using NFe.Dominio.Features.Transportadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFe.Infra.PDF.Features.Notas_Fiscais.Mapeadores
{
    public static class TransportadorMap
    {
        public static void SetarCamposTransportador(Transportador transportador, AcroFields fields, int quantidade)
        {
            fields.SetField("TRANSP_TRANSPORTA_XNOME", transportador.Nome + transportador.RazaoSocial);
            fields.SetField("TRANSP_TRANSPORTA_XENDER", transportador.Endereco.Logradouro);
            fields.SetField("TRANSP_VOL_QVOL.0", quantidade.ToString());
            fields.SetField("TRANSP_MODFRETE", transportador.ResponsabilidadeFrete.ToString());
            fields.SetField("TRANSP_VEICTRANSP_UF", transportador.Endereco.Estado);

            if (transportador.Cnpj.EhValido)
                fields.SetField("TRANSP_TRANSPORTA_CNPJ", transportador.Cnpj.valor);
            else
                fields.SetField("TRANSP_TRANSPORTA_CPF", transportador.Cpf.valor);

            fields.SetField("TRANSP_TRANSPORTA_IE", transportador.InscricaoEstadual);
            fields.SetField("TRANSP_TRANSPORTA_XMUN", transportador.Endereco.Municipio);
            fields.SetField("TRANSP_TRANSPORTA_UF", transportador.Endereco.Estado);
        }
    }
}
