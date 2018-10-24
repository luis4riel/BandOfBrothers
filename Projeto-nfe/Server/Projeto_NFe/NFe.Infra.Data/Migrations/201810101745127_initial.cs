namespace NFe.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TBDestinatario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                        RazaoSocial = c.String(maxLength: 50),
                        Cpf = c.String(),
                        Cnpj = c.String(),
                        InscricaoEstadual = c.String(nullable: false, maxLength: 50),
                        Endereco_Logradouro = c.String(nullable: false),
                        Endereco_Numero = c.String(nullable: false),
                        Endereco_Bairro = c.String(nullable: false),
                        Endereco_Municipio = c.String(nullable: false),
                        Endereco_Estado = c.String(nullable: false),
                        Endereco_Pais = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TBEmitente",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InscricaoMunicipal = c.String(nullable: false, maxLength: 50),
                        Nome = c.String(nullable: false, maxLength: 50),
                        RazaoSocial = c.String(nullable: false, maxLength: 50),
                        Cpf = c.String(),
                        Cnpj = c.String(),
                        InscricaoEstadual = c.String(nullable: false, maxLength: 50),
                        Endereco_Logradouro = c.String(nullable: false),
                        Endereco_Numero = c.String(nullable: false),
                        Endereco_Bairro = c.String(nullable: false),
                        Endereco_Municipio = c.String(nullable: false),
                        Endereco_Estado = c.String(nullable: false),
                        Endereco_Pais = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TBNotaFiscal",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NotaFiscalXml = c.String(nullable: false),
                        NaturezaOperacao = c.String(nullable: false, maxLength: 50),
                        DataEmissao = c.DateTime(),
                        DataEntrada = c.DateTime(nullable: false),
                        ChaveAcesso = c.String(nullable: false, maxLength: 50),
                        Emitido = c.Boolean(nullable: false),
                        ValorFrete = c.Decimal(precision: 18, scale: 2),
                        TotalProdutos = c.Decimal(precision: 18, scale: 2),
                        TotalNota = c.Decimal(precision: 18, scale: 2),
                        ImpostoICMS = c.Decimal(precision: 18, scale: 2),
                        ImpostoIPI = c.Decimal(precision: 18, scale: 2),
                        IdDestinatario = c.Int(nullable: false),
                        IdEmitente = c.Int(nullable: false),
                        IdTransportador = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TBDestinatario", t => t.IdDestinatario, cascadeDelete: true)
                .ForeignKey("dbo.TBEmitente", t => t.IdEmitente, cascadeDelete: true)
                .ForeignKey("dbo.TBTransportador", t => t.IdTransportador, cascadeDelete: true)
                .Index(t => t.IdDestinatario)
                .Index(t => t.IdEmitente)
                .Index(t => t.IdTransportador);
            
            CreateTable(
                "dbo.TBProduto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CodigoProduto = c.Int(nullable: false),
                        Descricao = c.String(nullable: false, maxLength: 50),
                        Quantidade = c.Int(nullable: false),
                        ValorTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ImpostoICMS = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ImpostoIpi = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NotaFiscal_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TBNotaFiscal", t => t.NotaFiscal_Id)
                .Index(t => t.NotaFiscal_Id);
            
            CreateTable(
                "dbo.TBTransportador",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ResponsabilidadeFrete = c.Int(nullable: false),
                        Nome = c.String(maxLength: 50),
                        RazaoSocial = c.String(maxLength: 50),
                        Cpf = c.String(),
                        Cnpj = c.String(),
                        InscricaoEstadual = c.String(nullable: false, maxLength: 50),
                        Endereco_Logradouro = c.String(nullable: false),
                        Endereco_Numero = c.String(nullable: false),
                        Endereco_Bairro = c.String(nullable: false),
                        Endereco_Municipio = c.String(nullable: false),
                        Endereco_Estado = c.String(nullable: false),
                        Endereco_Pais = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBNotaFiscal", "IdTransportador", "dbo.TBTransportador");
            DropForeignKey("dbo.TBProduto", "NotaFiscal_Id", "dbo.TBNotaFiscal");
            DropForeignKey("dbo.TBNotaFiscal", "IdEmitente", "dbo.TBEmitente");
            DropForeignKey("dbo.TBNotaFiscal", "IdDestinatario", "dbo.TBDestinatario");
            DropIndex("dbo.TBProduto", new[] { "NotaFiscal_Id" });
            DropIndex("dbo.TBNotaFiscal", new[] { "IdTransportador" });
            DropIndex("dbo.TBNotaFiscal", new[] { "IdEmitente" });
            DropIndex("dbo.TBNotaFiscal", new[] { "IdDestinatario" });
            DropTable("dbo.TBTransportador");
            DropTable("dbo.TBProduto");
            DropTable("dbo.TBNotaFiscal");
            DropTable("dbo.TBEmitente");
            DropTable("dbo.TBDestinatario");
        }
    }
}
