using iTextSharp.text.pdf;
using NFe.Dominio.Features.Destinatarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFe.Infra.PDF.Features.Notas_Fiscais.Mapeadores
{
    public class DestinatarioMap
    {
        public static void SetarCamposDestinatario(Destinatario destinatario, DateTime dataEmissao, DateTime dataEntrada, AcroFields fields)
        {
            fields.SetField("DEST_XNOME", destinatario.Nome + " | " + destinatario.RazaoSocial);
            fields.SetField("IDE_DEMI", Convert.ToDateTime(dataEmissao).ToShortDateString());
            fields.SetField("IDE_DSAIENT", dataEntrada.ToShortDateString());

            if (destinatario.Cpf.EhValido)
                fields.SetField("DEST_CPF", destinatario.Cpf.ValorFormatado);
            else
                fields.SetField("DEST_CNPJ", destinatario.Cnpj.ValorFormatado);

            fields.SetField("DEST_ENDERDEST_XLGR", destinatario.Endereco.Logradouro);
            fields.SetField("DEST_ENDERDEST_XBAIRRO", destinatario.Endereco.Bairro);
            fields.SetField("DEST_ENDERDEST_XMUN", destinatario.Endereco.Municipio);
            fields.SetField("DEST_ENDERDEST_UF", destinatario.Endereco.Estado);
            fields.SetField("DEST_ENDERDEST_NRO", destinatario.Endereco.Numero);
            fields.SetField("DEST_IE", destinatario.InscricaoEstadual);
        }
    }
}
