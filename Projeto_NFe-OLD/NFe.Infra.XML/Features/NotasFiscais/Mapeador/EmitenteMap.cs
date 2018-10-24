using NFe.Dominio.Features.Emitentes;
using NFe.Infra.XML.Features.NotasFiscais.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFe.Infra.XML.Features.NotasFiscais.Mapeador
{
    public static class EmitenteMap
    {
        public static Emitente XmlParaEmitente(NotaFiscalModeloXml NotaFiscalModeloXml)
        {
            Emitente emitente = new Emitente();
            emitente.Cnpj = NotaFiscalModeloXml.infNFe.emit.CnpjEmitente;
            emitente.Nome = NotaFiscalModeloXml.infNFe.emit.Nome;
            emitente.RazaoSocial = NotaFiscalModeloXml.infNFe.emit.RazaoSocial;
            emitente.InscricaoEstadual = NotaFiscalModeloXml.infNFe.emit.InscricaoEstadual;
            emitente.InscricaoMunicipal = NotaFiscalModeloXml.infNFe.emit.InscricaoMunicipal;

            emitente.Endereco.Bairro = NotaFiscalModeloXml.infNFe.emit.enderDest.Bairro;
            emitente.Endereco.Estado = NotaFiscalModeloXml.infNFe.emit.enderDest.Estado;
            emitente.Endereco.Municipio = NotaFiscalModeloXml.infNFe.emit.enderDest.Municipio;
            emitente.Endereco.Logradouro = NotaFiscalModeloXml.infNFe.emit.enderDest.Logradouro;
            emitente.Endereco.Numero = NotaFiscalModeloXml.infNFe.emit.enderDest.Numero;
            emitente.Endereco.Pais = NotaFiscalModeloXml.infNFe.emit.enderDest.Pais;

            return emitente;
        }

        public static EmitenteConfiguracao EmitenteParaXml(Emitente emitente)
        {
            EmitenteConfiguracao EmitenteConfiguracao = new EmitenteConfiguracao();

            EmitenteConfiguracao.CnpjEmitente = emitente.Cnpj.valorFormatado;

            EmitenteConfiguracao.RazaoSocial = emitente.RazaoSocial;
            EmitenteConfiguracao.Nome = emitente.Nome;

            EmitenteConfiguracao.InscricaoEstadual = emitente.InscricaoEstadual;
            EmitenteConfiguracao.InscricaoMunicipal = emitente.InscricaoMunicipal;
            EmitenteConfiguracao.enderDest.Estado = emitente.Endereco.Estado;
            EmitenteConfiguracao.enderDest.Logradouro = emitente.Endereco.Logradouro;
            EmitenteConfiguracao.enderDest.Municipio = emitente.Endereco.Municipio;
            EmitenteConfiguracao.enderDest.Numero = emitente.Endereco.Numero;
            EmitenteConfiguracao.enderDest.Pais = emitente.Endereco.Pais;
            EmitenteConfiguracao.enderDest.Bairro = emitente.Endereco.Bairro;

            return EmitenteConfiguracao;
        }
    }
}
