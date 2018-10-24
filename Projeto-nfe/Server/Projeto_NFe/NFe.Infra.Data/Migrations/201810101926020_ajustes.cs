
namespace NFe.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public partial class ajustes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TBProduto", "NotaFiscal_Id", "dbo.TBNotaFiscal");
            DropIndex("dbo.TBProduto", new[] { "NotaFiscal_Id" });
            CreateTable(
                "dbo.ProdutoNotaFiscals",
                c => new
                    {
                        Produto_Id = c.Int(nullable: false),
                        NotaFiscal_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Produto_Id, t.NotaFiscal_Id })
                .ForeignKey("dbo.TBProduto", t => t.Produto_Id, cascadeDelete: true)
                .ForeignKey("dbo.TBNotaFiscal", t => t.NotaFiscal_Id, cascadeDelete: true)
                .Index(t => t.Produto_Id)
                .Index(t => t.NotaFiscal_Id);
            
            AlterColumn("dbo.TBNotaFiscal", "ChaveAcesso", c => c.String(maxLength: 50));
            DropColumn("dbo.TBProduto", "NotaFiscal_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TBProduto", "NotaFiscal_Id", c => c.Int());
            DropForeignKey("dbo.ProdutoNotaFiscals", "NotaFiscal_Id", "dbo.TBNotaFiscal");
            DropForeignKey("dbo.ProdutoNotaFiscals", "Produto_Id", "dbo.TBProduto");
            DropIndex("dbo.ProdutoNotaFiscals", new[] { "NotaFiscal_Id" });
            DropIndex("dbo.ProdutoNotaFiscals", new[] { "Produto_Id" });
            AlterColumn("dbo.TBNotaFiscal", "ChaveAcesso", c => c.String(nullable: false, maxLength: 50));
            DropTable("dbo.ProdutoNotaFiscals");
            CreateIndex("dbo.TBProduto", "NotaFiscal_Id");
            AddForeignKey("dbo.TBProduto", "NotaFiscal_Id", "dbo.TBNotaFiscal", "Id");
        }
    }
}
