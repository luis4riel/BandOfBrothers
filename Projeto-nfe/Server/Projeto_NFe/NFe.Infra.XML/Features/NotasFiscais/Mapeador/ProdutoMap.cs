using NFe.Dominio.Features.Produtos;
using NFe.Infra.XML.Features.NotasFiscais.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFe.Infra.XML.Features.NotasFiscais.Mapeador
{
    public static class ProdutoMap
    {
        public static IList<Produto> XmlParaProduto (NotaFiscalModeloXml NotaFiscalModeloXml)
        {
            IList<Produto> listaProdutos = new List<Produto>();

            foreach (var prodDet in NotaFiscalModeloXml.infNFe.det)
            {
                Produto produto = new Produto();

                produto.CodigoProduto = prodDet.Prod.CodigoProduto;
                produto.Descricao = prodDet.Prod.DescricaoProduto;
                produto.Quantidade = prodDet.Prod.Quantidade;
                produto.ValorProduto.ICMS = prodDet.Imposto.Icms.IcmsProduto.Icms;
                produto.ValorProduto.Ipi = prodDet.Imposto.Icms.IcmsProduto.Ipi;
                produto.ValorProduto.Unitario = prodDet.Prod.Unitario;

                listaProdutos.Add(produto);
            }

            return listaProdutos;
        }

        public static List<ProdutoConfiguracao> ProdutoParaXml (IList<Produto> produtos)
        {
            List<ProdutoConfiguracao> det = new List<ProdutoConfiguracao>();

            int i = 1;

            foreach (var produto in produtos)
            {
                ProdutoConfiguracao produtoConfiguracao = new ProdutoConfiguracao();

                produtoConfiguracao.Prod.CodigoProduto = produto.CodigoProduto;
                produtoConfiguracao.Prod.DescricaoProduto = "Trib ICMS Integral Aliquota 10.00 - PIS e COFINS cod 04 - Orig 0";
                produtoConfiguracao.Prod.Quantidade = produto.Quantidade;
                produtoConfiguracao.Prod.Total = produto.ValorProduto.Total;
                produtoConfiguracao.Prod.Unitario = produto.ValorProduto.Unitario;
                produtoConfiguracao.nItemNumber = i;
                produtoConfiguracao.Imposto.Icms.IcmsProduto.Icms = produto.ValorProduto.ICMS;
                produtoConfiguracao.Imposto.Icms.IcmsProduto.Ipi = produto.ValorProduto.Ipi;

                det.Add(produtoConfiguracao);

                i++;
            }

            return det;
        }
    }
}
