using NFe.Dominio.Features.Notas_Fiscais;
using NFe.Infra.Utils;
using NFe.Infra.XML.Exceptions;
using NFe.Infra.XML.Features.NotasFiscais.Mapeador;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NFe.Infra.XML.Features.NotasFiscais
{
    public class NotaXmlRepositorio : INotaXmlRepositorio
    {
        public NotaXmlRepositorio()
        {
            NotaFiscalModeloXml = new NotaFiscalModeloXml();
        }

        public virtual NotaFiscalModeloXml NotaFiscalModeloXml { get; set; }

        public virtual string XmlNotaFiscal
        {
            get
            {
                return SerializeHelper.Serialize(NotaFiscalModeloXml);
            }
        }

        public void NotaFiscalParaXml(NotaFiscal notaFiscal)
        {
            if (notaFiscal != null)
            {
                NotaFiscalModeloXml.infNFe = InfNfeMap.GeraValoresParaInfNFeXml(notaFiscal.ChaveAcesso);
                NotaFiscalModeloXml.infNFe.ide = IdeMap.GeraValoresParaIdeNFeXml(notaFiscal);
                NotaFiscalModeloXml.infNFe.det = ProdutoMap.ProdutoParaXml(notaFiscal.Produtos);
                NotaFiscalModeloXml.infNFe.dest = DestinatarioMap.DestinatarioParaXml(notaFiscal.Destinatario);
                NotaFiscalModeloXml.infNFe.emit = EmitenteMap.EmitenteParaXml(notaFiscal.Emitente);
                NotaFiscalModeloXml.infNFe.total.ICMSTot = IcmsTotMap.GeraValoresParaIcmsTotalXml(notaFiscal);
                NotaFiscalModeloXml.infNFe.transp = TransportadorMap.TransportadorParaXml(notaFiscal.Transportador);
            }
            else
                throw new NotaFiscalXmlNulaException();
        }

        public NotaFiscal XmlParaNotaFiscal(NotaFiscal notaFiscal)
        {
            if (!(notaFiscal == null))
            {
                NotaFiscalModeloXml = SerializeHelper.Deserialize<NotaFiscalModeloXml>(notaFiscal.NotaFiscalXml);

                notaFiscal.Emitente = EmitenteMap.XmlParaEmitente(NotaFiscalModeloXml);
                notaFiscal.Emitente.Id = notaFiscal.Emitente.Id;
                notaFiscal.Transportador = TransportadorMap.XmlParaTransportador(NotaFiscalModeloXml);
                notaFiscal.Transportador.Id = notaFiscal.Transportador.Id;
                notaFiscal.Destinatario = DestinatarioMap.XmlParaDestinatario(NotaFiscalModeloXml);
                notaFiscal.Destinatario.Id = notaFiscal.Destinatario.Id;
                notaFiscal.Produtos = ProdutoMap.XmlParaProduto(NotaFiscalModeloXml);

                return notaFiscal;
            }
            else
                throw new NotaFiscalXmlNulaException();
        }

        public  void ExportaParaArquivoXml(string caminho, NotaFiscal notaFiscal)
        {
            if (notaFiscal != null)
            {
                using (var fs = new FileStream(caminho, FileMode.OpenOrCreate))
                using (StreamWriter file = new StreamWriter(fs, Encoding.UTF8))
                {
                    NotaFiscalParaXml(notaFiscal);
                    file.WriteLine(XmlNotaFiscal);
                }
            }
            else
             throw new NotaFiscalXmlNulaException();
        }
    }
}
