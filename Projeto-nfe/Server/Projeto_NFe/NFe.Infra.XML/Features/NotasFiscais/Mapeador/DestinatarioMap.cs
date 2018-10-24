using NFe.Dominio.Features.Destinatarios;
using NFe.Infra.XML.Features.NotasFiscais.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFe.Infra.XML.Features.NotasFiscais.Mapeador
{
    public static class DestinatarioMap
    {
        public static Destinatario XmlParaDestinatario (NotaFiscalModeloXml NotaFiscalModeloXml)
        {
            Destinatario destinatario = new Destinatario();

            if (!string.IsNullOrEmpty(NotaFiscalModeloXml.infNFe.dest.CpfDestinatario))
                destinatario.Cpf = NotaFiscalModeloXml.infNFe.dest.CpfDestinatario;
            else
                destinatario.Cnpj = NotaFiscalModeloXml.infNFe.dest.CnpjDestinatario;

            destinatario.Nome = NotaFiscalModeloXml.infNFe.dest.Nome;
            destinatario.RazaoSocial = NotaFiscalModeloXml.infNFe.dest.Nome;
            destinatario.InscricaoEstadual = NotaFiscalModeloXml.infNFe.dest.InscricaoEstadual;
            destinatario.Endereco.Bairro = NotaFiscalModeloXml.infNFe.dest.enderDest.Bairro;
            destinatario.Endereco.Estado = NotaFiscalModeloXml.infNFe.dest.enderDest.Estado;
            destinatario.Endereco.Municipio = NotaFiscalModeloXml.infNFe.dest.enderDest.Municipio;
            destinatario.Endereco.Logradouro = NotaFiscalModeloXml.infNFe.dest.enderDest.Logradouro;
            destinatario.Endereco.Numero = NotaFiscalModeloXml.infNFe.dest.enderDest.Numero;
            destinatario.Endereco.Pais = NotaFiscalModeloXml.infNFe.dest.enderDest.Pais;

            return destinatario;
        }

        public static DestinatarioConfiguracao DestinatarioParaXml (Destinatario destinatario)
        {
            DestinatarioConfiguracao DestinatarioConfiguracao = new DestinatarioConfiguracao();

            if (destinatario.Cnpj.ValorFormatado != "")
                DestinatarioConfiguracao.CnpjDestinatario = destinatario.Cnpj.ValorFormatado;
            else
                DestinatarioConfiguracao.CpfDestinatario = destinatario.Cpf.ValorFormatado;

            if (destinatario.Nome != "")
                DestinatarioConfiguracao.Nome = destinatario.Nome;
            else
                DestinatarioConfiguracao.Nome = destinatario.RazaoSocial;

            DestinatarioConfiguracao.InscricaoEstadual = destinatario.InscricaoEstadual;
            DestinatarioConfiguracao.enderDest.Estado = destinatario.Endereco.Estado;
            DestinatarioConfiguracao.enderDest.Logradouro = destinatario.Endereco.Logradouro;
            DestinatarioConfiguracao.enderDest.Municipio = destinatario.Endereco.Municipio;
            DestinatarioConfiguracao.enderDest.Numero = destinatario.Endereco.Numero;
            DestinatarioConfiguracao.enderDest.Pais = destinatario.Endereco.Pais;
            DestinatarioConfiguracao.enderDest.Bairro = destinatario.Endereco.Bairro;

            return DestinatarioConfiguracao;
        }
    }
}
