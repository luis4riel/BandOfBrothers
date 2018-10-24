using iTextSharp.text.pdf;
using NFe.Dominio.Features.Notas_Fiscais;
using NFe.Infra.PDF.Features.Notas_Fiscais.Mapeadores;
using System;
using System.IO;
using System.Linq;

namespace NFe.Infra.PDF.Features.Notas_Fiscais
{
    public static class NotaFiscalPdf
    {
        public static void Exportar(string caminho, NotaFiscal nota)
        {
            string caminhoModeloNFE = AppDomain.CurrentDomain.BaseDirectory + "\\..\\..\\..\\NFe.Infra.PDF\\Modelos\\Modelo NFE.pdf";
            PdfReader reader = new PdfReader(caminhoModeloNFE);
            using (PdfStamper stamper = new PdfStamper(reader, new FileStream(caminho, FileMode.Create)))
            {
                AcroFields fields = stamper.AcroFields;

                EmitenteMap.SetarCamposEmitente(nota.Emitente, fields);
                DestinatarioMap.SetarCamposDestinatario(nota.Destinatario, Convert.ToDateTime(nota.DataEmissao), nota.DataEntrada, fields);
                ValorNotaMap.SetarCamposValorNota(nota.Valor, nota.ChaveAcesso, nota.NaturezaOperacao, fields);
                TransportadorMap.SetarCamposTransportador(nota.Transportador, fields, nota.Produtos.Sum(prod => prod.Quantidade));
                ProdutoMap.SetarCamposProduto(nota.Produtos, fields);

                stamper.FormFlattening = true;
                stamper.Close();
            }
        }
    }
}
