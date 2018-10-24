using NFe.Dominio.Features.Transportadores;
using NFe.Infra.XML.Features.NotasFiscais.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFe.Infra.XML.Features.NotasFiscais.Mapeador
{
    public static class TransportadorMap
    {
        public static Transportador XmlParaTransportador (NotaFiscalModeloXml NotaFiscalModeloXml)
        {
            Transportador transportador = new Transportador();

            transportador.Cnpj = NotaFiscalModeloXml.infNFe.transp.Transporta.CnpjDestinatario;
            transportador.Cpf = NotaFiscalModeloXml.infNFe.transp.Transporta.CnpjDestinatario;
            transportador.InscricaoEstadual = NotaFiscalModeloXml.infNFe.transp.Transporta.InscricaoEstadual;
            transportador.Nome = NotaFiscalModeloXml.infNFe.transp.Transporta.Nome;
            transportador.RazaoSocial = NotaFiscalModeloXml.infNFe.transp.Transporta.Nome;
            transportador.ResponsabilidadeFrete = (Frete)NotaFiscalModeloXml.infNFe.transp.modFrete;
            transportador.Endereco.Estado = NotaFiscalModeloXml.infNFe.transp.Transporta.Estado;
            transportador.Endereco.Municipio = NotaFiscalModeloXml.infNFe.transp.Transporta.Municipio;
            transportador.Endereco.Logradouro = NotaFiscalModeloXml.infNFe.transp.Transporta.Logradouro;
            transportador.Endereco.Bairro = "Valor Não Informado";
            transportador.Endereco.Numero = "s/n";
            transportador.Endereco.Pais = "Brasil";

            return transportador;
        }

        public static TransportadorConfiguracao TransportadorParaXml (Transportador transportador)
        {
            TransportadorConfiguracao TransportadorConfiguracao = new TransportadorConfiguracao();

            TransportadorConfiguracao.modFrete = (int)transportador.ResponsabilidadeFrete;
            if (string.IsNullOrEmpty(transportador.Cpf))
                TransportadorConfiguracao.Transporta.CnpjDestinatario = transportador.Cnpj.ValorFormatado;
            else
                TransportadorConfiguracao.Transporta.CnpjDestinatario = transportador.Cpf.ValorFormatado;
            TransportadorConfiguracao.Transporta.Estado = transportador.Endereco.Estado;
            TransportadorConfiguracao.Transporta.InscricaoEstadual = transportador.InscricaoEstadual;
            TransportadorConfiguracao.Transporta.Logradouro = transportador.Endereco.Logradouro;
            TransportadorConfiguracao.Transporta.Municipio = transportador.Endereco.Municipio;
            if (string.IsNullOrEmpty(transportador.Nome))
                TransportadorConfiguracao.Transporta.Nome = transportador.RazaoSocial;
            else
                TransportadorConfiguracao.Transporta.Nome = transportador.Nome;

            return TransportadorConfiguracao;
        }
    }
}
