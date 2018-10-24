using iTextSharp.text.pdf;
using NFe.Dominio.Features.Emitentes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFe.Infra.PDF.Features.Notas_Fiscais.Mapeadores
{
    public static class EmitenteMap
    {
        public static void SetarCamposEmitente(Emitente emitente, AcroFields fields)
        {
            fields.SetField("NOME", emitente.Nome);
            fields.SetField("LOGRADOURO", emitente.Endereco.Logradouro);
            fields.SetField("BAIRRO", emitente.Endereco.Bairro);
            fields.SetField("IDE_TPNF", "1");
            fields.SetField("IDE_TOTALPAGES", "1");
            fields.SetField("IDE_CURRENTPAGE", "1");
            fields.SetField("EMIT_IE", emitente.InscricaoEstadual);
            fields.SetField("EMIT_IM", emitente.InscricaoMunicipal);

            if (emitente.Cpf.EhValido)
                fields.SetField("EMIT_CPF", emitente.Cpf.ValorFormatado);
            else
                fields.SetField("EMIT_CNPJ", emitente.Cnpj.ValorFormatado);
        }
    }
}
